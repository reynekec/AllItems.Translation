using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Domain;
using AllItems.Translation.Core.Tokenization;

namespace AllItems.Translation.Core.Translation;

/// <summary>
/// Orchestrates a single-source-to-single-target word-by-word translation: tokenizes the
/// sentence, resolves each word against the local cache (fetching+caching from
/// <see cref="ITranslationProvider"/> only for words not yet seen), and uses the full-sentence
/// translation both as a cached reference line and as a context hint for picking the right
/// meaning of an ambiguous word the first time it is encountered.
/// </summary>
public sealed class SentenceTranslationService : ISentenceTranslationService
{
    private readonly ITranslationProvider _translationProvider;
    private readonly IWordRepository _wordRepository;
    private readonly ISentenceTranslationRepository _sentenceRepository;
    private readonly IApiUsageTracker _usageTracker;
    private readonly ISentenceTokenizer _tokenizer;
    private readonly IWordAligner _wordAligner;

    public SentenceTranslationService(
        ITranslationProvider translationProvider,
        IWordRepository wordRepository,
        ISentenceTranslationRepository sentenceRepository,
        IApiUsageTracker usageTracker,
        ISentenceTokenizer tokenizer,
        IWordAligner wordAligner)
    {
        _translationProvider = translationProvider;
        _wordRepository = wordRepository;
        _sentenceRepository = sentenceRepository;
        _usageTracker = usageTracker;
        _tokenizer = tokenizer;
        _wordAligner = wordAligner;
    }

    public async Task<SentenceTranslationResult> TranslateAsync(
        string sourceSentence,
        Language sourceLanguage,
        Language targetLanguage,
        CancellationToken cancellationToken = default)
    {
        sourceSentence ??= string.Empty;
        var tokens = _tokenizer.Tokenize(sourceSentence);
        var wordPositions = tokens
            .Select((token, index) => (token, index))
            .Where(x => x.token.IsWord)
            .ToList();

        if (wordPositions.Count == 0)
        {
            var passthroughSlots = tokens
                .Select((token, index) => new TranslatedSlot(index, false, token.Text, null, [], 0))
                .ToList();
            return new SentenceTranslationResult(sourceLanguage, targetLanguage, sourceSentence, passthroughSlots, string.Empty);
        }

        var normalizedSentence = sourceSentence.Trim();
        var referenceTranslation = await _sentenceRepository.FindAsync(sourceLanguage, targetLanguage, normalizedSentence, cancellationToken);
        if (referenceTranslation is null)
        {
            referenceTranslation = await _translationProvider.TranslateSentenceAsync(sourceSentence, sourceLanguage, targetLanguage, cancellationToken);
            await _sentenceRepository.SaveAsync(sourceLanguage, targetLanguage, normalizedSentence, referenceTranslation, cancellationToken);
            await _usageTracker.RecordCharactersAsync(sourceSentence.Length, cancellationToken);
        }

        var sourceWords = wordPositions.Select(x => x.token.Text).ToList();
        var alignedGuesses = _wordAligner.AlignWords(sourceWords, referenceTranslation);

        var slots = new TranslatedSlot?[tokens.Count];

        for (var wordIndex = 0; wordIndex < wordPositions.Count; wordIndex++)
        {
            var (token, tokenIndex) = wordPositions[wordIndex];
            var slot = await ResolveWordSlotAsync(
                token.Text,
                tokenIndex,
                sourceLanguage,
                targetLanguage,
                alignedGuesses[wordIndex],
                cancellationToken);
            slots[tokenIndex] = slot;
        }

        for (var i = 0; i < tokens.Count; i++)
        {
            slots[i] ??= new TranslatedSlot(i, false, tokens[i].Text, null, [], 0);
        }

        return new SentenceTranslationResult(sourceLanguage, targetLanguage, sourceSentence, slots!, referenceTranslation);
    }

    private async Task<TranslatedSlot> ResolveWordSlotAsync(
        string sourceWord,
        int tokenIndex,
        Language sourceLanguage,
        Language targetLanguage,
        string? contextGuess,
        CancellationToken cancellationToken)
    {
        var normalized = sourceWord.ToLowerInvariant();
        var wordEntry = await _wordRepository.GetOrCreateAsync(sourceLanguage, normalized, cancellationToken);
        var existing = await _wordRepository.GetTranslationsAsync(wordEntry.Id, targetLanguage, cancellationToken);

        if (existing.Count == 0)
        {
            var isolated = await _translationProvider.TranslateWordAsync(sourceWord, sourceLanguage, targetLanguage, cancellationToken);
            await _usageTracker.RecordCharactersAsync(sourceWord.Length, cancellationToken);

            var added = new List<WordTranslation>();
            var contextDiffers = contextGuess is not null
                && !string.Equals(contextGuess, isolated, StringComparison.OrdinalIgnoreCase)
                && !string.Equals(contextGuess, sourceWord, StringComparison.OrdinalIgnoreCase);

            if (contextDiffers)
            {
                added.Add(await _wordRepository.AddTranslationAsync(wordEntry.Id, targetLanguage, contextGuess!, isPreferred: true, cancellationToken));
                added.Add(await _wordRepository.AddTranslationAsync(wordEntry.Id, targetLanguage, isolated, isPreferred: false, cancellationToken));
            }
            else
            {
                added.Add(await _wordRepository.AddTranslationAsync(wordEntry.Id, targetLanguage, isolated, isPreferred: true, cancellationToken));
            }

            existing = added;
        }
        else if (contextGuess is not null && !existing.Any(e => string.Equals(e.TargetText, contextGuess, StringComparison.OrdinalIgnoreCase)))
        {
            var newOption = await _wordRepository.AddTranslationAsync(wordEntry.Id, targetLanguage, contextGuess, isPreferred: false, cancellationToken);
            existing = [.. existing, newOption];
        }

        var options = existing.Select(e => new WordMeaningOption(e.Id, e.TargetText)).ToList();
        var selectedIndex = Math.Max(0, existing.ToList().FindIndex(e => e.IsPreferred));

        return new TranslatedSlot(tokenIndex, true, sourceWord, wordEntry.Id, options, selectedIndex);
    }

    public async Task<SentenceTranslationResult> CycleWordMeaningAsync(
        SentenceTranslationResult currentResult,
        int tokenIndex,
        CancellationToken cancellationToken = default)
    {
        var slot = currentResult.Slots.FirstOrDefault(s => s.TokenIndex == tokenIndex);
        if (slot is null || !slot.IsWord || slot.Options.Count <= 1 || slot.WordEntryId is null)
        {
            return currentResult;
        }

        var nextIndex = (slot.SelectedOptionIndex + 1) % slot.Options.Count;
        var nextOption = slot.Options[nextIndex];

        await _wordRepository.SetPreferredAsync(slot.WordEntryId.Value, currentResult.TargetLanguage, nextOption.TranslationId, cancellationToken);

        var updatedSlot = slot with { SelectedOptionIndex = nextIndex };
        var updatedSlots = currentResult.Slots
            .Select(s => s.TokenIndex == tokenIndex ? updatedSlot : s)
            .ToList();

        return currentResult with { Slots = updatedSlots };
    }
}

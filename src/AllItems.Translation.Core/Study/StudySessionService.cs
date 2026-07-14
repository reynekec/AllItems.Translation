using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Domain;

namespace AllItems.Translation.Core.Study;

public sealed class StudySessionService(
    IWordRepository wordRepository,
    IReviewStateRepository reviewStateRepository,
    ISpacedRepetitionScheduler scheduler,
    IClock clock) : IStudySessionService
{
    private const int LeechThreshold = 3;

    public async Task<IReadOnlyList<StudyCard>> BuildSessionAsync(
        Language sourceLanguage,
        Language targetLanguage,
        int maxCards,
        CancellationToken cancellationToken = default)
    {
        var words = await wordRepository.GetWordsWithPreferredTranslationAsync(sourceLanguage, targetLanguage, cancellationToken);
        if (words.Count == 0)
        {
            return [];
        }

        var states = await reviewStateRepository.GetStatesAsync(targetLanguage, words.Select(w => w.Id).ToList(), cancellationToken);
        var today = clock.UtcNow.Date;

        var due = new List<(WordEntry Word, WordReviewState State)>();
        var fresh = new List<(WordEntry Word, WordReviewState State)>();

        foreach (var word in words)
        {
            if (states.TryGetValue(word.Id, out var state))
            {
                if (state.DueDateUtc is null || state.DueDateUtc.Value.Date <= today)
                {
                    due.Add((word, state));
                }
            }
            else
            {
                fresh.Add((word, NewState(word.Id, targetLanguage)));
            }
        }

        return due
            .OrderBy(x => x.State.DueDateUtc)
            .Concat(fresh)
            .Take(maxCards)
            .Select(x => BuildCard(sourceLanguage, targetLanguage, x.Word, x.State))
            .ToList();
    }

    public async Task<IReadOnlyList<StudyCard>> BuildLeechSessionAsync(
        Language sourceLanguage,
        Language targetLanguage,
        int maxCards,
        CancellationToken cancellationToken = default)
    {
        var leeches = await reviewStateRepository.GetLeechesAsync(targetLanguage, LeechThreshold, cancellationToken);
        if (leeches.Count == 0)
        {
            return [];
        }

        var words = await wordRepository.GetWordsByIdsAsync(leeches.Select(l => l.WordEntryId).ToList(), targetLanguage, cancellationToken);
        var statesByWordId = leeches.ToDictionary(l => l.WordEntryId);

        return words
            .Where(w => w.Translations.Count > 0 && statesByWordId.ContainsKey(w.Id))
            .Take(maxCards)
            .Select(w => BuildCard(sourceLanguage, targetLanguage, w, statesByWordId[w.Id]))
            .ToList();
    }

    public async Task<IReadOnlyList<StudyCard>> BuildRetrainSessionAsync(
        Language sourceLanguage,
        Language targetLanguage,
        IReadOnlyList<int> wordEntryIds,
        int maxCards,
        CancellationToken cancellationToken = default)
    {
        if (wordEntryIds.Count == 0 || maxCards <= 0)
        {
            return [];
        }

        var requestedIds = wordEntryIds
            .Where(id => id > 0)
            .Distinct()
            .Take(maxCards)
            .ToList();

        if (requestedIds.Count == 0)
        {
            return [];
        }

        var words = await wordRepository.GetWordsByIdsAsync(requestedIds, targetLanguage, cancellationToken);
        if (words.Count == 0)
        {
            return [];
        }

        var wordById = words
            .Where(w => w.Translations.Count > 0)
            .ToDictionary(w => w.Id);

        if (wordById.Count == 0)
        {
            return [];
        }

        var states = await reviewStateRepository.GetStatesAsync(targetLanguage, wordById.Keys.ToList(), cancellationToken);
        var cards = new List<StudyCard>(requestedIds.Count);
        foreach (var id in requestedIds)
        {
            if (!wordById.TryGetValue(id, out var word))
            {
                continue;
            }

            var state = states.TryGetValue(id, out var existingState)
                ? existingState
                : NewState(id, targetLanguage);

            cards.Add(BuildCard(sourceLanguage, targetLanguage, word, state));
        }

        return cards;
    }

    public async Task<int> GetLeechCountAsync(
        Language targetLanguage,
        CancellationToken cancellationToken = default)
    {
        var leeches = await reviewStateRepository.GetLeechesAsync(targetLanguage, LeechThreshold, cancellationToken);
        return leeches.Count;
    }

    public async Task<int> GetRetrainCountAsync(
        Language sourceLanguage,
        Language targetLanguage,
        IReadOnlyList<int> wordEntryIds,
        CancellationToken cancellationToken = default)
    {
        var cards = await BuildRetrainSessionAsync(sourceLanguage, targetLanguage, wordEntryIds, int.MaxValue, cancellationToken);
        return cards.Count;
    }

    public async Task<int> GetAvailableWordCountAsync(
        Language sourceLanguage,
        Language targetLanguage,
        CancellationToken cancellationToken = default)
    {
        var words = await wordRepository.GetWordsWithPreferredTranslationAsync(sourceLanguage, targetLanguage, cancellationToken);
        return words.Count;
    }

    public async Task RecordAnswerAsync(StudyCard card, ReviewGrade grade, CancellationToken cancellationToken = default)
    {
        var updated = scheduler.Schedule(card.ReviewState, grade, clock.UtcNow);
        await reviewStateRepository.UpsertAsync(updated, cancellationToken);
    }

    private static StudyCard BuildCard(Language sourceLanguage, Language targetLanguage, WordEntry word, WordReviewState state) => new(
        word.Id,
        sourceLanguage,
        word.NormalizedText,
        word.Article,
        word.ExampleSentence,
        word.Highlights,
        targetLanguage,
        word.Translations[0].TargetText,
        word.Translations[0].ExampleSentence,
        word.Translations[0].Highlights,
        state);

    private static WordReviewState NewState(int wordEntryId, Language targetLanguage) => new()
    {
        WordEntryId = wordEntryId,
        TargetLanguage = targetLanguage
    };
}

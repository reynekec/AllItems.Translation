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
        int maxCards,
        CancellationToken cancellationToken = default)
    {
        if (maxCards <= 0)
        {
            return [];
        }

        var words = await wordRepository.GetWordsWithPreferredTranslationAsync(sourceLanguage, targetLanguage, cancellationToken);
        if (words.Count == 0)
        {
            return [];
        }

        var states = await reviewStateRepository.GetStatesAsync(targetLanguage, words.Select(w => w.Id).ToList(), cancellationToken);

        return words
            .Where(word => states.TryGetValue(word.Id, out var state) && state.LapseCount > 0)
            .Select(word => (Word: word, State: states[word.Id]))
            .OrderByDescending(x => x.State.LapseCount)
            .ThenBy(x => x.State.DueDateUtc ?? DateTime.MinValue)
            .ThenBy(x => x.Word.NormalizedText, StringComparer.Ordinal)
            .Take(maxCards)
            .Select(x => BuildCard(sourceLanguage, targetLanguage, x.Word, x.State))
            .ToList();
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
        CancellationToken cancellationToken = default)
    {
        var cards = await BuildRetrainSessionAsync(sourceLanguage, targetLanguage, int.MaxValue, cancellationToken);
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

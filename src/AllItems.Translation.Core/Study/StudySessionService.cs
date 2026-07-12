using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Domain;

namespace AllItems.Translation.Core.Study;

public sealed class StudySessionService(
    IWordRepository wordRepository,
    IReviewStateRepository reviewStateRepository,
    ISpacedRepetitionScheduler scheduler,
    IClock clock) : IStudySessionService
{
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
            .Select(x => new StudyCard(
                x.Word.Id,
                sourceLanguage,
                x.Word.NormalizedText,
                targetLanguage,
                x.Word.Translations[0].TargetText,
                x.State))
            .ToList();
    }

    public async Task RecordAnswerAsync(StudyCard card, ReviewGrade grade, CancellationToken cancellationToken = default)
    {
        var updated = scheduler.Schedule(card.ReviewState, grade, clock.UtcNow);
        await reviewStateRepository.UpsertAsync(updated, cancellationToken);
    }

    private static WordReviewState NewState(int wordEntryId, Language targetLanguage) => new()
    {
        WordEntryId = wordEntryId,
        TargetLanguage = targetLanguage
    };
}

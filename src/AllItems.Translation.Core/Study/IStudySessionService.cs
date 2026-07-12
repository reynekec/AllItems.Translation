using AllItems.Translation.Core.Domain;

namespace AllItems.Translation.Core.Study;

public interface IStudySessionService
{
    /// <summary>
    /// Builds a review session for one language pair: overdue cards first (most overdue first),
    /// then never-reviewed cards to fill up to <paramref name="maxCards"/>.
    /// </summary>
    Task<IReadOnlyList<StudyCard>> BuildSessionAsync(
        Language sourceLanguage,
        Language targetLanguage,
        int maxCards,
        CancellationToken cancellationToken = default);

    /// <summary>Grades a card and persists its next due date via the spaced-repetition scheduler.</summary>
    Task RecordAnswerAsync(StudyCard card, ReviewGrade grade, CancellationToken cancellationToken = default);
}

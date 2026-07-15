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

    /// <summary>Builds a session containing only words flagged as leeches (repeatedly missed), regardless of due date.</summary>
    Task<IReadOnlyList<StudyCard>> BuildLeechSessionAsync(
        Language sourceLanguage,
        Language targetLanguage,
        int maxCards,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Builds a retraining session from persisted historically missed cards for the given language pair.
    /// Cards are prioritized by weakness so the most repeatedly missed items appear first.
    /// </summary>
    Task<IReadOnlyList<StudyCard>> BuildRetrainSessionAsync(
        Language sourceLanguage,
        Language targetLanguage,
        int maxCards,
        CancellationToken cancellationToken = default);

    /// <summary>Returns the number of currently available leech/trouble cards for the target language.</summary>
    Task<int> GetLeechCountAsync(
        Language targetLanguage,
        CancellationToken cancellationToken = default);

    /// <summary>Returns how many historically missed cards are available for retraining in this language pair.</summary>
    Task<int> GetRetrainCountAsync(
        Language sourceLanguage,
        Language targetLanguage,
        CancellationToken cancellationToken = default);

    /// <summary>Returns the total number of words available for this source-target study pair.</summary>
    Task<int> GetAvailableWordCountAsync(
        Language sourceLanguage,
        Language targetLanguage,
        CancellationToken cancellationToken = default);

    /// <summary>Grades a card and persists its next due date via the spaced-repetition scheduler.</summary>
    Task RecordAnswerAsync(StudyCard card, ReviewGrade grade, CancellationToken cancellationToken = default);
}

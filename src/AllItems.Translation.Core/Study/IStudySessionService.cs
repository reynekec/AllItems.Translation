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
    /// Builds a retraining session from an explicit list of card word IDs, preserving the provided order.
    /// Missing words or words without a preferred translation for <paramref name="targetLanguage"/> are skipped.
    /// </summary>
    Task<IReadOnlyList<StudyCard>> BuildRetrainSessionAsync(
        Language sourceLanguage,
        Language targetLanguage,
        IReadOnlyList<int> wordEntryIds,
        int maxCards,
        CancellationToken cancellationToken = default);

    /// <summary>Returns the number of currently available leech/trouble cards for the target language.</summary>
    Task<int> GetLeechCountAsync(
        Language targetLanguage,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns how many requested retrain card IDs currently resolve to study cards for the language pair.
    /// </summary>
    Task<int> GetRetrainCountAsync(
        Language sourceLanguage,
        Language targetLanguage,
        IReadOnlyList<int> wordEntryIds,
        CancellationToken cancellationToken = default);

    /// <summary>Returns the total number of words available for this source-target study pair.</summary>
    Task<int> GetAvailableWordCountAsync(
        Language sourceLanguage,
        Language targetLanguage,
        CancellationToken cancellationToken = default);

    /// <summary>Grades a card and persists its next due date via the spaced-repetition scheduler.</summary>
    Task RecordAnswerAsync(StudyCard card, ReviewGrade grade, CancellationToken cancellationToken = default);
}

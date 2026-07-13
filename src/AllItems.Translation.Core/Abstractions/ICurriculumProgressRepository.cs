using AllItems.Translation.Core.Curriculum;

namespace AllItems.Translation.Core.Abstractions;

public interface ICurriculumProgressRepository
{
    /// <summary>Of the given exercise IDs, which have been answered correctly at least once.</summary>
    Task<IReadOnlySet<string>> GetCompletedExerciseIdsAsync(IReadOnlyCollection<string> exerciseIds, CancellationToken cancellationToken = default);

    Task<IReadOnlyDictionary<string, CurriculumExerciseReviewState>> GetReviewStatesAsync(
        IReadOnlyCollection<string> exerciseIds,
        CancellationToken cancellationToken = default);

    Task<IReadOnlySet<string>> GetAttemptedExerciseIdsAsync(CancellationToken cancellationToken = default);

    Task UpsertReviewStateAsync(CurriculumExerciseReviewState state, CancellationToken cancellationToken = default);

    Task MarkExerciseCompletedAsync(string exerciseId, CancellationToken cancellationToken = default);

    Task RecordAttemptAsync(string exerciseId, bool isCorrect, CancellationToken cancellationToken = default);
}

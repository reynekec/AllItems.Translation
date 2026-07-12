namespace AllItems.Translation.Core.Abstractions;

public interface ICurriculumProgressRepository
{
    /// <summary>Of the given exercise IDs, which have been answered correctly at least once.</summary>
    Task<IReadOnlySet<string>> GetCompletedExerciseIdsAsync(IReadOnlyCollection<string> exerciseIds, CancellationToken cancellationToken = default);

    Task MarkExerciseCompletedAsync(string exerciseId, CancellationToken cancellationToken = default);
}

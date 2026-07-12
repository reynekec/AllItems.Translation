namespace AllItems.Translation.Core.Curriculum;

public interface ICurriculumService
{
    /// <summary>
    /// One summary per CEFR level, in order. A level unlocks only once the previous level has
    /// authored content and every exercise in it has been completed - an empty (not-yet-authored)
    /// level never unlocks the next one.
    /// </summary>
    Task<IReadOnlyList<LevelSummary>> GetLevelSummariesAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<UnitSummary>> GetUnitSummariesAsync(CefrLevel level, CancellationToken cancellationToken = default);

    /// <summary>Grades the answer and, if correct, records the exercise as completed.</summary>
    Task<GradingResult> SubmitAnswerAsync(Exercise exercise, ExerciseAnswer answer, CancellationToken cancellationToken = default);
}

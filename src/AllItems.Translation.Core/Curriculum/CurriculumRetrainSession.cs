namespace AllItems.Translation.Core.Curriculum;

public sealed record CurriculumRetrainSession(
    IReadOnlyList<CurriculumRetrainExercise> Exercises,
    int DueExerciseCount,
    int TotalAttemptedExerciseCount);
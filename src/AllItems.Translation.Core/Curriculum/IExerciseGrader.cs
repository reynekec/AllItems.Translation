namespace AllItems.Translation.Core.Curriculum;

/// <summary>Pure grading logic - no I/O, so the scoring rules for every exercise type are directly unit-testable.</summary>
public interface IExerciseGrader
{
    GradingResult Grade(Exercise exercise, ExerciseAnswer answer);
}

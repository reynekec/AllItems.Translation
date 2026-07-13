namespace AllItems.Translation.Core.Curriculum;

public sealed class CurriculumExerciseReviewState
{
    public string ExerciseId { get; set; } = string.Empty;
    public double EasinessFactor { get; set; } = 2.5;
    public int IntervalDays { get; set; }
    public int Repetitions { get; set; }
    public int LapseCount { get; set; }
    public int CorrectAttempts { get; set; }
    public int IncorrectAttempts { get; set; }
    public DateTime? DueDateUtc { get; set; }
    public DateTime? LastReviewedUtc { get; set; }
}
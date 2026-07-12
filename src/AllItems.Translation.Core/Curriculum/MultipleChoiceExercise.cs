namespace AllItems.Translation.Core.Curriculum;

/// <summary>Pick the correct option - lowest-pressure format, recognition rather than recall.</summary>
public sealed record MultipleChoiceExercise : Exercise
{
    public required string Question { get; init; }
    public required IReadOnlyList<string> Options { get; init; }
    public required int CorrectOptionIndex { get; init; }
}

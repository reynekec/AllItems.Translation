namespace AllItems.Translation.Core.Curriculum;

/// <summary>Arrange scrambled words into correct German word order.</summary>
public sealed record SentenceOrderExercise : Exercise
{
    public required IReadOnlyList<string> ScrambledWords { get; init; }
    public required IReadOnlyList<string> CorrectOrder { get; init; }
}

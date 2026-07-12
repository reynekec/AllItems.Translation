namespace AllItems.Translation.Core.Curriculum;

/// <summary>A sentence with one blank to type in - the core grammar-practice format at every level.</summary>
public sealed record ClozeExercise : Exercise
{
    public required string TextBefore { get; init; }
    public required string TextAfter { get; init; }
    public required string CorrectAnswer { get; init; }
}

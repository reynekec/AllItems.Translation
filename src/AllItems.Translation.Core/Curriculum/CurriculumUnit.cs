namespace AllItems.Translation.Core.Curriculum;

/// <summary>A themed group of exercises within one CEFR level (e.g. "A1 - sein und haben").</summary>
public sealed record CurriculumUnit
{
    public required string Id { get; init; }
    public required CefrLevel Level { get; init; }
    public required int SortOrder { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required IReadOnlyList<Exercise> Exercises { get; init; }
}

namespace AllItems.Translation.Core.Curriculum;

/// <summary>
/// One practice item within a <see cref="CurriculumUnit"/>. Concrete shape (multiple choice, cloze,
/// sentence order, conjugation drill) is a subtype - the framework is meant to grow more of these
/// over time without touching this base type.
/// </summary>
public abstract record Exercise
{
    /// <summary>Stable across app updates - content lives in code, not the database, so this is what progress tracking keys on.</summary>
    public required string Id { get; init; }

    public required string Instruction { get; init; }

    /// <summary>Shown after answering, right or wrong - the gentle "here's why" explanation.</summary>
    public required string Explanation { get; init; }
}

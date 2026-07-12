namespace AllItems.Translation.Core.Curriculum;

/// <summary>One labeled slot in a <see cref="ConjugationDrillExercise"/>, e.g. Label "ich", CorrectForm "bin".</summary>
public sealed record ConjugationSlot(string Label, string CorrectForm);

/// <summary>Fill in every form of a verb or article across a fixed set of slots (persons, cases, etc.).</summary>
public sealed record ConjugationDrillExercise : Exercise
{
    public required string BaseWord { get; init; }
    public required IReadOnlyList<ConjugationSlot> Slots { get; init; }
}

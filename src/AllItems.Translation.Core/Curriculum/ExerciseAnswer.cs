namespace AllItems.Translation.Core.Curriculum;

/// <summary>
/// The user's response to an exercise. Only the field matching the exercise's type is populated -
/// this shape is deliberately a flat "one of" rather than a class hierarchy so the UI layer can
/// build it without knowing grading details.
/// </summary>
public sealed record ExerciseAnswer(
    int? SelectedOptionIndex = null,
    string? TypedText = null,
    IReadOnlyList<string>? WordOrder = null,
    IReadOnlyDictionary<string, string>? ConjugationAnswers = null);

namespace AllItems.Translation.Core.Curriculum;

/// <summary>
/// Grades typed answers leniently (trimmed, case-insensitive) - a stray Shift key or extra space
/// shouldn't read as "wrong" for someone already nervous about getting German right.
/// </summary>
public sealed class ExerciseGrader : IExerciseGrader
{
    public GradingResult Grade(Exercise exercise, ExerciseAnswer answer) => exercise switch
    {
        MultipleChoiceExercise mc => GradeMultipleChoice(mc, answer),
        ClozeExercise cloze => GradeCloze(cloze, answer),
        SentenceOrderExercise order => GradeSentenceOrder(order, answer),
        ConjugationDrillExercise drill => GradeConjugationDrill(drill, answer),
        _ => throw new NotSupportedException($"No grading logic registered for exercise type '{exercise.GetType().Name}'.")
    };

    private static GradingResult GradeMultipleChoice(MultipleChoiceExercise exercise, ExerciseAnswer answer)
    {
        var isCorrect = answer.SelectedOptionIndex == exercise.CorrectOptionIndex;
        return new GradingResult(isCorrect, exercise.Explanation);
    }

    private static GradingResult GradeCloze(ClozeExercise exercise, ExerciseAnswer answer)
    {
        var acceptedAnswers = exercise.AcceptedAnswers.Count == 0
            ? new[] { exercise.CorrectAnswer }
            : new[] { exercise.CorrectAnswer }.Concat(exercise.AcceptedAnswers);

        var isCorrect = acceptedAnswers.Any(expected => TextsMatch(answer.TypedText, expected));
        return new GradingResult(isCorrect, exercise.Explanation);
    }

    private static GradingResult GradeSentenceOrder(SentenceOrderExercise exercise, ExerciseAnswer answer)
    {
        var chosen = answer.WordOrder ?? [];
        var isCorrect = chosen.Count == exercise.CorrectOrder.Count
            && chosen.Select((word, index) => word.Trim() == exercise.CorrectOrder[index]).All(match => match);

        return new GradingResult(isCorrect, exercise.Explanation);
    }

    private static GradingResult GradeConjugationDrill(ConjugationDrillExercise exercise, ExerciseAnswer answer)
    {
        var typedAnswers = answer.ConjugationAnswers ?? new Dictionary<string, string>();
        var slotCorrectness = exercise.Slots.ToDictionary(
            slot => slot.Label,
            slot => typedAnswers.TryGetValue(slot.Label, out var typed) && TextsMatch(typed, slot.CorrectForm));

        var isCorrect = slotCorrectness.Values.All(correct => correct);
        return new GradingResult(isCorrect, exercise.Explanation, slotCorrectness);
    }

    private static bool TextsMatch(string? typed, string expected) =>
        string.Equals(Normalize(typed), Normalize(expected), StringComparison.OrdinalIgnoreCase);

    private static string Normalize(string? text) =>
        string.Join(' ', (text ?? string.Empty).Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
}

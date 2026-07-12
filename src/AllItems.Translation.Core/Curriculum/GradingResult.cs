namespace AllItems.Translation.Core.Curriculum;

/// <summary>
/// Result of grading one answer. <see cref="SlotCorrectness"/> is only populated for conjugation
/// drills, so a wrong answer there can highlight just the slots that need another try.
/// </summary>
public sealed record GradingResult(
    bool IsCorrect,
    string Explanation,
    IReadOnlyDictionary<string, bool>? SlotCorrectness = null);

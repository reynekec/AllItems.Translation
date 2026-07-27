namespace AllItems.Translation.Core.Study;

/// <summary>
/// Self-assessed recall quality after seeing a flashcard's answer, using SM-2's native 0-5 scale.
/// Grades below 3 are treated as lapses (the card resets and its lapse count increases).
/// </summary>
public enum ReviewGrade
{
    /// <summary>Complete blackout; no recollection at all.</summary>
    Blackout = 0,

    /// <summary>Incorrect, but the answer felt familiar once revealed.</summary>
    Incorrect = 1,

    /// <summary>Incorrect, yet the correct answer seemed easy to recall.</summary>
    Familiar = 2,

    /// <summary>Correct, but recalled with serious difficulty.</summary>
    Hard = 3,

    /// <summary>Correct after some hesitation.</summary>
    Good = 4,

    /// <summary>Perfect, effortless recall.</summary>
    Easy = 5
}

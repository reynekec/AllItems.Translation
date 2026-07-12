namespace AllItems.Translation.Core.Domain;

/// <summary>
/// SM-2 spaced-repetition state for one (word, target language) pair. A word can be well-known
/// in Afrikaans but still shaky in English, so each target language schedules independently.
/// </summary>
public class WordReviewState
{
    public int Id { get; set; }
    public int WordEntryId { get; set; }
    public Language TargetLanguage { get; set; }

    public double EasinessFactor { get; set; } = 2.5;
    public int IntervalDays { get; set; }
    public int Repetitions { get; set; }

    /// <summary>Null means "never reviewed" - a brand-new card.</summary>
    public DateTime? DueDateUtc { get; set; }
    public DateTime? LastReviewedUtc { get; set; }
}

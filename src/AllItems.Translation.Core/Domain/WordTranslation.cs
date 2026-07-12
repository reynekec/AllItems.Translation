namespace AllItems.Translation.Core.Domain;

/// <summary>
/// One candidate meaning of a <see cref="WordEntry"/> in a specific target language.
/// A word can accumulate several of these (homonyms/context-dependent meanings) over time.
/// </summary>
public class WordTranslation
{
    public int Id { get; set; }
    public int WordEntryId { get; set; }
    public WordEntry? WordEntry { get; set; }

    public Language TargetLanguage { get; set; }
    public required string TargetText { get; set; }

    /// <summary>True for the meaning the user last confirmed/clicked, or the best guess if never overridden.</summary>
    public bool IsPreferred { get; set; }

    public int UsageCount { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}

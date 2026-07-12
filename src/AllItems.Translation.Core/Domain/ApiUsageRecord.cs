namespace AllItems.Translation.Core.Domain;

/// <summary>Running character-usage counter for the translation API, one row per calendar month.</summary>
public class ApiUsageRecord
{
    public int Id { get; set; }

    /// <summary>Format: "yyyy-MM", in UTC.</summary>
    public required string YearMonth { get; set; }

    public long CharacterCount { get; set; }
}

namespace AllItems.Translation.Core.Domain;

/// <summary>
/// A cached fluent (whole-sentence) translation, used both to avoid repeat API calls
/// and as the source of context for picking the right word meaning on first encounter.
/// </summary>
public class SentenceTranslation
{
    public int Id { get; set; }
    public Language SourceLanguage { get; set; }
    public Language TargetLanguage { get; set; }
    public required string NormalizedSourceText { get; set; }
    public required string TranslatedText { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}

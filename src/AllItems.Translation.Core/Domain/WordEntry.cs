namespace AllItems.Translation.Core.Domain;

/// <summary>
/// A single distinct source-language word, keyed by its lowercase-invariant form.
/// One entry accumulates multiple <see cref="WordTranslation"/> candidates per target language
/// as it is encountered in different sentence contexts over time.
/// </summary>
public class WordEntry
{
    public int Id { get; set; }
    public Language Language { get; set; }
    public required string NormalizedText { get; set; }

    public List<WordTranslation> Translations { get; set; } = [];
}

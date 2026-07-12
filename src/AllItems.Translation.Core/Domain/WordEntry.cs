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

    /// <summary>"der"/"die"/"das" for a German noun, null otherwise.</summary>
    public string? Article { get; set; }

    /// <summary>A short (aim for &lt;= 5 words) example sentence using this word, or null if not yet authored.</summary>
    public string? ExampleSentence { get; set; }

    /// <summary>Words within <see cref="ExampleSentence"/> whose form changed for a grammatical reason, with an explanation.</summary>
    public List<SentenceHighlight> Highlights { get; set; } = [];

    public List<WordTranslation> Translations { get; set; } = [];
}

using AllItems.Translation.Core.Domain;

namespace AllItems.Translation.Core.Translation;

/// <summary>
/// The full result for one destination language: the word-by-word reassembled sentence
/// (as clickable slots) plus Google's fluent reference translation for comparison.
/// </summary>
public sealed record SentenceTranslationResult(
    Language SourceLanguage,
    Language TargetLanguage,
    string SourceSentence,
    IReadOnlyList<TranslatedSlot> Slots,
    string ReferenceTranslation)
{
    public string ReassembledText => string.Concat(Slots.Select(s => s.DisplayText));
}

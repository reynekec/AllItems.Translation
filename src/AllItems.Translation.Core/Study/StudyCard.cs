using AllItems.Translation.Core.Domain;

namespace AllItems.Translation.Core.Study;

/// <summary>One flashcard: a source word to recall, and the target-language meaning as the answer.</summary>
public sealed record StudyCard(
    int WordEntryId,
    Language SourceLanguage,
    string FrontText,
    string? Article,
    string? ExampleSentence,
    IReadOnlyList<SentenceHighlight> Highlights,
    Language TargetLanguage,
    string BackText,
    WordReviewState ReviewState);

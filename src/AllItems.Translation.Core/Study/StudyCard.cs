using AllItems.Translation.Core.Domain;

namespace AllItems.Translation.Core.Study;

/// <summary>
/// One flashcard: a source word to recall, and the target-language meaning as the answer.
/// The front carries the source-language example sentence; the back carries the destination-language
/// one, so the sentence always reads in the language of the side it sits on.
/// </summary>
public sealed record StudyCard(
    int WordEntryId,
    Language SourceLanguage,
    string FrontText,
    string? Article,
    string? ExampleSentence,
    IReadOnlyList<SentenceHighlight> Highlights,
    Language TargetLanguage,
    string BackText,
    string? BackExampleSentence,
    IReadOnlyList<SentenceHighlight> BackHighlights,
    WordReviewState ReviewState);

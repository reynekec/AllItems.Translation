using AllItems.Translation.Core.Domain;

namespace AllItems.Translation.Core.Study;

/// <summary>
/// One flashcard: a source word to recall, and the target-language meaning as the answer.
/// <see cref="Article"/>, <see cref="ExampleSentence"/>, and <see cref="Highlights"/> are always German-language
/// content: when <see cref="IsGermanFront"/> is false (studying "German" from a non-German source), they
/// describe the answer, not the prompt, and a UI must not reveal them until after the answer is shown.
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
    WordReviewState ReviewState,
    bool IsGermanFront);

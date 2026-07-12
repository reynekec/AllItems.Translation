using AllItems.Translation.Core.Domain;

namespace AllItems.Translation.Core.Curriculum;

/// <summary>One seed word for the bulk vocabulary import, distinct from Training's hand-authored exercises.</summary>
/// <remarks>
/// <see cref="ExampleSentence"/>/<see cref="Highlights"/> are the German content; the two English
/// counterparts are the natural English translation of that sentence and its highlights, so a card
/// can show the sentence in whichever language sits on that side (source on the front, destination
/// on the back).
/// </remarks>
public sealed record VocabularyWord(
    string German,
    string English,
    string? Article = null,
    string? ExampleSentence = null,
    IReadOnlyList<SentenceHighlight>? Highlights = null,
    string? EnglishExampleSentence = null,
    IReadOnlyList<SentenceHighlight>? EnglishHighlights = null);

using AllItems.Translation.Core.Domain;

namespace AllItems.Translation.Core.Curriculum;

/// <summary>
/// Marks an exercise as the primary teaching moment for a specific word, so a correct answer can
/// feed it into the shared Dictionary/Flashcards system instead of staying isolated inside Training.
/// Only set on one exercise per distinct word within a unit - reinforcement exercises for a word
/// already tagged elsewhere leave this null to avoid duplicate dictionary entries.
/// </summary>
public sealed record VocabularyTeaching(
    string GermanWord,
    string EnglishMeaning,
    string? Article = null,
    string? ExampleSentence = null,
    IReadOnlyList<SentenceHighlight>? Highlights = null);

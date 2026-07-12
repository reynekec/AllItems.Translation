namespace AllItems.Translation.App.ViewModels;

/// <summary>One word within a flashcard's example sentence, flagged if its form changed for a grammatical reason.</summary>
public sealed record SentenceWordViewModel(string Text, bool IsHighlighted, string? Reason);

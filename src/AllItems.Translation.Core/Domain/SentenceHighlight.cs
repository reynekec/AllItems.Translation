namespace AllItems.Translation.Core.Domain;

/// <summary>One word within a <see cref="WordEntry.ExampleSentence"/> whose form differs from the dictionary form for a grammatical reason (tense, mood, case), plus a human-readable explanation.</summary>
public sealed record SentenceHighlight(string Word, string Reason);

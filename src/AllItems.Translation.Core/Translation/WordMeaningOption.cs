namespace AllItems.Translation.Core.Translation;

/// <summary>One candidate meaning offered for a translated word slot, in click-to-cycle order.</summary>
public sealed record WordMeaningOption(int TranslationId, string Text);

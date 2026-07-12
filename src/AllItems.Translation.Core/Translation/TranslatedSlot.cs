namespace AllItems.Translation.Core.Translation;

/// <summary>
/// One reassembled piece of the destination sentence: either a translated word (clickable,
/// with alternate meanings to cycle through) or a verbatim separator (space/punctuation).
/// </summary>
public sealed record TranslatedSlot(
    int TokenIndex,
    bool IsWord,
    string SourceText,
    int? WordEntryId,
    IReadOnlyList<WordMeaningOption> Options,
    int SelectedOptionIndex)
{
    public string DisplayText => IsWord && Options.Count > 0
        ? Options[SelectedOptionIndex].Text
        : SourceText;
}

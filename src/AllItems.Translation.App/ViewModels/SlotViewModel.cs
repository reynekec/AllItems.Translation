using AllItems.Translation.Core.Translation;

namespace AllItems.Translation.App.ViewModels;

/// <summary>Read-only UI wrapper around one <see cref="TranslatedSlot"/> for data binding.</summary>
public sealed class SlotViewModel(TranslatedSlot slot)
{
    public int TokenIndex => slot.TokenIndex;
    public bool IsWord => slot.IsWord;
    public string DisplayText => slot.DisplayText;
    public bool IsClickable => slot.IsWord && slot.Options.Count > 1;
}

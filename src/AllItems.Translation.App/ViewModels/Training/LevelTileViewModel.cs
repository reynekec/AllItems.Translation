using AllItems.Translation.Core.Curriculum;

namespace AllItems.Translation.App.ViewModels.Training;

public sealed class LevelTileViewModel(LevelSummary summary)
{
    public CefrLevel Level => summary.Level;
    public bool IsUnlocked => summary.IsUnlocked;
    public bool HasContent => summary.TotalUnitCount > 0;
    public bool IsClickable => IsUnlocked && HasContent;

    public string StatusText => (IsUnlocked, HasContent) switch
    {
        (false, _) => "Locked - finish the previous level first",
        (true, false) => "Coming soon",
        (true, true) => $"{summary.CompletedUnitCount} / {summary.TotalUnitCount} units complete"
    };
}

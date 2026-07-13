using AllItems.Translation.Core.Curriculum;

namespace AllItems.Translation.App.ViewModels.Training;

public sealed class UnitTileViewModel(UnitSummary summary)
{
    public CurriculumUnit Unit => summary.Unit;
    public string Title => Unit.Title;
    public string Description => Unit.Description;
    public int CompletedExerciseCount => summary.CompletedExerciseCount;
    public int TotalExerciseCount => summary.TotalExerciseCount;
    public bool IsComplete => summary.CompletedExerciseCount == summary.TotalExerciseCount;
    public string ProgressText => $"{summary.CompletedExerciseCount} / {summary.TotalExerciseCount}";
}

using AllItems.Translation.Core.Curriculum;

namespace AllItems.Translation.App.ViewModels.Training;

public sealed class TeachingAreaGuideExampleViewModel(TeachingAreaGuideExample example)
{
    public string German => example.German;
    public string English => example.English;
    public string? Note => example.Note;
    public bool HasNote => !string.IsNullOrWhiteSpace(example.Note);
}
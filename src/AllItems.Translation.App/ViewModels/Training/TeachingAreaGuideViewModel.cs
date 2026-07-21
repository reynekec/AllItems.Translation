using System.Collections.ObjectModel;
using AllItems.Translation.Core.Curriculum;

namespace AllItems.Translation.App.ViewModels.Training;

public sealed class TeachingAreaGuideViewModel
{
    public TeachingAreaGuideViewModel(CurriculumUnit unit)
    {
        UnitId = unit.Id;
        Title = unit.Title;
        Summary = unit.Description;
        Overview = unit.Guide?.Overview ?? unit.Description;
        WhyItMatters = unit.Guide?.WhyItMatters ?? "This teaching area builds an important pattern you will meet repeatedly in real German.";
        WhatToPractice = unit.Guide?.WhatToPractice ?? "Look for the repeated pattern in the exercises and focus on how it changes meaning.";
        Examples = new ObservableCollection<TeachingAreaGuideExampleViewModel>(
            (unit.Guide?.Examples ?? [])
            .Select(example => new TeachingAreaGuideExampleViewModel(example)));
    }

    public string UnitId { get; }
    public string Title { get; }
    public string Summary { get; }
    public string Overview { get; }
    public string WhyItMatters { get; }
    public string WhatToPractice { get; }
    public ObservableCollection<TeachingAreaGuideExampleViewModel> Examples { get; }
}
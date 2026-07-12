using System.Collections.ObjectModel;
using AllItems.Translation.Core.Curriculum;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AllItems.Translation.App.ViewModels.Training;

public sealed partial class MultipleChoiceOptionViewModel(int index, string text) : ObservableObject
{
    public int Index { get; } = index;
    public string Text { get; } = text;

    [ObservableProperty]
    private bool isSelected;
}

public sealed partial class MultipleChoiceExerciseViewModel : ExerciseViewModel
{
    private readonly MultipleChoiceExercise _exercise;

    public string Question => _exercise.Question;
    public ObservableCollection<MultipleChoiceOptionViewModel> Options { get; }

    public int SelectedOptionIndex { get; private set; } = -1;

    public MultipleChoiceExerciseViewModel(MultipleChoiceExercise exercise) : base(exercise)
    {
        _exercise = exercise;
        Options = new ObservableCollection<MultipleChoiceOptionViewModel>(
            exercise.Options.Select((text, index) => new MultipleChoiceOptionViewModel(index, text)));
    }

    [RelayCommand]
    private void SelectOption(MultipleChoiceOptionViewModel? option)
    {
        if (option is null)
        {
            return;
        }

        foreach (var candidate in Options)
        {
            candidate.IsSelected = false;
        }

        option.IsSelected = true;
        SelectedOptionIndex = option.Index;
    }

    public override ExerciseAnswer BuildAnswer() => new(SelectedOptionIndex: SelectedOptionIndex);

    public override void Reset()
    {
        base.Reset();
        foreach (var option in Options)
        {
            option.IsSelected = false;
        }

        SelectedOptionIndex = -1;
    }
}

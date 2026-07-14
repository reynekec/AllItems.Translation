using System.Collections.ObjectModel;
using AllItems.Translation.Core.Curriculum;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AllItems.Translation.App.ViewModels.Training;

public enum MultipleChoiceOptionVisualState
{
    Neutral,
    Selected,
    Correct,
    Incorrect
}

public sealed partial class MultipleChoiceOptionViewModel(int index, string text) : ObservableObject
{
    public int Index { get; } = index;
    public string Text { get; } = text;

    [ObservableProperty]
    private bool isSelected;

    [ObservableProperty]
    private MultipleChoiceOptionVisualState visualState;
}

public sealed partial class MultipleChoiceExerciseViewModel : ExerciseViewModel
{
    private readonly MultipleChoiceExercise _exercise;

    public string Question => _exercise.Question;
    public ObservableCollection<MultipleChoiceOptionViewModel> Options { get; }

    public int SelectedOptionIndex { get; private set; } = -1;
    public override bool RequiresExplicitCheck => false;

    public MultipleChoiceExerciseViewModel(MultipleChoiceExercise exercise) : base(exercise)
    {
        _exercise = exercise;
        var shuffled = exercise.Options
            .Select((text, index) => new MultipleChoiceOptionViewModel(index, text))
            .OrderBy(_ => Random.Shared.Next())
            .ToList();
        Options = new ObservableCollection<MultipleChoiceOptionViewModel>(shuffled);
    }

    [RelayCommand]
    private void SelectOption(MultipleChoiceOptionViewModel? option)
    {
        if (option is null || IsAnswered)
        {
            return;
        }

        foreach (var candidate in Options)
        {
            candidate.IsSelected = false;
            candidate.VisualState = MultipleChoiceOptionVisualState.Neutral;
        }

        option.IsSelected = true;
        option.VisualState = MultipleChoiceOptionVisualState.Selected;
        SelectedOptionIndex = option.Index;

        // Multiple-choice answers are checked immediately after selection.
        if (SubmitAnswerAsync is not null)
        {
            _ = SubmitAnswerAsync();
        }
    }

    public void ApplyIncorrectAnswerHighlights()
    {
        foreach (var option in Options)
        {
            if (option.Index == _exercise.CorrectOptionIndex)
            {
                option.VisualState = MultipleChoiceOptionVisualState.Correct;
                continue;
            }

            if (option.Index == SelectedOptionIndex)
            {
                option.VisualState = MultipleChoiceOptionVisualState.Incorrect;
                continue;
            }

            option.VisualState = MultipleChoiceOptionVisualState.Neutral;
        }
    }

    public override ExerciseAnswer BuildAnswer() => new(SelectedOptionIndex: SelectedOptionIndex);

    public override void Reset()
    {
        base.Reset();
        foreach (var option in Options)
        {
            option.IsSelected = false;
            option.VisualState = MultipleChoiceOptionVisualState.Neutral;
        }

        SelectedOptionIndex = -1;
    }
}

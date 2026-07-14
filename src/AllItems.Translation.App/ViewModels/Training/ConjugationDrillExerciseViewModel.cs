using System.Collections.ObjectModel;
using AllItems.Translation.Core.Curriculum;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AllItems.Translation.App.ViewModels.Training;

public sealed partial class ConjugationSlotAnswerViewModel(string label, string correctForm) : ObservableObject
{
    public string Label { get; } = label;
    public string CorrectForm { get; } = correctForm;

    [ObservableProperty]
    private string typedForm = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowCorrectAnswer))]
    private bool? isCorrect;

    public bool ShowCorrectAnswer => IsCorrect == false;
}

public sealed partial class ConjugationDrillExerciseViewModel : ExerciseViewModel
{
    private readonly ConjugationDrillExercise _exercise;

    public string BaseWord => _exercise.BaseWord;
    public ObservableCollection<ConjugationSlotAnswerViewModel> Slots { get; }

    public ConjugationDrillExerciseViewModel(ConjugationDrillExercise exercise) : base(exercise)
    {
        _exercise = exercise;
        Slots = new ObservableCollection<ConjugationSlotAnswerViewModel>(
            exercise.Slots.Select(slot => new ConjugationSlotAnswerViewModel(slot.Label, slot.CorrectForm)));
    }

    public override ExerciseAnswer BuildAnswer() => new(
        ConjugationAnswers: Slots.ToDictionary(slot => slot.Label, slot => slot.TypedForm));

    public void ApplySlotCorrectness(IReadOnlyDictionary<string, bool>? slotCorrectness)
    {
        if (slotCorrectness is null)
        {
            return;
        }

        foreach (var slot in Slots)
        {
            slot.IsCorrect = slotCorrectness.TryGetValue(slot.Label, out var correct) ? correct : null;
        }
    }

    public override void Reset()
    {
        base.Reset();
        foreach (var slot in Slots)
        {
            if (slot.IsCorrect != true)
            {
                slot.TypedForm = string.Empty;
            }

            slot.IsCorrect = null;
        }
    }
}

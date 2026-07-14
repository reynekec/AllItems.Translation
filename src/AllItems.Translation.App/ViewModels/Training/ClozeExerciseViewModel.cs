using AllItems.Translation.Core.Curriculum;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AllItems.Translation.App.ViewModels.Training;

public sealed partial class ClozeExerciseViewModel : ExerciseViewModel
{
    private readonly ClozeExercise _exercise;
    private const string FallbackEnglishHint = "Use the English meaning implied by the sentence.";

    public string TextBefore => _exercise.TextBefore;
    public string TextAfter => _exercise.TextAfter;
    public string EnglishHint => _exercise.Teaches?.EnglishMeaning ?? FallbackEnglishHint;
    public string HintHelperText => "This hint gives an English clue only; it does not reveal the German answer.";

    [ObservableProperty]
    private string typedAnswer = string.Empty;

    [ObservableProperty]
    private bool isHintVisible;

    public ClozeExerciseViewModel(ClozeExercise exercise) : base(exercise)
    {
        _exercise = exercise;
    }

    public override ExerciseAnswer BuildAnswer() => new(TypedText: TypedAnswer);

    [RelayCommand]
    private void RevealHint() => IsHintVisible = true;

    public override void Reset()
    {
        base.Reset();
        TypedAnswer = string.Empty;
        IsHintVisible = false;
    }
}

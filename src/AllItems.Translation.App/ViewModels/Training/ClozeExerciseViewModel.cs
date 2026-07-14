using AllItems.Translation.Core.Curriculum;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AllItems.Translation.App.ViewModels.Training;

public sealed partial class ClozeExerciseViewModel : ExerciseViewModel
{
    private readonly ClozeExercise _exercise;

    public string TextBefore => _exercise.TextBefore;
    public string TextAfter => _exercise.TextAfter;
    public string EnglishHint => ClozeEnglishHintProvider.GetHint(_exercise);
    public string HintHelperText => "This hint gives an English clue only; it does not reveal the German answer.";

    [ObservableProperty]
    private string typedAnswer = string.Empty;

    public ClozeExerciseViewModel(ClozeExercise exercise) : base(exercise)
    {
        _exercise = exercise;
    }

    public override ExerciseAnswer BuildAnswer() => new(TypedText: TypedAnswer);

    public override void Reset()
    {
        base.Reset();
        TypedAnswer = string.Empty;
    }
}

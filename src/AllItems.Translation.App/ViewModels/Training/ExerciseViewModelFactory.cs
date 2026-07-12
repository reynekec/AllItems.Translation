using AllItems.Translation.Core.Curriculum;

namespace AllItems.Translation.App.ViewModels.Training;

public static class ExerciseViewModelFactory
{
    public static ExerciseViewModel Create(Exercise exercise) => exercise switch
    {
        MultipleChoiceExercise mc => new MultipleChoiceExerciseViewModel(mc),
        ClozeExercise cloze => new ClozeExerciseViewModel(cloze),
        SentenceOrderExercise order => new SentenceOrderExerciseViewModel(order),
        ConjugationDrillExercise drill => new ConjugationDrillExerciseViewModel(drill),
        _ => throw new NotSupportedException($"No view model registered for exercise type '{exercise.GetType().Name}'.")
    };
}

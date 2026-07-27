using AllItems.Translation.Core.Curriculum;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Linq;

namespace AllItems.Translation.App.ViewModels.Training;

/// <summary>
/// UI-facing wrapper around one <see cref="Exercise"/>. A subtype exists per exercise type; the
/// window renders the right one via DataTemplate selection on the concrete type.
/// </summary>
public abstract partial class ExerciseViewModel(Exercise exercise) : ObservableObject
{
    public Exercise Exercise { get; } = exercise;
    public string Instruction => Exercise.Instruction;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowCheckAnswerButton))]
    private bool isAnswered;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowCorrectFeedback))]
    [NotifyPropertyChangedFor(nameof(ShowIncorrectFeedback))]
    private bool? isCorrect;

    [ObservableProperty]
    private string? explanation;

    public bool ShowCorrectFeedback => IsCorrect == true;
    public bool ShowIncorrectFeedback => IsCorrect == false;
    public string CorrectAnswerDisplay => Exercise switch
    {
        ClozeExercise cloze => cloze.CorrectAnswer,
        MultipleChoiceExercise multipleChoice
            when multipleChoice.CorrectOptionIndex >= 0 && multipleChoice.CorrectOptionIndex < multipleChoice.Options.Count
                => multipleChoice.Options[multipleChoice.CorrectOptionIndex],
        SentenceOrderExercise sentenceOrder => string.Join(" ", sentenceOrder.CorrectOrder),
        ConjugationDrillExercise conjugation
            => string.Join(", ", conjugation.Slots.Select(slot => $"{slot.Label} {slot.CorrectForm}")),
        _ => string.Empty
    };
    public virtual bool RequiresExplicitCheck => true;
    public bool ShowCheckAnswerButton => RequiresExplicitCheck && !IsAnswered;
    public Func<Task>? SubmitAnswerAsync { get; set; }

    public abstract ExerciseAnswer BuildAnswer();

    public virtual void Reset()
    {
        IsAnswered = false;
        IsCorrect = null;
        Explanation = null;
    }
}

using System.Collections.ObjectModel;
using AllItems.Translation.Core.Curriculum;
using CommunityToolkit.Mvvm.Input;

namespace AllItems.Translation.App.ViewModels.Training;

public sealed partial class SentenceOrderExerciseViewModel : ExerciseViewModel
{
    public ObservableCollection<string> AvailableWords { get; }
    public ObservableCollection<string> ChosenWords { get; } = [];

    public SentenceOrderExerciseViewModel(SentenceOrderExercise exercise) : base(exercise)
    {
        AvailableWords = new ObservableCollection<string>(exercise.ScrambledWords);
    }

    [RelayCommand]
    private void ChooseWord(string? word)
    {
        if (word is null || !AvailableWords.Remove(word))
        {
            return;
        }

        ChosenWords.Add(word);
    }

    [RelayCommand]
    private void RemoveWord(string? word)
    {
        if (word is null || !ChosenWords.Remove(word))
        {
            return;
        }

        AvailableWords.Add(word);
    }

    public override ExerciseAnswer BuildAnswer() => new(WordOrder: ChosenWords.ToList());

    public override void Reset()
    {
        base.Reset();
        foreach (var word in ChosenWords.ToList())
        {
            ChosenWords.Remove(word);
            AvailableWords.Add(word);
        }
    }
}

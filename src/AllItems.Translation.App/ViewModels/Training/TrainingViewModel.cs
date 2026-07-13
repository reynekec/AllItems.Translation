using System.Collections.ObjectModel;
using AllItems.Translation.Core.Curriculum;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AllItems.Translation.App.ViewModels.Training;

public sealed partial class TrainingViewModel(ICurriculumService curriculumService) : ObservableObject
{
    private CefrLevel _selectedLevel;
    private IReadOnlyList<Exercise> _currentExercises = [];
    private int _currentExerciseIndex;

    public ObservableCollection<LevelTileViewModel> Levels { get; } = [];
    public ObservableCollection<UnitTileViewModel> Units { get; } = [];

    [ObservableProperty]
    private TrainingScreen screen = TrainingScreen.Levels;

    [ObservableProperty]
    private string selectedLevelTitle = string.Empty;

    [ObservableProperty]
    private string selectedLevelProgressText = string.Empty;

    [ObservableProperty]
    private CurriculumUnit? selectedUnit;

    [ObservableProperty]
    private ExerciseViewModel? currentExercise;

    [ObservableProperty]
    private string exerciseProgressText = string.Empty;

    [ObservableProperty]
    private bool isUnitComplete;

    [ObservableProperty]
    private bool isLoadingLevels;

    public void Initialize() => _ = LoadLevelsAsync();

    private async Task LoadLevelsAsync()
    {
        IsLoadingLevels = true;
        try
        {
            var summaries = await curriculumService.GetLevelSummariesAsync();
            Levels.Clear();
            foreach (var summary in summaries)
            {
                Levels.Add(new LevelTileViewModel(summary));
            }
        }
        finally
        {
            IsLoadingLevels = false;
        }
    }

    [RelayCommand]
    private async Task OpenLevelAsync(LevelTileViewModel? levelTile)
    {
        if (levelTile is null || !levelTile.IsClickable)
        {
            return;
        }

        _selectedLevel = levelTile.Level;
        SelectedLevelTitle = levelTile.Level.ToString();
        await LoadUnitsAsync();
        Screen = TrainingScreen.Units;
    }

    private async Task LoadUnitsAsync()
    {
        var summaries = await curriculumService.GetUnitSummariesAsync(_selectedLevel);
        Units.Clear();
        var completedUnits = 0;
        foreach (var summary in summaries)
        {
            var unitTile = new UnitTileViewModel(summary);
            Units.Add(unitTile);
            if (unitTile.IsComplete)
            {
                completedUnits++;
            }
        }

        SelectedLevelProgressText = $"{completedUnits} / {Units.Count}";
    }

    [RelayCommand]
    private void OpenUnit(UnitTileViewModel? unitTile)
    {
        if (unitTile is null)
        {
            return;
        }

        SelectedUnit = unitTile.Unit;
        _currentExercises = unitTile.Unit.Exercises;
        _currentExerciseIndex = Math.Min(unitTile.CompletedExerciseCount, Math.Max(_currentExercises.Count - 1, 0));
        IsUnitComplete = false;
        ShowCurrentExercise();
        Screen = TrainingScreen.Exercise;
    }

    private void ShowCurrentExercise()
    {
        var exercise = _currentExercises[_currentExerciseIndex];
        CurrentExercise = ExerciseViewModelFactory.Create(exercise);
        ExerciseProgressText = $"{_currentExerciseIndex + 1} / {_currentExercises.Count}";
    }

    [RelayCommand]
    private async Task CheckAnswerAsync()
    {
        if (CurrentExercise is null)
        {
            return;
        }

        var answer = CurrentExercise.BuildAnswer();
        var result = await curriculumService.SubmitAnswerAsync(CurrentExercise.Exercise, answer);

        CurrentExercise.IsAnswered = true;
        CurrentExercise.IsCorrect = result.IsCorrect;
        CurrentExercise.Explanation = result.Explanation;

        if (CurrentExercise is ConjugationDrillExerciseViewModel drillViewModel)
        {
            drillViewModel.ApplySlotCorrectness(result.SlotCorrectness);
        }
    }

    [RelayCommand]
    private void TryAgain() => CurrentExercise?.Reset();

    [RelayCommand]
    private async Task NextExerciseAsync()
    {
        _currentExerciseIndex++;
        if (_currentExerciseIndex >= _currentExercises.Count)
        {
            IsUnitComplete = true;
            await LoadUnitsAsync();
            await LoadLevelsAsync();
            return;
        }

        ShowCurrentExercise();
    }

    [RelayCommand]
    private void BackToUnits() => Screen = TrainingScreen.Units;

    [RelayCommand]
    private async Task BackToLevelsAsync()
    {
        Screen = TrainingScreen.Levels;
        SelectedLevelProgressText = string.Empty;
        await LoadLevelsAsync();
    }
}

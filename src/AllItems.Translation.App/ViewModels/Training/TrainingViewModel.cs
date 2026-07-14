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
    private bool _isRetrainSession;

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

    [ObservableProperty]
    private string activeSessionTitle = string.Empty;

    [ObservableProperty]
    private string activeSessionProgressDetail = string.Empty;

    [ObservableProperty]
    private string retrainEmptyMessage = "No retraining is due yet. Finish or revisit some A1-C2 exercises and due items will appear here.";

    [ObservableProperty]
    private bool isRetrainButtonEnabled = true;

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
        _isRetrainSession = false;
        IsUnitComplete = false;
        ActiveSessionTitle = unitTile.Title;
        ActiveSessionProgressDetail = "Unit training";
        ShowCurrentExercise();
        Screen = TrainingScreen.Exercise;
    }

    [RelayCommand]
    private async Task OpenRetrainAsync()
    {
        IsRetrainButtonEnabled = false;
        try
        {
            var retrainSession = await curriculumService.BuildRetrainSessionAsync();
            ActiveSessionTitle = "Mixed retraining";
            ActiveSessionProgressDetail = $"Due: {retrainSession.DueExerciseCount} | Attempted: {retrainSession.TotalAttemptedExerciseCount}";

            if (retrainSession.Exercises.Count == 0)
            {
                _currentExercises = [];
                _currentExerciseIndex = 0;
                _isRetrainSession = true;
                IsUnitComplete = false;
                Screen = TrainingScreen.RetrainEmpty;
                return;
            }

            _currentExercises = retrainSession.Exercises.Select(item => item.Exercise).ToList();
            _currentExerciseIndex = 0;
            _isRetrainSession = true;
            SelectedUnit = null;
            IsUnitComplete = false;
            ShowCurrentExercise();
            Screen = TrainingScreen.Exercise;
        }
        finally
        {
            IsRetrainButtonEnabled = true;
        }
    }

    private void ShowCurrentExercise()
    {
        var exercise = _currentExercises[_currentExerciseIndex];
        CurrentExercise = ExerciseViewModelFactory.Create(exercise);
        CurrentExercise.SubmitAnswerAsync = CheckAnswerAsync;
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

        if (result.IsCorrect)
        {
            await NextExerciseAsync();
            return;
        }

        CurrentExercise.IsAnswered = true;
        CurrentExercise.IsCorrect = result.IsCorrect;
        CurrentExercise.Explanation = result.Explanation;

        if (CurrentExercise is ConjugationDrillExerciseViewModel drillViewModel)
        {
            drillViewModel.ApplySlotCorrectness(result.SlotCorrectness);
        }

        if (CurrentExercise is MultipleChoiceExerciseViewModel multipleChoiceViewModel)
        {
            multipleChoiceViewModel.ApplyIncorrectAnswerHighlights();
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
            if (!_isRetrainSession)
            {
                await LoadUnitsAsync();
            }
            await LoadLevelsAsync();
            return;
        }

        ShowCurrentExercise();
    }

    [RelayCommand]
    private void BackToUnits()
    {
        _isRetrainSession = false;
        Screen = TrainingScreen.Units;
    }

    [RelayCommand]
    private async Task BackToLevelsAsync()
    {
        _isRetrainSession = false;
        Screen = TrainingScreen.Levels;
        SelectedLevelProgressText = string.Empty;
        await LoadLevelsAsync();
    }
}

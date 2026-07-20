using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Domain;
using AllItems.Translation.Core.Study;

namespace AllItems.Translation.Core.Curriculum;

public sealed class CurriculumService(
    ICurriculumCatalog catalog,
    ICurriculumProgressRepository progressRepository,
    IExerciseGrader grader,
    IWordRepository wordRepository,
    IVocabularyImportService vocabularyImportService,
    ISpacedRepetitionScheduler scheduler,
    IClock clock) : ICurriculumService
{
    public async Task<IReadOnlyList<LevelSummary>> GetLevelSummariesAsync(CancellationToken cancellationToken = default)
    {
        var summaries = new List<LevelSummary>();
        var previousLevelComplete = true;

        foreach (var level in Enum.GetValues<CefrLevel>())
        {
            var units = catalog.GetUnits(level);
            var isUnlocked = level == CefrLevel.A1 || previousLevelComplete;

            var completedUnitCount = 0;
            if (units.Count > 0)
            {
                var exerciseIds = units.SelectMany(u => u.Exercises).Select(e => e.Id).ToList();
                var completedIds = await progressRepository.GetCompletedExerciseIdsAsync(exerciseIds, cancellationToken);
                completedUnitCount = units.Count(u => u.Exercises.All(e => completedIds.Contains(e.Id)));
            }

            if (isUnlocked)
            {
                await vocabularyImportService.EnsureLevelImportedAsync(level, cancellationToken);
            }

            summaries.Add(new LevelSummary(level, isUnlocked, completedUnitCount, units.Count));

            previousLevelComplete = isUnlocked && units.Count > 0 && completedUnitCount == units.Count;
        }

        return summaries;
    }

    public async Task<IReadOnlyList<UnitSummary>> GetUnitSummariesAsync(CefrLevel level, CancellationToken cancellationToken = default)
    {
        var units = catalog.GetUnits(level);
        if (units.Count == 0)
        {
            return [];
        }

        var exerciseIds = units.SelectMany(u => u.Exercises).Select(e => e.Id).ToList();
        var completedIds = await progressRepository.GetCompletedExerciseIdsAsync(exerciseIds, cancellationToken);

        return units
            .OrderBy(u => u.SortOrder)
            .Select(u => new UnitSummary(u, u.Exercises.Count(e => completedIds.Contains(e.Id)), u.Exercises.Count))
            .ToList();
    }

    public async Task<CurriculumRetrainSession> BuildRetrainSessionAsync(CancellationToken cancellationToken = default)
    {
        var attemptedIds = await progressRepository.GetAttemptedExerciseIdsAsync(cancellationToken);
        if (attemptedIds.Count == 0)
        {
            return new CurriculumRetrainSession([], 0, 0);
        }

        var allExercises = Enum.GetValues<CefrLevel>()
            .SelectMany(level => catalog.GetUnits(level))
            .SelectMany(unit => unit.Exercises)
            .ToDictionary(exercise => exercise.Id, StringComparer.Ordinal);

        var eligibleIds = attemptedIds.Where(allExercises.ContainsKey).ToList();
        if (eligibleIds.Count == 0)
        {
            return new CurriculumRetrainSession([], 0, attemptedIds.Count);
        }

        var states = await progressRepository.GetReviewStatesAsync(eligibleIds, cancellationToken);
        var now = clock.UtcNow;

        var allDueExercises = eligibleIds
            .Select(id => new CurriculumRetrainExercise(
                allExercises[id],
                states.TryGetValue(id, out var state)
                    ? state
                    : new CurriculumExerciseReviewState { ExerciseId = id }))
            .Where(item => item.ReviewState.DueDateUtc is null || item.ReviewState.DueDateUtc <= now)
            .OrderByDescending(item => item.ReviewState.IncorrectAttempts > 0)
            .ThenByDescending(item => item.ReviewState.LapseCount)
            .ThenByDescending(item => item.ReviewState.IncorrectAttempts - item.ReviewState.CorrectAttempts)
            .ThenBy(item => item.ReviewState.DueDateUtc ?? DateTime.MinValue)
            .ThenBy(item => item.Exercise.Id, StringComparer.Ordinal)
            .ToList();

        var dueExercises = allDueExercises.Take(20).ToList();

        return new CurriculumRetrainSession(dueExercises, allDueExercises.Count, attemptedIds.Count);
    }

    public async Task<GradingResult> SubmitAnswerAsync(Exercise exercise, ExerciseAnswer answer, CancellationToken cancellationToken = default)
    {
        var result = grader.Grade(exercise, answer);
        await progressRepository.RecordAttemptAsync(exercise.Id, result.IsCorrect, cancellationToken);

        var currentState = (await progressRepository.GetReviewStatesAsync([exercise.Id], cancellationToken))
            .GetValueOrDefault(exercise.Id)
            ?? new CurriculumExerciseReviewState { ExerciseId = exercise.Id };

        var scheduledState = scheduler.Schedule(
            new WordReviewState
            {
                EasinessFactor = currentState.EasinessFactor,
                IntervalDays = currentState.IntervalDays,
                Repetitions = currentState.Repetitions,
                LapseCount = currentState.LapseCount,
                DueDateUtc = currentState.DueDateUtc,
                LastReviewedUtc = currentState.LastReviewedUtc
            },
            result.IsCorrect ? ReviewGrade.Good : ReviewGrade.Again,
            clock.UtcNow);

        currentState.EasinessFactor = scheduledState.EasinessFactor;
        currentState.IntervalDays = scheduledState.IntervalDays;
        currentState.Repetitions = scheduledState.Repetitions;
        currentState.LapseCount = scheduledState.LapseCount;
        currentState.DueDateUtc = scheduledState.DueDateUtc;
        currentState.LastReviewedUtc = scheduledState.LastReviewedUtc;
        if (result.IsCorrect)
        {
            currentState.CorrectAttempts++;
        }
        else
        {
            currentState.IncorrectAttempts++;
        }

        await progressRepository.UpsertReviewStateAsync(currentState, cancellationToken);

        if (result.IsCorrect)
        {
            await progressRepository.MarkExerciseCompletedAsync(exercise.Id, cancellationToken);

            if (exercise.Teaches is { } teaching)
            {
                await AddToDictionaryAsync(teaching, cancellationToken);
            }
        }

        return result;
    }

    private async Task AddToDictionaryAsync(VocabularyTeaching teaching, CancellationToken cancellationToken)
    {
        var normalized = teaching.GermanWord.ToLowerInvariant();
        var entry = await wordRepository.GetOrCreateAsync(Language.German, normalized, cancellationToken);
        var existing = await wordRepository.GetTranslationsAsync(entry.Id, Language.English, cancellationToken);

        var alreadyKnown = existing.Any(t => string.Equals(t.TargetText, teaching.EnglishMeaning, StringComparison.OrdinalIgnoreCase));
        if (!alreadyKnown)
        {
            await wordRepository.AddTranslationAsync(entry.Id, Language.English, teaching.EnglishMeaning, isPreferred: existing.Count == 0, cancellationToken);
        }

        if (!string.IsNullOrWhiteSpace(teaching.ExampleSentence))
        {
            await wordRepository.SetStudyContentAsync(entry.Id, teaching.Article, teaching.ExampleSentence, teaching.Highlights ?? [], cancellationToken);
        }
    }
}

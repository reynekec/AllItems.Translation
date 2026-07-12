using AllItems.Translation.Core.Abstractions;

namespace AllItems.Translation.Core.Curriculum;

public sealed class CurriculumService(
    ICurriculumCatalog catalog,
    ICurriculumProgressRepository progressRepository,
    IExerciseGrader grader) : ICurriculumService
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

    public async Task<GradingResult> SubmitAnswerAsync(Exercise exercise, ExerciseAnswer answer, CancellationToken cancellationToken = default)
    {
        var result = grader.Grade(exercise, answer);
        if (result.IsCorrect)
        {
            await progressRepository.MarkExerciseCompletedAsync(exercise.Id, cancellationToken);
        }

        return result;
    }
}

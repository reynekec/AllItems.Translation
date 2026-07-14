using AllItems.Translation.Core.Curriculum;

namespace AllItems.Translation.Tests.Curriculum;

public class ClozeEnglishHintCoverageTests
{
    [Fact]
    public void EveryClozeExercise_HasSpecificEnglishHint_AcrossA1ToC2()
    {
        var catalog = new StaticCurriculumCatalog();

        var clozeExercises = Enum.GetValues<CefrLevel>()
            .SelectMany(level => catalog.GetUnits(level))
            .SelectMany(unit => unit.Exercises)
            .OfType<ClozeExercise>()
            .ToList();

        Assert.NotEmpty(clozeExercises);

        var missing = clozeExercises
            .Where(exercise => string.Equals(ClozeEnglishHintProvider.GetHint(exercise), ClozeEnglishHintProvider.UnknownHint, StringComparison.Ordinal))
            .Select(exercise => exercise.Id)
            .OrderBy(id => id, StringComparer.Ordinal)
            .ToList();

        Assert.True(missing.Count == 0, "Missing English hints for cloze exercises: " + string.Join(", ", missing));
    }
}

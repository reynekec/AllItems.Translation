using AllItems.Translation.Core.Curriculum;

namespace AllItems.Translation.Tests.Curriculum;

public class StaticCurriculumCatalogTests
{
    private readonly StaticCurriculumCatalog _catalog = new();

    [Theory]
    [InlineData(CefrLevel.A1)]
    [InlineData(CefrLevel.A2)]
    [InlineData(CefrLevel.B1)]
    [InlineData(CefrLevel.B2)]
    [InlineData(CefrLevel.C1)]
    [InlineData(CefrLevel.C2)]
    public void GetUnits_EveryLevel_ReturnsAuthoredContent(CefrLevel level)
    {
        Assert.NotEmpty(_catalog.GetUnits(level));
    }

    [Fact]
    public void AllExerciseIds_AreUniqueAcrossTheWholeCatalog()
    {
        var allIds = Enum.GetValues<CefrLevel>()
            .SelectMany(_catalog.GetUnits)
            .SelectMany(u => u.Exercises)
            .Select(e => e.Id)
            .ToList();

        Assert.Equal(allIds.Count, allIds.Distinct().Count());
    }

    [Fact]
    public void AllUnitIds_AreUniqueAcrossTheWholeCatalog()
    {
        var allUnitIds = Enum.GetValues<CefrLevel>()
            .SelectMany(_catalog.GetUnits)
            .Select(u => u.Id)
            .ToList();

        Assert.Equal(allUnitIds.Count, allUnitIds.Distinct().Count());
    }
}

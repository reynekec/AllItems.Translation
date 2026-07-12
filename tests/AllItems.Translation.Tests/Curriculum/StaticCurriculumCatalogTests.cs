using AllItems.Translation.Core.Curriculum;

namespace AllItems.Translation.Tests.Curriculum;

public class StaticCurriculumCatalogTests
{
    private readonly StaticCurriculumCatalog _catalog = new();

    [Fact]
    public void GetUnits_A1_ReturnsAuthoredContent()
    {
        Assert.NotEmpty(_catalog.GetUnits(CefrLevel.A1));
    }

    [Fact]
    public void GetUnits_A2_ReturnsAuthoredContent()
    {
        Assert.NotEmpty(_catalog.GetUnits(CefrLevel.A2));
    }

    [Fact]
    public void GetUnits_B1_ReturnsAuthoredContent()
    {
        Assert.NotEmpty(_catalog.GetUnits(CefrLevel.B1));
    }

    [Fact]
    public void GetUnits_B2_ReturnsAuthoredContent()
    {
        Assert.NotEmpty(_catalog.GetUnits(CefrLevel.B2));
    }

    [Fact]
    public void GetUnits_C1_ReturnsAuthoredContent()
    {
        Assert.NotEmpty(_catalog.GetUnits(CefrLevel.C1));
    }

    [Fact]
    public void GetUnits_C2_IsNotYetAuthored()
    {
        Assert.Empty(_catalog.GetUnits(CefrLevel.C2));
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

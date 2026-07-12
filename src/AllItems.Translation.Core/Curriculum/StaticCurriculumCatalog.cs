using AllItems.Translation.Core.Curriculum.Content;

namespace AllItems.Translation.Core.Curriculum;

/// <summary>
/// Aggregates hand-authored curriculum content per level. B1-C2 are intentionally empty for now -
/// see <see cref="ICurriculumService"/>'s unlock rule for how an empty level is handled.
/// </summary>
public sealed class StaticCurriculumCatalog : ICurriculumCatalog
{
    public IReadOnlyList<CurriculumUnit> GetUnits(CefrLevel level) => level switch
    {
        CefrLevel.A1 => A1Units.All,
        CefrLevel.A2 => A2Units.All,
        CefrLevel.B1 => [],
        CefrLevel.B2 => [],
        CefrLevel.C1 => [],
        CefrLevel.C2 => [],
        _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
    };
}

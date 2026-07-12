using AllItems.Translation.Core.Curriculum.Content;

namespace AllItems.Translation.Core.Curriculum;

/// <summary>Aggregates hand-authored curriculum content per level - the full A1-C2 progression.</summary>
public sealed class StaticCurriculumCatalog : ICurriculumCatalog
{
    public IReadOnlyList<CurriculumUnit> GetUnits(CefrLevel level) => level switch
    {
        CefrLevel.A1 => A1Units.All,
        CefrLevel.A2 => A2Units.All,
        CefrLevel.B1 => B1Units.All,
        CefrLevel.B2 => B2Units.All,
        CefrLevel.C1 => C1Units.All,
        CefrLevel.C2 => C2Units.All,
        _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
    };
}

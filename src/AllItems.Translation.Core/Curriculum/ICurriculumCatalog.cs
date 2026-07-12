namespace AllItems.Translation.Core.Curriculum;

/// <summary>The authored curriculum content - static by design, since units/exercises are hand-written, not user data.</summary>
public interface ICurriculumCatalog
{
    IReadOnlyList<CurriculumUnit> GetUnits(CefrLevel level);
}

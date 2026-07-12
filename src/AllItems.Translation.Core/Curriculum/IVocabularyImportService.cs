namespace AllItems.Translation.Core.Curriculum;

/// <summary>Imports a CEFR level's bulk vocabulary word list into the Dictionary, once, the first time that level unlocks.</summary>
public interface IVocabularyImportService
{
    Task EnsureLevelImportedAsync(CefrLevel level, CancellationToken cancellationToken = default);
}

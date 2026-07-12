namespace AllItems.Translation.Core.Curriculum;

/// <summary>Imports a CEFR level's bulk vocabulary word list into the Dictionary, once, the first time that level unlocks.</summary>
public interface IVocabularyImportService
{
    Task EnsureLevelImportedAsync(CefrLevel level, CancellationToken cancellationToken = default);

    /// <summary>Imports every CEFR level's bulk vocabulary right away, ignoring the normal unlock gating.</summary>
    Task ImportAllLevelsAsync(CancellationToken cancellationToken = default);
}

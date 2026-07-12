using AllItems.Translation.Core.Curriculum;

namespace AllItems.Translation.Core.Abstractions;

/// <summary>Tracks which CEFR levels' bulk vocabulary has already been imported into the Dictionary.</summary>
public interface IVocabularyImportRepository
{
    Task<bool> IsLevelImportedAsync(CefrLevel level, CancellationToken cancellationToken = default);

    Task MarkLevelImportedAsync(CefrLevel level, CancellationToken cancellationToken = default);
}

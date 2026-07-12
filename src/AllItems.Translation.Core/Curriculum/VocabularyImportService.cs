using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Domain;

namespace AllItems.Translation.Core.Curriculum;

public sealed class VocabularyImportService(
    IVocabularySeedRepository seedRepository,
    IVocabularyImportRepository importRepository,
    IWordRepository wordRepository) : IVocabularyImportService
{
    public async Task EnsureLevelImportedAsync(CefrLevel level, CancellationToken cancellationToken = default)
    {
        if (await importRepository.IsLevelImportedAsync(level, cancellationToken))
        {
            return;
        }

        foreach (var word in seedRepository.GetWords(level))
        {
            var normalized = word.German.ToLowerInvariant();
            var entry = await wordRepository.GetOrCreateAsync(Language.German, normalized, cancellationToken);
            var existing = await wordRepository.GetTranslationsAsync(entry.Id, Language.English, cancellationToken);

            var alreadyKnown = existing.Any(t => string.Equals(t.TargetText, word.English, StringComparison.OrdinalIgnoreCase));
            if (!alreadyKnown)
            {
                await wordRepository.AddTranslationAsync(entry.Id, Language.English, word.English, isPreferred: existing.Count == 0, cancellationToken);
            }
        }

        await importRepository.MarkLevelImportedAsync(level, cancellationToken);
    }
}

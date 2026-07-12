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

        var words = seedRepository.GetWords(level);
        if (words.Count > 0)
        {
            await wordRepository.ImportWordsAsync(Language.German, words, cancellationToken);
        }

        await importRepository.MarkLevelImportedAsync(level, cancellationToken);
    }
}

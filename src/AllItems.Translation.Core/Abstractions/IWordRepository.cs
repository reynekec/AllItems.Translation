using AllItems.Translation.Core.Domain;

namespace AllItems.Translation.Core.Abstractions;

public interface IWordRepository
{
    /// <summary>Gets the word entry for this exact (language, normalized text) pair, creating it if absent.</summary>
    Task<WordEntry> GetOrCreateAsync(Language language, string normalizedText, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<WordTranslation>> GetTranslationsAsync(int wordEntryId, Language targetLanguage, CancellationToken cancellationToken = default);

    Task<WordTranslation> AddTranslationAsync(int wordEntryId, Language targetLanguage, string targetText, bool isPreferred, CancellationToken cancellationToken = default);

    /// <summary>Marks one translation as the preferred meaning, clearing the flag on its siblings.</summary>
    Task SetPreferredAsync(int wordEntryId, Language targetLanguage, int translationId, CancellationToken cancellationToken = default);

    /// <summary>All known words with all their translations, for the Dictionary Manager screen.</summary>
    Task<IReadOnlyList<WordEntry>> GetAllWithTranslationsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Words in <paramref name="sourceLanguage"/> that have a preferred translation into
    /// <paramref name="targetLanguage"/> - the study-session card pool for that language pair.
    /// Each returned entry's <see cref="WordEntry.Translations"/> contains just that one preferred translation.
    /// </summary>
    Task<IReadOnlyList<WordEntry>> GetWordsWithPreferredTranslationAsync(Language sourceLanguage, Language targetLanguage, CancellationToken cancellationToken = default);

    Task DeleteTranslationAsync(int translationId, CancellationToken cancellationToken = default);

    Task UpdateTranslationTextAsync(int translationId, string newText, CancellationToken cancellationToken = default);
}

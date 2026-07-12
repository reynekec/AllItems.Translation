using AllItems.Translation.Core.Domain;

namespace AllItems.Translation.Core.Translation;

public interface ISentenceTranslationService
{
    /// <summary>
    /// Translates <paramref name="sourceSentence"/> word-by-word into <paramref name="targetLanguage"/>,
    /// using cached meanings where known and fetching+caching new ones as needed.
    /// </summary>
    Task<SentenceTranslationResult> TranslateAsync(
        string sourceSentence,
        Language sourceLanguage,
        Language targetLanguage,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Cycles the word at <paramref name="tokenIndex"/> to the next candidate meaning, persists
    /// that choice as the new preferred meaning for this word, and returns the updated result.
    /// </summary>
    Task<SentenceTranslationResult> CycleWordMeaningAsync(
        SentenceTranslationResult currentResult,
        int tokenIndex,
        CancellationToken cancellationToken = default);
}

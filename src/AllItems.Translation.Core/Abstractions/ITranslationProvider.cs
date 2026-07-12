using AllItems.Translation.Core.Domain;

namespace AllItems.Translation.Core.Abstractions;

/// <summary>
/// Abstraction over whichever machine-translation backend supplies fresh translations
/// (Google Cloud Translation today; swappable without touching the rest of the app).
/// </summary>
public interface ITranslationProvider
{
    /// <summary>Translates a single word/short phrase with no surrounding context.</summary>
    Task<string> TranslateWordAsync(string word, Language source, Language target, CancellationToken cancellationToken = default);

    /// <summary>Translates a full sentence, letting the provider use context for a fluent result.</summary>
    Task<string> TranslateSentenceAsync(string sentence, Language source, Language target, CancellationToken cancellationToken = default);
}

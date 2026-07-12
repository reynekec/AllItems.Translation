using AllItems.Translation.Core.Domain;

namespace AllItems.Translation.Core.Abstractions;

public interface ISentenceTranslationRepository
{
    Task<string?> FindAsync(Language source, Language target, string normalizedSentence, CancellationToken cancellationToken = default);

    Task SaveAsync(Language source, Language target, string normalizedSentence, string translatedText, CancellationToken cancellationToken = default);
}

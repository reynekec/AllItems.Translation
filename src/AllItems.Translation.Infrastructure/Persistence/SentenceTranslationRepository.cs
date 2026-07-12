using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace AllItems.Translation.Infrastructure.Persistence;

public sealed class SentenceTranslationRepository(AppDbContext db, IClock clock) : ISentenceTranslationRepository
{
    public async Task<string?> FindAsync(Language source, Language target, string normalizedSentence, CancellationToken cancellationToken = default)
    {
        var entity = await db.SentenceTranslations.FirstOrDefaultAsync(
            s => s.SourceLanguage == source && s.TargetLanguage == target && s.NormalizedSourceText == normalizedSentence,
            cancellationToken);
        return entity?.TranslatedText;
    }

    public async Task SaveAsync(Language source, Language target, string normalizedSentence, string translatedText, CancellationToken cancellationToken = default)
    {
        db.SentenceTranslations.Add(new SentenceTranslation
        {
            SourceLanguage = source,
            TargetLanguage = target,
            NormalizedSourceText = normalizedSentence,
            TranslatedText = translatedText,
            CreatedAtUtc = clock.UtcNow
        });
        await db.SaveChangesAsync(cancellationToken);
    }
}

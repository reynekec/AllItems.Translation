using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace AllItems.Translation.Infrastructure.Persistence;

public sealed class WordRepository(AppDbContext db, IClock clock) : IWordRepository
{
    public async Task<WordEntry> GetOrCreateAsync(Language language, string normalizedText, CancellationToken cancellationToken = default)
    {
        var existing = await db.WordEntries
            .FirstOrDefaultAsync(w => w.Language == language && w.NormalizedText == normalizedText, cancellationToken);
        if (existing is not null)
        {
            return existing;
        }

        var entry = new WordEntry { Language = language, NormalizedText = normalizedText };
        db.WordEntries.Add(entry);
        await db.SaveChangesAsync(cancellationToken);
        return entry;
    }

    public async Task<IReadOnlyList<WordTranslation>> GetTranslationsAsync(int wordEntryId, Language targetLanguage, CancellationToken cancellationToken = default)
    {
        return await db.WordTranslations
            .Where(t => t.WordEntryId == wordEntryId && t.TargetLanguage == targetLanguage)
            .OrderByDescending(t => t.IsPreferred)
            .ThenBy(t => t.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<WordTranslation> AddTranslationAsync(int wordEntryId, Language targetLanguage, string targetText, bool isPreferred, CancellationToken cancellationToken = default)
    {
        if (isPreferred)
        {
            await ClearPreferredAsync(wordEntryId, targetLanguage, cancellationToken);
        }

        var translation = new WordTranslation
        {
            WordEntryId = wordEntryId,
            TargetLanguage = targetLanguage,
            TargetText = targetText,
            IsPreferred = isPreferred,
            CreatedAtUtc = clock.UtcNow
        };
        db.WordTranslations.Add(translation);
        await db.SaveChangesAsync(cancellationToken);
        return translation;
    }

    public async Task SetPreferredAsync(int wordEntryId, Language targetLanguage, int translationId, CancellationToken cancellationToken = default)
    {
        var siblings = await db.WordTranslations
            .Where(t => t.WordEntryId == wordEntryId && t.TargetLanguage == targetLanguage)
            .ToListAsync(cancellationToken);

        foreach (var sibling in siblings)
        {
            sibling.IsPreferred = sibling.Id == translationId;
        }

        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<WordEntry>> GetAllWithTranslationsAsync(CancellationToken cancellationToken = default)
    {
        return await db.WordEntries
            .Include(w => w.Translations)
            .OrderBy(w => w.Language)
            .ThenBy(w => w.NormalizedText)
            .ToListAsync(cancellationToken);
    }

    public async Task DeleteTranslationAsync(int translationId, CancellationToken cancellationToken = default)
    {
        var translation = await db.WordTranslations.FindAsync([translationId], cancellationToken);
        if (translation is null)
        {
            return;
        }

        db.WordTranslations.Remove(translation);
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateTranslationTextAsync(int translationId, string newText, CancellationToken cancellationToken = default)
    {
        var translation = await db.WordTranslations.FindAsync([translationId], cancellationToken);
        if (translation is null)
        {
            return;
        }

        translation.TargetText = newText;
        await db.SaveChangesAsync(cancellationToken);
    }

    private async Task ClearPreferredAsync(int wordEntryId, Language targetLanguage, CancellationToken cancellationToken)
    {
        var siblings = await db.WordTranslations
            .Where(t => t.WordEntryId == wordEntryId && t.TargetLanguage == targetLanguage && t.IsPreferred)
            .ToListAsync(cancellationToken);

        foreach (var sibling in siblings)
        {
            sibling.IsPreferred = false;
        }
    }
}

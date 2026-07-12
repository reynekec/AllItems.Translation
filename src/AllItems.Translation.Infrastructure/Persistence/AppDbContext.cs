using AllItems.Translation.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace AllItems.Translation.Infrastructure.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<WordEntry> WordEntries => Set<WordEntry>();
    public DbSet<WordTranslation> WordTranslations => Set<WordTranslation>();
    public DbSet<SentenceTranslation> SentenceTranslations => Set<SentenceTranslation>();
    public DbSet<ApiUsageRecord> ApiUsageRecords => Set<ApiUsageRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WordEntry>(entity =>
        {
            entity.HasIndex(w => new { w.Language, w.NormalizedText }).IsUnique();
            entity.HasMany(w => w.Translations)
                .WithOne(t => t.WordEntry!)
                .HasForeignKey(t => t.WordEntryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<WordTranslation>(entity =>
        {
            entity.HasIndex(t => new { t.WordEntryId, t.TargetLanguage });
        });

        modelBuilder.Entity<SentenceTranslation>(entity =>
        {
            entity.HasIndex(s => new { s.SourceLanguage, s.TargetLanguage, s.NormalizedSourceText }).IsUnique();
        });

        modelBuilder.Entity<ApiUsageRecord>(entity =>
        {
            entity.HasIndex(u => u.YearMonth).IsUnique();
        });
    }
}

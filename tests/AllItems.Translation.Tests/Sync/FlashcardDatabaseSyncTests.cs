using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Domain;
using AllItems.Translation.Core.Study;
using AllItems.Translation.Infrastructure.Persistence;

namespace AllItems.Translation.Tests.Sync;

/// <summary>
/// Proves the one-way sync payload choice: exporting the whole SQLite file and reopening it elsewhere reproduces
/// the exact same flashcard session (cards, order, and SM-2 review state), with no per-row import code. This is
/// what lets the phone just download the .db and run the identical <see cref="StudySessionService"/>.
/// </summary>
public sealed class FlashcardDatabaseSyncTests : IDisposable
{
    private sealed class FixedClock(DateTime utcNow) : IClock
    {
        public DateTime UtcNow { get; set; } = utcNow;
    }

    private readonly FixedClock _clock = new(new DateTime(2026, 7, 14, 0, 0, 0, DateTimeKind.Utc));
    private readonly List<string> _databasePaths = [];

    private (SqliteConnectionFactory Factory, string Path) NewDatabase()
    {
        var path = Path.Combine(Path.GetTempPath(), $"allitems-sync-{Guid.NewGuid():N}.db");
        _databasePaths.Add(path);
        // Pooling=False so no handle survives Dispose(); otherwise WAL mode keeps the file locked on copy/delete.
        var factory = new SqliteConnectionFactory($"Data Source={path};Pooling=False");
        new DatabaseInitializer(factory).InitializeAsync().GetAwaiter().GetResult();
        return (factory, path);
    }

    private StudySessionService StudyService(SqliteConnectionFactory factory)
    {
        var words = new WordRepository(factory, _clock);
        var reviews = new SqlReviewStateRepository(factory);
        return new StudySessionService(words, reviews, new Sm2Scheduler(), _clock);
    }

    /// <summary>Simulates the desktop export: checkpoint the WAL, then copy just the .db file (as the export service uploads it).</summary>
    private static string ExportCopy(SqliteConnectionFactory source, string sourcePath)
    {
        source.RunAsync(connection =>
        {
            using var pragma = connection.CreateCommand();
            pragma.CommandText = "PRAGMA wal_checkpoint(TRUNCATE);";
            pragma.ExecuteNonQuery();
        }).GetAwaiter().GetResult();

        var exportedPath = sourcePath + ".export";
        File.Copy(sourcePath, exportedPath, overwrite: true);
        return exportedPath;
    }

    [Fact]
    public async Task ExportedDatabase_ReopenedElsewhere_ReproducesTheSameCards()
    {
        var (factory, path) = NewDatabase();
        var words = new WordRepository(factory, _clock);

        var hund = await words.GetOrCreateAsync(Language.German, "hund");
        await words.AddTranslationAsync(hund.Id, Language.Afrikaans, "hond", isPreferred: true);
        var katze = await words.GetOrCreateAsync(Language.German, "katze");
        await words.AddTranslationAsync(katze.Id, Language.Afrikaans, "kat", isPreferred: true);

        var exportedPath = ExportCopy(factory, path);
        _databasePaths.Add(exportedPath);

        // Reopen the exported file with a completely fresh factory - this is the phone opening the download.
        var importedFactory = new SqliteConnectionFactory($"Data Source={exportedPath};Pooling=False");
        var session = await StudyService(importedFactory).BuildSessionAsync(Language.German, Language.Afrikaans, 20);

        Assert.Equal(2, session.Count);
        Assert.Contains(session, c => c.FrontText == "hund" && c.BackText == "hond");
        Assert.Contains(session, c => c.FrontText == "katze" && c.BackText == "kat");
    }

    [Fact]
    public async Task ExportedDatabase_PreservesSm2ReviewState()
    {
        var (factory, path) = NewDatabase();
        var words = new WordRepository(factory, _clock);
        var reviews = new SqlReviewStateRepository(factory);

        var hund = await words.GetOrCreateAsync(Language.German, "hund");
        await words.AddTranslationAsync(hund.Id, Language.Afrikaans, "hond", isPreferred: true);

        await reviews.UpsertAsync(new WordReviewState
        {
            WordEntryId = hund.Id,
            TargetLanguage = Language.Afrikaans,
            EasinessFactor = 2.36,
            IntervalDays = 6,
            Repetitions = 3,
            LapseCount = 1,
            DueDateUtc = _clock.UtcNow.Date.AddDays(-1),
            LastReviewedUtc = _clock.UtcNow.Date.AddDays(-7)
        });

        var exportedPath = ExportCopy(factory, path);
        _databasePaths.Add(exportedPath);

        var importedFactory = new SqliteConnectionFactory($"Data Source={exportedPath};Pooling=False");
        var importedReviews = new SqlReviewStateRepository(importedFactory);
        var states = await importedReviews.GetStatesAsync(Language.Afrikaans, new[] { hund.Id });

        Assert.True(states.TryGetValue(hund.Id, out var state));
        Assert.Equal(2.36, state!.EasinessFactor, precision: 5);
        Assert.Equal(6, state.IntervalDays);
        Assert.Equal(3, state.Repetitions);
        Assert.Equal(1, state.LapseCount);
    }

    [Fact]
    public async Task WalCheckpoint_MakesTheDatabaseFileSelfContained()
    {
        var (factory, path) = NewDatabase();
        var words = new WordRepository(factory, _clock);

        var hund = await words.GetOrCreateAsync(Language.German, "hund");
        await words.AddTranslationAsync(hund.Id, Language.Afrikaans, "hond", isPreferred: true);

        var exportedPath = ExportCopy(factory, path);
        _databasePaths.Add(exportedPath);

        // Copy ONLY the .db (no -wal/-shm sidecars) - the exported bytes must already contain everything.
        Assert.False(File.Exists(exportedPath + "-wal"));

        var importedFactory = new SqliteConnectionFactory($"Data Source={exportedPath};Pooling=False");
        var session = await StudyService(importedFactory).BuildSessionAsync(Language.German, Language.Afrikaans, 20);

        Assert.Single(session);
    }

    public void Dispose()
    {
        foreach (var basePath in _databasePaths)
        {
            foreach (var path in new[] { basePath, basePath + "-wal", basePath + "-shm" })
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
    }
}

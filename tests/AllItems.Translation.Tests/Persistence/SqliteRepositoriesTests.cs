using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Domain;
using AllItems.Translation.Infrastructure.Persistence;

namespace AllItems.Translation.Tests.Persistence;

/// <summary>Exercises the hand-rolled ADO.NET repositories against a real, temporary SQLite file.</summary>
public sealed class SqliteRepositoriesTests : IDisposable
{
    private sealed class FixedClock(DateTime utcNow) : IClock
    {
        public DateTime UtcNow { get; set; } = utcNow;
    }

    private readonly string _databasePath;
    private readonly SqliteConnectionFactory _connectionFactory;
    private readonly FixedClock _clock = new(new DateTime(2026, 7, 12, 0, 0, 0, DateTimeKind.Utc));

    public SqliteRepositoriesTests()
    {
        _databasePath = Path.Combine(Path.GetTempPath(), $"allitems-test-{Guid.NewGuid():N}.db");
        _connectionFactory = new SqliteConnectionFactory($"Data Source={_databasePath}");
        new DatabaseInitializer(_connectionFactory).InitializeAsync().GetAwaiter().GetResult();
    }

    public void Dispose()
    {
        if (File.Exists(_databasePath))
        {
            File.Delete(_databasePath);
        }
    }

    [Fact]
    public async Task GetOrCreateAsync_SameWord_ReturnsSameEntry()
    {
        var repository = new WordRepository(_connectionFactory, _clock);

        var first = await repository.GetOrCreateAsync(Language.German, "hund");
        var second = await repository.GetOrCreateAsync(Language.German, "hund");

        Assert.Equal(first.Id, second.Id);
    }

    [Fact]
    public async Task AddTranslationAsync_MarkingPreferred_ClearsPreviousPreferred()
    {
        var repository = new WordRepository(_connectionFactory, _clock);
        var entry = await repository.GetOrCreateAsync(Language.German, "schloss");

        var first = await repository.AddTranslationAsync(entry.Id, Language.Afrikaans, "slot", isPreferred: true);
        var second = await repository.AddTranslationAsync(entry.Id, Language.Afrikaans, "kasteel", isPreferred: true);

        var translations = await repository.GetTranslationsAsync(entry.Id, Language.Afrikaans);

        Assert.Equal(2, translations.Count);
        Assert.True(translations.Single(t => t.Id == second.Id).IsPreferred);
        Assert.False(translations.Single(t => t.Id == first.Id).IsPreferred);
    }

    [Fact]
    public async Task SetPreferredAsync_SwitchesPreferredFlagBetweenSiblings()
    {
        var repository = new WordRepository(_connectionFactory, _clock);
        var entry = await repository.GetOrCreateAsync(Language.German, "bank");
        var bench = await repository.AddTranslationAsync(entry.Id, Language.Afrikaans, "bank", isPreferred: true);
        var financial = await repository.AddTranslationAsync(entry.Id, Language.Afrikaans, "bankinstelling", isPreferred: false);

        await repository.SetPreferredAsync(entry.Id, Language.Afrikaans, financial.Id);

        var translations = await repository.GetTranslationsAsync(entry.Id, Language.Afrikaans);
        Assert.True(translations.Single(t => t.Id == financial.Id).IsPreferred);
        Assert.False(translations.Single(t => t.Id == bench.Id).IsPreferred);
    }

    [Fact]
    public async Task DeleteTranslationAsync_RemovesOnlyThatTranslation()
    {
        var repository = new WordRepository(_connectionFactory, _clock);
        var entry = await repository.GetOrCreateAsync(Language.German, "mutter");
        var mother = await repository.AddTranslationAsync(entry.Id, Language.Afrikaans, "moeder", isPreferred: true);
        await repository.AddTranslationAsync(entry.Id, Language.Afrikaans, "moer", isPreferred: false);

        await repository.DeleteTranslationAsync(mother.Id);

        var remaining = await repository.GetTranslationsAsync(entry.Id, Language.Afrikaans);
        Assert.Single(remaining);
        Assert.Equal("moer", remaining[0].TargetText);
    }

    [Fact]
    public async Task UpdateTranslationTextAsync_ChangesStoredText()
    {
        var repository = new WordRepository(_connectionFactory, _clock);
        var entry = await repository.GetOrCreateAsync(Language.German, "katze");
        var translation = await repository.AddTranslationAsync(entry.Id, Language.Afrikaans, "wrong", isPreferred: true);

        await repository.UpdateTranslationTextAsync(translation.Id, "kat");

        var translations = await repository.GetTranslationsAsync(entry.Id, Language.Afrikaans);
        Assert.Equal("kat", translations.Single().TargetText);
    }

    [Fact]
    public async Task GetAllWithTranslationsAsync_IncludesWordsWithNoTranslationsYet()
    {
        var repository = new WordRepository(_connectionFactory, _clock);
        var withTranslation = await repository.GetOrCreateAsync(Language.German, "haus");
        await repository.AddTranslationAsync(withTranslation.Id, Language.Afrikaans, "huis", isPreferred: true);
        await repository.GetOrCreateAsync(Language.German, "ohnetranslation");

        var all = await repository.GetAllWithTranslationsAsync();

        Assert.Equal(2, all.Count);
        Assert.Contains(all, w => w.NormalizedText == "haus" && w.Translations.Count == 1);
        Assert.Contains(all, w => w.NormalizedText == "ohnetranslation" && w.Translations.Count == 0);
    }

    [Fact]
    public async Task SentenceTranslationRepository_SaveThenFind_RoundTrips()
    {
        var repository = new SentenceTranslationRepository(_connectionFactory, _clock);

        Assert.Null(await repository.FindAsync(Language.German, Language.Afrikaans, "Hallo Welt"));

        await repository.SaveAsync(Language.German, Language.Afrikaans, "Hallo Welt", "Hallo Wereld");

        Assert.Equal("Hallo Wereld", await repository.FindAsync(Language.German, Language.Afrikaans, "Hallo Welt"));
    }

    [Fact]
    public async Task SentenceTranslationRepository_SaveTwice_DoesNotThrow()
    {
        var repository = new SentenceTranslationRepository(_connectionFactory, _clock);

        await repository.SaveAsync(Language.German, Language.English, "Danke", "Thanks");
        await repository.SaveAsync(Language.German, Language.English, "Danke", "Thanks");

        Assert.Equal("Thanks", await repository.FindAsync(Language.German, Language.English, "Danke"));
    }

    [Fact]
    public async Task ApiUsageTracker_AccumulatesWithinSameMonth()
    {
        var tracker = new ApiUsageTracker(_connectionFactory, _clock);

        await tracker.RecordCharactersAsync(10);
        await tracker.RecordCharactersAsync(5);

        Assert.Equal(15, await tracker.GetCurrentMonthUsageAsync());
    }

    [Fact]
    public async Task ApiUsageTracker_DifferentMonths_TrackedSeparately()
    {
        var tracker = new ApiUsageTracker(_connectionFactory, _clock);

        await tracker.RecordCharactersAsync(10);
        _clock.UtcNow = _clock.UtcNow.AddMonths(1);
        await tracker.RecordCharactersAsync(7);

        Assert.Equal(7, await tracker.GetCurrentMonthUsageAsync());
    }
}

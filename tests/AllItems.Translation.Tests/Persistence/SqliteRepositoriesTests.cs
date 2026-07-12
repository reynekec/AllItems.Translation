using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Curriculum;
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

    [Fact]
    public async Task GetWordsWithPreferredTranslationAsync_OnlyReturnsPreferredMeaning()
    {
        var repository = new WordRepository(_connectionFactory, _clock);
        var entry = await repository.GetOrCreateAsync(Language.German, "bank");
        await repository.AddTranslationAsync(entry.Id, Language.Afrikaans, "bankinstelling", isPreferred: false);
        var preferred = await repository.AddTranslationAsync(entry.Id, Language.Afrikaans, "bank", isPreferred: true);

        var words = await repository.GetWordsWithPreferredTranslationAsync(Language.German, Language.Afrikaans);

        var word = Assert.Single(words);
        var translation = Assert.Single(word.Translations);
        Assert.Equal(preferred.Id, translation.Id);
        Assert.Equal("bank", translation.TargetText);
    }

    [Fact]
    public async Task GetWordsWithPreferredTranslationAsync_ExcludesWordsWithNoPreferredTranslationInThatLanguage()
    {
        var repository = new WordRepository(_connectionFactory, _clock);
        var entry = await repository.GetOrCreateAsync(Language.German, "haus");
        await repository.AddTranslationAsync(entry.Id, Language.English, "house", isPreferred: true);

        var words = await repository.GetWordsWithPreferredTranslationAsync(Language.German, Language.Afrikaans);

        Assert.Empty(words);
    }

    [Fact]
    public async Task ReviewStateRepository_UpsertThenGet_RoundTrips()
    {
        var wordRepository = new WordRepository(_connectionFactory, _clock);
        var entry = await wordRepository.GetOrCreateAsync(Language.German, "hund");
        var reviewRepository = new SqlReviewStateRepository(_connectionFactory);

        var state = new WordReviewState
        {
            WordEntryId = entry.Id,
            TargetLanguage = Language.Afrikaans,
            EasinessFactor = 2.6,
            IntervalDays = 6,
            Repetitions = 2,
            DueDateUtc = _clock.UtcNow.AddDays(6),
            LastReviewedUtc = _clock.UtcNow
        };
        await reviewRepository.UpsertAsync(state);

        var states = await reviewRepository.GetStatesAsync(Language.Afrikaans, [entry.Id]);

        var stored = Assert.Single(states.Values);
        Assert.Equal(2.6, stored.EasinessFactor);
        Assert.Equal(6, stored.IntervalDays);
        Assert.Equal(2, stored.Repetitions);
        Assert.Equal(state.DueDateUtc, stored.DueDateUtc);
    }

    [Fact]
    public async Task ReviewStateRepository_UpsertTwice_UpdatesInPlaceRatherThanDuplicating()
    {
        var wordRepository = new WordRepository(_connectionFactory, _clock);
        var entry = await wordRepository.GetOrCreateAsync(Language.German, "katze");
        var reviewRepository = new SqlReviewStateRepository(_connectionFactory);

        await reviewRepository.UpsertAsync(new WordReviewState { WordEntryId = entry.Id, TargetLanguage = Language.Afrikaans, Repetitions = 1 });
        await reviewRepository.UpsertAsync(new WordReviewState { WordEntryId = entry.Id, TargetLanguage = Language.Afrikaans, Repetitions = 2 });

        var states = await reviewRepository.GetStatesAsync(Language.Afrikaans, [entry.Id]);

        var stored = Assert.Single(states.Values);
        Assert.Equal(2, stored.Repetitions);
    }

    [Fact]
    public async Task ReviewStateRepository_UnknownWord_IsAbsentFromResults()
    {
        var reviewRepository = new SqlReviewStateRepository(_connectionFactory);

        var states = await reviewRepository.GetStatesAsync(Language.Afrikaans, [999]);

        Assert.Empty(states);
    }

    [Fact]
    public async Task ReviewStateRepository_LapseCount_RoundTrips()
    {
        var wordRepository = new WordRepository(_connectionFactory, _clock);
        var entry = await wordRepository.GetOrCreateAsync(Language.German, "vergessen");
        var reviewRepository = new SqlReviewStateRepository(_connectionFactory);

        await reviewRepository.UpsertAsync(new WordReviewState { WordEntryId = entry.Id, TargetLanguage = Language.Afrikaans, LapseCount = 3 });

        var states = await reviewRepository.GetStatesAsync(Language.Afrikaans, [entry.Id]);

        Assert.Equal(3, states[entry.Id].LapseCount);
    }

    [Fact]
    public async Task ReviewStateRepository_GetLeechesAsync_ReturnsOnlyWordsAtOrAboveThreshold()
    {
        var wordRepository = new WordRepository(_connectionFactory, _clock);
        var leechEntry = await wordRepository.GetOrCreateAsync(Language.German, "schwierig");
        var okEntry = await wordRepository.GetOrCreateAsync(Language.German, "einfach");
        var reviewRepository = new SqlReviewStateRepository(_connectionFactory);

        await reviewRepository.UpsertAsync(new WordReviewState { WordEntryId = leechEntry.Id, TargetLanguage = Language.Afrikaans, LapseCount = 3 });
        await reviewRepository.UpsertAsync(new WordReviewState { WordEntryId = okEntry.Id, TargetLanguage = Language.Afrikaans, LapseCount = 1 });

        var leeches = await reviewRepository.GetLeechesAsync(Language.Afrikaans, 3);

        var leech = Assert.Single(leeches);
        Assert.Equal(leechEntry.Id, leech.WordEntryId);
    }

    [Fact]
    public async Task WordRepository_SetStudyContentAsync_RoundTripsArticleSentenceAndHighlights()
    {
        var wordRepository = new WordRepository(_connectionFactory, _clock);
        var entry = await wordRepository.GetOrCreateAsync(Language.German, "buch");
        await wordRepository.AddTranslationAsync(entry.Id, Language.English, "book", isPreferred: true);

        await wordRepository.SetStudyContentAsync(
            entry.Id,
            "das",
            "Ich las das Buch.",
            [new SentenceHighlight("las", "Präteritum (simple past) of 'lesen'")]);

        var words = await wordRepository.GetWordsWithPreferredTranslationAsync(Language.German, Language.English);

        var word = Assert.Single(words, w => w.Id == entry.Id);
        Assert.Equal("das", word.Article);
        Assert.Equal("Ich las das Buch.", word.ExampleSentence);
        var highlight = Assert.Single(word.Highlights);
        Assert.Equal("las", highlight.Word);
        Assert.Equal("Präteritum (simple past) of 'lesen'", highlight.Reason);
    }

    [Fact]
    public async Task WordRepository_SetStudyContentAsync_CalledTwice_ReplacesHighlightsRatherThanAppending()
    {
        var wordRepository = new WordRepository(_connectionFactory, _clock);
        var entry = await wordRepository.GetOrCreateAsync(Language.German, "gehen");
        await wordRepository.AddTranslationAsync(entry.Id, Language.English, "to go", isPreferred: true);

        await wordRepository.SetStudyContentAsync(entry.Id, null, "Ich ging nach Hause.", [new SentenceHighlight("ging", "past tense")]);
        await wordRepository.SetStudyContentAsync(entry.Id, null, "Ich gehe nach Hause.", []);

        var words = await wordRepository.GetWordsByIdsAsync([entry.Id], Language.English);

        var word = Assert.Single(words);
        Assert.Equal("Ich gehe nach Hause.", word.ExampleSentence);
        Assert.Empty(word.Highlights);
    }

    [Fact]
    public async Task WordRepository_GetWordsByIdsAsync_UnknownId_ExcludedFromResults()
    {
        var wordRepository = new WordRepository(_connectionFactory, _clock);

        var words = await wordRepository.GetWordsByIdsAsync([999], Language.English);

        Assert.Empty(words);
    }

    [Fact]
    public async Task CurriculumProgressRepository_MarkCompleted_ThenQuery_ReturnsIt()
    {
        var repository = new SqlCurriculumProgressRepository(_connectionFactory, _clock);

        await repository.MarkExerciseCompletedAsync("a1-u1-e1");

        var completed = await repository.GetCompletedExerciseIdsAsync(["a1-u1-e1", "a1-u1-e2"]);

        Assert.Contains("a1-u1-e1", completed);
        Assert.DoesNotContain("a1-u1-e2", completed);
    }

    [Fact]
    public async Task CurriculumProgressRepository_MarkCompletedTwice_DoesNotThrow()
    {
        var repository = new SqlCurriculumProgressRepository(_connectionFactory, _clock);

        await repository.MarkExerciseCompletedAsync("a1-u1-e1");
        await repository.MarkExerciseCompletedAsync("a1-u1-e1");

        var completed = await repository.GetCompletedExerciseIdsAsync(["a1-u1-e1"]);
        Assert.Single(completed);
    }

    [Fact]
    public async Task CurriculumProgressRepository_EmptyIdList_ReturnsEmptySet()
    {
        var repository = new SqlCurriculumProgressRepository(_connectionFactory, _clock);

        var completed = await repository.GetCompletedExerciseIdsAsync([]);

        Assert.Empty(completed);
    }

    [Fact]
    public async Task VocabularyImportRepository_UnmarkedLevel_IsNotImported()
    {
        var repository = new SqlVocabularyImportRepository(_connectionFactory, _clock);

        Assert.False(await repository.IsLevelImportedAsync(CefrLevel.A1));
    }

    [Fact]
    public async Task VocabularyImportRepository_MarkLevelImported_ThenQuery_ReturnsTrue()
    {
        var repository = new SqlVocabularyImportRepository(_connectionFactory, _clock);

        await repository.MarkLevelImportedAsync(CefrLevel.A1);

        Assert.True(await repository.IsLevelImportedAsync(CefrLevel.A1));
        Assert.False(await repository.IsLevelImportedAsync(CefrLevel.A2));
    }

    [Fact]
    public async Task VocabularyImportRepository_MarkLevelImportedTwice_DoesNotThrow()
    {
        var repository = new SqlVocabularyImportRepository(_connectionFactory, _clock);

        await repository.MarkLevelImportedAsync(CefrLevel.B1);
        await repository.MarkLevelImportedAsync(CefrLevel.B1);

        Assert.True(await repository.IsLevelImportedAsync(CefrLevel.B1));
    }
}

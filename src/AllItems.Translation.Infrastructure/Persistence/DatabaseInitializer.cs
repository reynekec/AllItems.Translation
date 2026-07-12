namespace AllItems.Translation.Infrastructure.Persistence;

/// <summary>Creates the SQLite schema on first run - a hand-rolled stand-in for EF Core migrations.</summary>
public sealed class DatabaseInitializer(SqliteConnectionFactory connectionFactory)
{
    public Task InitializeAsync(CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            using var command = connection.CreateCommand();
            command.CommandText = """
                CREATE TABLE IF NOT EXISTS WordEntries (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Language INTEGER NOT NULL,
                    NormalizedText TEXT NOT NULL
                );
                CREATE UNIQUE INDEX IF NOT EXISTS IX_WordEntries_Language_NormalizedText
                    ON WordEntries (Language, NormalizedText);

                CREATE TABLE IF NOT EXISTS WordTranslations (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    WordEntryId INTEGER NOT NULL REFERENCES WordEntries (Id) ON DELETE CASCADE,
                    TargetLanguage INTEGER NOT NULL,
                    TargetText TEXT NOT NULL,
                    IsPreferred INTEGER NOT NULL DEFAULT 0,
                    UsageCount INTEGER NOT NULL DEFAULT 0,
                    CreatedAtUtc TEXT NOT NULL
                );
                CREATE INDEX IF NOT EXISTS IX_WordTranslations_WordEntryId_TargetLanguage
                    ON WordTranslations (WordEntryId, TargetLanguage);

                CREATE TABLE IF NOT EXISTS SentenceTranslations (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    SourceLanguage INTEGER NOT NULL,
                    TargetLanguage INTEGER NOT NULL,
                    NormalizedSourceText TEXT NOT NULL,
                    TranslatedText TEXT NOT NULL,
                    CreatedAtUtc TEXT NOT NULL
                );
                CREATE UNIQUE INDEX IF NOT EXISTS IX_SentenceTranslations_Source_Target_Text
                    ON SentenceTranslations (SourceLanguage, TargetLanguage, NormalizedSourceText);

                CREATE TABLE IF NOT EXISTS ApiUsageRecords (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    YearMonth TEXT NOT NULL,
                    CharacterCount INTEGER NOT NULL DEFAULT 0
                );
                CREATE UNIQUE INDEX IF NOT EXISTS IX_ApiUsageRecords_YearMonth
                    ON ApiUsageRecords (YearMonth);

                CREATE TABLE IF NOT EXISTS ReviewStates (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    WordEntryId INTEGER NOT NULL REFERENCES WordEntries (Id) ON DELETE CASCADE,
                    TargetLanguage INTEGER NOT NULL,
                    EasinessFactor REAL NOT NULL DEFAULT 2.5,
                    IntervalDays INTEGER NOT NULL DEFAULT 0,
                    Repetitions INTEGER NOT NULL DEFAULT 0,
                    DueDateUtc TEXT NULL,
                    LastReviewedUtc TEXT NULL
                );
                CREATE UNIQUE INDEX IF NOT EXISTS IX_ReviewStates_WordEntryId_TargetLanguage
                    ON ReviewStates (WordEntryId, TargetLanguage);

                CREATE TABLE IF NOT EXISTS CurriculumExerciseProgress (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ExerciseId TEXT NOT NULL,
                    CompletedAtUtc TEXT NOT NULL
                );
                CREATE UNIQUE INDEX IF NOT EXISTS IX_CurriculumExerciseProgress_ExerciseId
                    ON CurriculumExerciseProgress (ExerciseId);

                CREATE TABLE IF NOT EXISTS VocabularyLevelImports (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Level INTEGER NOT NULL,
                    ImportedAtUtc TEXT NOT NULL
                );
                CREATE UNIQUE INDEX IF NOT EXISTS IX_VocabularyLevelImports_Level
                    ON VocabularyLevelImports (Level);
                """;
            command.ExecuteNonQuery();
        }, cancellationToken);
}

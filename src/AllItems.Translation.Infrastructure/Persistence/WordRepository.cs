using System.Globalization;
using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Domain;
using Microsoft.Data.Sqlite;

namespace AllItems.Translation.Infrastructure.Persistence;

public sealed class WordRepository(SqliteConnectionFactory connectionFactory, IClock clock) : IWordRepository
{
    public Task<WordEntry> GetOrCreateAsync(Language language, string normalizedText, CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            using (var insert = connection.CreateCommand())
            {
                insert.CommandText = """
                    INSERT OR IGNORE INTO WordEntries (Language, NormalizedText)
                    VALUES ($language, $text);
                    """;
                insert.Parameters.AddWithValue("$language", (int)language);
                insert.Parameters.AddWithValue("$text", normalizedText);
                insert.ExecuteNonQuery();
            }

            using var select = connection.CreateCommand();
            select.CommandText = "SELECT Id FROM WordEntries WHERE Language = $language AND NormalizedText = $text;";
            select.Parameters.AddWithValue("$language", (int)language);
            select.Parameters.AddWithValue("$text", normalizedText);

            var id = Convert.ToInt32(select.ExecuteScalar(), CultureInfo.InvariantCulture);
            return new WordEntry { Id = id, Language = language, NormalizedText = normalizedText };
        }, cancellationToken);

    public Task<IReadOnlyList<WordTranslation>> GetTranslationsAsync(int wordEntryId, Language targetLanguage, CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            using var command = connection.CreateCommand();
            command.CommandText = """
                SELECT Id, TargetText, IsPreferred, UsageCount, CreatedAtUtc
                FROM WordTranslations
                WHERE WordEntryId = $wordEntryId AND TargetLanguage = $targetLanguage
                ORDER BY IsPreferred DESC, Id ASC;
                """;
            command.Parameters.AddWithValue("$wordEntryId", wordEntryId);
            command.Parameters.AddWithValue("$targetLanguage", (int)targetLanguage);

            var results = new List<WordTranslation>();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                results.Add(ReadTranslation(reader, wordEntryId, targetLanguage));
            }

            return (IReadOnlyList<WordTranslation>)results;
        }, cancellationToken);

    public Task<WordTranslation> AddTranslationAsync(int wordEntryId, Language targetLanguage, string targetText, bool isPreferred, CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            using var transaction = connection.BeginTransaction();

            if (isPreferred)
            {
                ClearPreferred(connection, transaction, wordEntryId, targetLanguage);
            }

            var createdAtUtc = clock.UtcNow;

            using (var insert = connection.CreateCommand())
            {
                insert.Transaction = transaction;
                insert.CommandText = """
                    INSERT INTO WordTranslations (WordEntryId, TargetLanguage, TargetText, IsPreferred, UsageCount, CreatedAtUtc)
                    VALUES ($wordEntryId, $targetLanguage, $targetText, $isPreferred, 0, $createdAtUtc);
                    SELECT last_insert_rowid();
                    """;
                insert.Parameters.AddWithValue("$wordEntryId", wordEntryId);
                insert.Parameters.AddWithValue("$targetLanguage", (int)targetLanguage);
                insert.Parameters.AddWithValue("$targetText", targetText);
                insert.Parameters.AddWithValue("$isPreferred", isPreferred ? 1 : 0);
                insert.Parameters.AddWithValue("$createdAtUtc", createdAtUtc.ToString("o", CultureInfo.InvariantCulture));

                var newId = Convert.ToInt32(insert.ExecuteScalar(), CultureInfo.InvariantCulture);

                transaction.Commit();

                return new WordTranslation
                {
                    Id = newId,
                    WordEntryId = wordEntryId,
                    TargetLanguage = targetLanguage,
                    TargetText = targetText,
                    IsPreferred = isPreferred,
                    UsageCount = 0,
                    CreatedAtUtc = createdAtUtc
                };
            }
        }, cancellationToken);

    public Task SetPreferredAsync(int wordEntryId, Language targetLanguage, int translationId, CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            using var command = connection.CreateCommand();
            command.CommandText = """
                UPDATE WordTranslations
                SET IsPreferred = CASE WHEN Id = $translationId THEN 1 ELSE 0 END
                WHERE WordEntryId = $wordEntryId AND TargetLanguage = $targetLanguage;
                """;
            command.Parameters.AddWithValue("$translationId", translationId);
            command.Parameters.AddWithValue("$wordEntryId", wordEntryId);
            command.Parameters.AddWithValue("$targetLanguage", (int)targetLanguage);
            command.ExecuteNonQuery();
        }, cancellationToken);

    public Task<IReadOnlyList<WordEntry>> GetAllWithTranslationsAsync(CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            using var command = connection.CreateCommand();
            command.CommandText = """
                SELECT we.Id, we.Language, we.NormalizedText,
                       wt.Id, wt.TargetLanguage, wt.TargetText, wt.IsPreferred, wt.UsageCount, wt.CreatedAtUtc
                FROM WordEntries we
                LEFT JOIN WordTranslations wt ON wt.WordEntryId = we.Id
                ORDER BY we.Language, we.NormalizedText, wt.TargetLanguage;
                """;

            var entriesById = new Dictionary<int, WordEntry>();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var entryId = reader.GetInt32(0);
                if (!entriesById.TryGetValue(entryId, out var entry))
                {
                    entry = new WordEntry
                    {
                        Id = entryId,
                        Language = (Language)reader.GetInt32(1),
                        NormalizedText = reader.GetString(2)
                    };
                    entriesById.Add(entryId, entry);
                }

                if (!reader.IsDBNull(3))
                {
                    entry.Translations.Add(new WordTranslation
                    {
                        Id = reader.GetInt32(3),
                        WordEntryId = entryId,
                        TargetLanguage = (Language)reader.GetInt32(4),
                        TargetText = reader.GetString(5),
                        IsPreferred = reader.GetInt64(6) != 0,
                        UsageCount = reader.GetInt32(7),
                        CreatedAtUtc = ParseUtc(reader.GetString(8))
                    });
                }
            }

            return (IReadOnlyList<WordEntry>)entriesById.Values.ToList();
        }, cancellationToken);

    public Task DeleteTranslationAsync(int translationId, CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM WordTranslations WHERE Id = $id;";
            command.Parameters.AddWithValue("$id", translationId);
            command.ExecuteNonQuery();
        }, cancellationToken);

    public Task UpdateTranslationTextAsync(int translationId, string newText, CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE WordTranslations SET TargetText = $text WHERE Id = $id;";
            command.Parameters.AddWithValue("$text", newText);
            command.Parameters.AddWithValue("$id", translationId);
            command.ExecuteNonQuery();
        }, cancellationToken);

    private static void ClearPreferred(SqliteConnection connection, SqliteTransaction transaction, int wordEntryId, Language targetLanguage)
    {
        using var clear = connection.CreateCommand();
        clear.Transaction = transaction;
        clear.CommandText = """
            UPDATE WordTranslations SET IsPreferred = 0
            WHERE WordEntryId = $wordEntryId AND TargetLanguage = $targetLanguage;
            """;
        clear.Parameters.AddWithValue("$wordEntryId", wordEntryId);
        clear.Parameters.AddWithValue("$targetLanguage", (int)targetLanguage);
        clear.ExecuteNonQuery();
    }

    private static WordTranslation ReadTranslation(SqliteDataReader reader, int wordEntryId, Language targetLanguage) => new()
    {
        Id = reader.GetInt32(0),
        WordEntryId = wordEntryId,
        TargetLanguage = targetLanguage,
        TargetText = reader.GetString(1),
        IsPreferred = reader.GetInt64(2) != 0,
        UsageCount = reader.GetInt32(3),
        CreatedAtUtc = ParseUtc(reader.GetString(4))
    };

    private static DateTime ParseUtc(string value) =>
        DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
}

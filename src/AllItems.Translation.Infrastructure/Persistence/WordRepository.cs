using System.Globalization;
using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Curriculum;
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
            select.CommandText = "SELECT Id, Article, ExampleSentence FROM WordEntries WHERE Language = $language AND NormalizedText = $text;";
            select.Parameters.AddWithValue("$language", (int)language);
            select.Parameters.AddWithValue("$text", normalizedText);

            using var reader = select.ExecuteReader();
            reader.Read();
            return new WordEntry
            {
                Id = reader.GetInt32(0),
                Language = language,
                NormalizedText = normalizedText,
                Article = reader.IsDBNull(1) ? null : reader.GetString(1),
                ExampleSentence = reader.IsDBNull(2) ? null : reader.GetString(2)
            };
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
                SELECT we.Id, we.Language, we.NormalizedText, we.Article, we.ExampleSentence,
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
                        NormalizedText = reader.GetString(2),
                        Article = reader.IsDBNull(3) ? null : reader.GetString(3),
                        ExampleSentence = reader.IsDBNull(4) ? null : reader.GetString(4)
                    };
                    entriesById.Add(entryId, entry);
                }

                if (!reader.IsDBNull(5))
                {
                    entry.Translations.Add(new WordTranslation
                    {
                        Id = reader.GetInt32(5),
                        WordEntryId = entryId,
                        TargetLanguage = (Language)reader.GetInt32(6),
                        TargetText = reader.GetString(7),
                        IsPreferred = reader.GetInt64(8) != 0,
                        UsageCount = reader.GetInt32(9),
                        CreatedAtUtc = ParseUtc(reader.GetString(10))
                    });
                }
            }

            return (IReadOnlyList<WordEntry>)entriesById.Values.ToList();
        }, cancellationToken);

    public Task<IReadOnlyList<WordEntry>> GetWordsWithPreferredTranslationAsync(Language sourceLanguage, Language targetLanguage, CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            // The bulk import stores both directions (German->X and X->German) as their own WordEntries,
            // so a study pair is a direct lookup: source-language entries whose preferred translation is
            // into the target language. AttachBackContent then resolves the answer's own sentence.
            var wordLanguage = sourceLanguage;
            var translationLanguage = targetLanguage;

            using var command = connection.CreateCommand();
            command.CommandText = """
                SELECT we.Id, we.NormalizedText, we.Article, we.ExampleSentence, wt.Id, wt.TargetText, wt.IsPreferred, wt.UsageCount, wt.CreatedAtUtc
                FROM WordEntries we
                JOIN WordTranslations wt ON wt.WordEntryId = we.Id
                WHERE we.Language = $wordLanguage AND wt.TargetLanguage = $translationLanguage AND wt.IsPreferred = 1;
                """;
            command.Parameters.AddWithValue("$wordLanguage", (int)wordLanguage);
            command.Parameters.AddWithValue("$translationLanguage", (int)translationLanguage);

            var results = new List<WordEntry>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var entryId = reader.GetInt32(0);
                    var entry = new WordEntry
                    {
                        Id = entryId,
                        Language = wordLanguage,
                        NormalizedText = reader.GetString(1),
                        Article = reader.IsDBNull(2) ? null : reader.GetString(2),
                        ExampleSentence = reader.IsDBNull(3) ? null : reader.GetString(3)
                    };
                    entry.Translations.Add(new WordTranslation
                    {
                        Id = reader.GetInt32(4),
                        WordEntryId = entryId,
                        TargetLanguage = translationLanguage,
                        TargetText = reader.GetString(5),
                        IsPreferred = reader.GetInt64(6) != 0,
                        UsageCount = reader.GetInt32(7),
                        CreatedAtUtc = ParseUtc(reader.GetString(8))
                    });
                    results.Add(entry);
                }
            }

            AttachHighlights(connection, results);
            AttachBackContent(connection, results, targetLanguage);
            return (IReadOnlyList<WordEntry>)results;
        }, cancellationToken);

    public Task<IReadOnlyList<WordEntry>> GetWordsByIdsAsync(IReadOnlyCollection<int> wordEntryIds, Language targetLanguage, CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            var results = new List<WordEntry>();
            if (wordEntryIds.Count == 0)
            {
                return (IReadOnlyList<WordEntry>)results;
            }

            var translationLanguage = targetLanguage;
            var ids = wordEntryIds.ToList();
            var parameterNames = ids.Select((_, index) => $"$id{index}").ToList();

            using var command = connection.CreateCommand();
            command.CommandText = $"""
                SELECT we.Id, we.Language, we.NormalizedText, we.Article, we.ExampleSentence,
                       wt.Id, wt.TargetText, wt.IsPreferred, wt.UsageCount, wt.CreatedAtUtc
                FROM WordEntries we
                LEFT JOIN WordTranslations wt ON wt.WordEntryId = we.Id AND wt.TargetLanguage = $translationLanguage AND wt.IsPreferred = 1
                WHERE we.Id IN ({string.Join(",", parameterNames)});
                """;
            command.Parameters.AddWithValue("$translationLanguage", (int)translationLanguage);
            for (var i = 0; i < ids.Count; i++)
            {
                command.Parameters.AddWithValue(parameterNames[i], ids[i]);
            }

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var entryId = reader.GetInt32(0);
                    var entry = new WordEntry
                    {
                        Id = entryId,
                        Language = (Language)reader.GetInt32(1),
                        NormalizedText = reader.GetString(2),
                        Article = reader.IsDBNull(3) ? null : reader.GetString(3),
                        ExampleSentence = reader.IsDBNull(4) ? null : reader.GetString(4)
                    };

                    if (!reader.IsDBNull(5))
                    {
                        entry.Translations.Add(new WordTranslation
                        {
                            Id = reader.GetInt32(5),
                            WordEntryId = entryId,
                            TargetLanguage = translationLanguage,
                            TargetText = reader.GetString(6),
                            IsPreferred = reader.GetInt64(7) != 0,
                            UsageCount = reader.GetInt32(8),
                            CreatedAtUtc = ParseUtc(reader.GetString(9))
                        });
                    }

                    results.Add(entry);
                }
            }

            AttachHighlights(connection, results);
            return (IReadOnlyList<WordEntry>)results;
        }, cancellationToken);

    public Task SetStudyContentAsync(int wordEntryId, string? article, string exampleSentence, IReadOnlyList<SentenceHighlight> highlights, CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            using var transaction = connection.BeginTransaction();

            using (var update = connection.CreateCommand())
            {
                update.Transaction = transaction;
                update.CommandText = "UPDATE WordEntries SET Article = $article, ExampleSentence = $sentence WHERE Id = $id;";
                update.Parameters.AddWithValue("$article", (object?)article ?? DBNull.Value);
                update.Parameters.AddWithValue("$sentence", exampleSentence);
                update.Parameters.AddWithValue("$id", wordEntryId);
                update.ExecuteNonQuery();
            }

            using (var delete = connection.CreateCommand())
            {
                delete.Transaction = transaction;
                delete.CommandText = "DELETE FROM WordSentenceHighlights WHERE WordEntryId = $id;";
                delete.Parameters.AddWithValue("$id", wordEntryId);
                delete.ExecuteNonQuery();
            }

            for (var i = 0; i < highlights.Count; i++)
            {
                using var insert = connection.CreateCommand();
                insert.Transaction = transaction;
                insert.CommandText = """
                    INSERT INTO WordSentenceHighlights (WordEntryId, WordText, Reason, Position)
                    VALUES ($wordEntryId, $word, $reason, $position);
                    """;
                insert.Parameters.AddWithValue("$wordEntryId", wordEntryId);
                insert.Parameters.AddWithValue("$word", highlights[i].Word);
                insert.Parameters.AddWithValue("$reason", highlights[i].Reason);
                insert.Parameters.AddWithValue("$position", i);
                insert.ExecuteNonQuery();
            }

            transaction.Commit();
        }, cancellationToken);

    public Task ImportWordsAsync(Language language, IReadOnlyList<VocabularyWord> words, CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            using var transaction = connection.BeginTransaction();

            foreach (var word in words)
            {
                // Forward direction (German -> English): the German entry carries the article,
                // example sentence and highlights so it can be studied with full context.
                var germanEntryId = GetOrCreateEntryId(connection, transaction, language, word.German.ToLowerInvariant());
                AddTranslationIfMissing(connection, transaction, germanEntryId, Language.English, word.English);

                if (!string.IsNullOrWhiteSpace(word.ExampleSentence))
                {
                    ReplaceStudyContent(connection, transaction, germanEntryId, word.Article, word.ExampleSentence, word.Highlights ?? []);
                }

                // Reverse direction (English -> German) so the same words are quizzable both ways.
                // The article travels with the answer ("der Apfel") to teach gender. The English entry
                // carries its OWN English example sentence (not a copy of the German one) so that when
                // English is the source it shows an English sentence on the front. Article stays null
                // here: it belongs to the German answer, and prepending it would read as "der apple".
                var englishEntryId = GetOrCreateEntryId(connection, transaction, Language.English, word.English.ToLowerInvariant());
                var germanAnswer = word.Article is null ? word.German : $"{word.Article} {word.German}";
                var storedAsPreferred = AddTranslationIfMissing(connection, transaction, englishEntryId, language, germanAnswer);

                if (storedAsPreferred && !string.IsNullOrWhiteSpace(word.EnglishExampleSentence))
                {
                    ReplaceStudyContent(connection, transaction, englishEntryId, article: null, word.EnglishExampleSentence, word.EnglishHighlights ?? []);
                }
            }

            transaction.Commit();
        }, cancellationToken);

    /// <summary>
    /// Adds <paramref name="targetText"/> as a translation unless an equal one already exists.
    /// Returns true when it was inserted as the entry's first (preferred) translation, which the
    /// reverse import uses to decide whether to attach the matching example sentence.
    /// </summary>
    private bool AddTranslationIfMissing(SqliteConnection connection, SqliteTransaction transaction, int wordEntryId, Language targetLanguage, string targetText)
    {
        var existing = GetTranslationTexts(connection, transaction, wordEntryId, targetLanguage);
        if (existing.Any(t => string.Equals(t, targetText, StringComparison.OrdinalIgnoreCase)))
        {
            return false;
        }

        var isPreferred = existing.Count == 0;
        InsertTranslation(connection, transaction, wordEntryId, targetLanguage, targetText, isPreferred, clock.UtcNow);
        return isPreferred;
    }

    private static int GetOrCreateEntryId(SqliteConnection connection, SqliteTransaction transaction, Language language, string normalizedText)
    {
        using (var insert = connection.CreateCommand())
        {
            insert.Transaction = transaction;
            insert.CommandText = """
                INSERT OR IGNORE INTO WordEntries (Language, NormalizedText)
                VALUES ($language, $text);
                """;
            insert.Parameters.AddWithValue("$language", (int)language);
            insert.Parameters.AddWithValue("$text", normalizedText);
            insert.ExecuteNonQuery();
        }

        using var select = connection.CreateCommand();
        select.Transaction = transaction;
        select.CommandText = "SELECT Id FROM WordEntries WHERE Language = $language AND NormalizedText = $text;";
        select.Parameters.AddWithValue("$language", (int)language);
        select.Parameters.AddWithValue("$text", normalizedText);
        return Convert.ToInt32(select.ExecuteScalar(), CultureInfo.InvariantCulture);
    }

    private static List<string> GetTranslationTexts(SqliteConnection connection, SqliteTransaction transaction, int wordEntryId, Language targetLanguage)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = """
            SELECT TargetText FROM WordTranslations
            WHERE WordEntryId = $wordEntryId AND TargetLanguage = $targetLanguage;
            """;
        command.Parameters.AddWithValue("$wordEntryId", wordEntryId);
        command.Parameters.AddWithValue("$targetLanguage", (int)targetLanguage);

        var results = new List<string>();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            results.Add(reader.GetString(0));
        }

        return results;
    }

    private static void InsertTranslation(SqliteConnection connection, SqliteTransaction transaction, int wordEntryId, Language targetLanguage, string targetText, bool isPreferred, DateTime createdAtUtc)
    {
        if (isPreferred)
        {
            ClearPreferred(connection, transaction, wordEntryId, targetLanguage);
        }

        using var insert = connection.CreateCommand();
        insert.Transaction = transaction;
        insert.CommandText = """
            INSERT INTO WordTranslations (WordEntryId, TargetLanguage, TargetText, IsPreferred, UsageCount, CreatedAtUtc)
            VALUES ($wordEntryId, $targetLanguage, $targetText, $isPreferred, 0, $createdAtUtc);
            """;
        insert.Parameters.AddWithValue("$wordEntryId", wordEntryId);
        insert.Parameters.AddWithValue("$targetLanguage", (int)targetLanguage);
        insert.Parameters.AddWithValue("$targetText", targetText);
        insert.Parameters.AddWithValue("$isPreferred", isPreferred ? 1 : 0);
        insert.Parameters.AddWithValue("$createdAtUtc", createdAtUtc.ToString("o", CultureInfo.InvariantCulture));
        insert.ExecuteNonQuery();
    }

    private static void ReplaceStudyContent(SqliteConnection connection, SqliteTransaction transaction, int wordEntryId, string? article, string exampleSentence, IReadOnlyList<SentenceHighlight> highlights)
    {
        using (var update = connection.CreateCommand())
        {
            update.Transaction = transaction;
            update.CommandText = "UPDATE WordEntries SET Article = $article, ExampleSentence = $sentence WHERE Id = $id;";
            update.Parameters.AddWithValue("$article", (object?)article ?? DBNull.Value);
            update.Parameters.AddWithValue("$sentence", exampleSentence);
            update.Parameters.AddWithValue("$id", wordEntryId);
            update.ExecuteNonQuery();
        }

        using (var delete = connection.CreateCommand())
        {
            delete.Transaction = transaction;
            delete.CommandText = "DELETE FROM WordSentenceHighlights WHERE WordEntryId = $id;";
            delete.Parameters.AddWithValue("$id", wordEntryId);
            delete.ExecuteNonQuery();
        }

        for (var i = 0; i < highlights.Count; i++)
        {
            using var insert = connection.CreateCommand();
            insert.Transaction = transaction;
            insert.CommandText = """
                INSERT INTO WordSentenceHighlights (WordEntryId, WordText, Reason, Position)
                VALUES ($wordEntryId, $word, $reason, $position);
                """;
            insert.Parameters.AddWithValue("$wordEntryId", wordEntryId);
            insert.Parameters.AddWithValue("$word", highlights[i].Word);
            insert.Parameters.AddWithValue("$reason", highlights[i].Reason);
            insert.Parameters.AddWithValue("$position", i);
            insert.ExecuteNonQuery();
        }
    }

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

    private static void AttachHighlights(SqliteConnection connection, List<WordEntry> entries)
    {
        if (entries.Count == 0)
        {
            return;
        }

        var highlightsByWordId = LoadHighlights(connection, entries.Select(e => e.Id).ToList());
        foreach (var entry in entries)
        {
            if (highlightsByWordId.TryGetValue(entry.Id, out var highlights))
            {
                entry.Highlights.AddRange(highlights);
            }
        }
    }

    /// <summary>
    /// Resolves the destination-language example sentence for each entry's preferred translation, so a
    /// study card can show the answer in context. The translation text (e.g. "really" or "der Apfel")
    /// is normalized back to a word entry key and looked up among <paramref name="targetLanguage"/>
    /// entries; its own sentence/highlights are copied onto the translation.
    /// </summary>
    private static void AttachBackContent(SqliteConnection connection, List<WordEntry> entries, Language targetLanguage)
    {
        var keyByEntryId = new Dictionary<int, string>();
        var keys = new HashSet<string>(StringComparer.Ordinal);
        foreach (var entry in entries)
        {
            if (entry.Translations.Count == 0)
            {
                continue;
            }

            var key = NormalizeForLookup(entry.Translations[0].TargetText);
            keyByEntryId[entry.Id] = key;
            keys.Add(key);
        }

        if (keys.Count == 0)
        {
            return;
        }

        var keyList = keys.ToList();
        var parameterNames = keyList.Select((_, index) => $"$bk{index}").ToList();

        var destByKey = new Dictionary<string, (int Id, string? ExampleSentence)>(StringComparer.Ordinal);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = $"""
                SELECT Id, NormalizedText, ExampleSentence
                FROM WordEntries
                WHERE Language = $language AND NormalizedText IN ({string.Join(",", parameterNames)});
                """;
            command.Parameters.AddWithValue("$language", (int)targetLanguage);
            for (var i = 0; i < keyList.Count; i++)
            {
                command.Parameters.AddWithValue(parameterNames[i], keyList[i]);
            }

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                destByKey[reader.GetString(1)] = (reader.GetInt32(0), reader.IsDBNull(2) ? null : reader.GetString(2));
            }
        }

        if (destByKey.Count == 0)
        {
            return;
        }

        var highlightsByDestId = LoadHighlights(connection, destByKey.Values.Select(v => v.Id).Distinct().ToList());
        foreach (var entry in entries)
        {
            if (!keyByEntryId.TryGetValue(entry.Id, out var key) || !destByKey.TryGetValue(key, out var dest))
            {
                continue;
            }

            var translation = entry.Translations[0];
            translation.ExampleSentence = dest.ExampleSentence;
            if (highlightsByDestId.TryGetValue(dest.Id, out var highlights))
            {
                translation.Highlights.AddRange(highlights);
            }
        }
    }

    /// <summary>Strips a leading German article and lower-cases, mapping a translation back to a word entry key.</summary>
    private static string NormalizeForLookup(string text)
    {
        var trimmed = text.Trim();
        foreach (var article in (ReadOnlySpan<string>)["der ", "die ", "das "])
        {
            if (trimmed.StartsWith(article, StringComparison.OrdinalIgnoreCase))
            {
                trimmed = trimmed[article.Length..];
                break;
            }
        }

        return trimmed.ToLowerInvariant();
    }

    private static Dictionary<int, List<SentenceHighlight>> LoadHighlights(SqliteConnection connection, IReadOnlyList<int> wordEntryIds)
    {
        var highlightsByWordId = new Dictionary<int, List<SentenceHighlight>>();
        if (wordEntryIds.Count == 0)
        {
            return highlightsByWordId;
        }

        var parameterNames = wordEntryIds.Select((_, index) => $"$hid{index}").ToList();

        using var command = connection.CreateCommand();
        command.CommandText = $"""
            SELECT WordEntryId, WordText, Reason
            FROM WordSentenceHighlights
            WHERE WordEntryId IN ({string.Join(",", parameterNames)})
            ORDER BY WordEntryId, Position;
            """;
        for (var i = 0; i < wordEntryIds.Count; i++)
        {
            command.Parameters.AddWithValue(parameterNames[i], wordEntryIds[i]);
        }

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var wordEntryId = reader.GetInt32(0);
            if (!highlightsByWordId.TryGetValue(wordEntryId, out var list))
            {
                list = [];
                highlightsByWordId[wordEntryId] = list;
            }

            list.Add(new SentenceHighlight(reader.GetString(1), reader.GetString(2)));
        }

        return highlightsByWordId;
    }

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

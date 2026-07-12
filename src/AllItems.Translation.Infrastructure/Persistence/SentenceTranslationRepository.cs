using System.Globalization;
using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Domain;

namespace AllItems.Translation.Infrastructure.Persistence;

public sealed class SentenceTranslationRepository(SqliteConnectionFactory connectionFactory, IClock clock) : ISentenceTranslationRepository
{
    public Task<string?> FindAsync(Language source, Language target, string normalizedSentence, CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            using var command = connection.CreateCommand();
            command.CommandText = """
                SELECT TranslatedText FROM SentenceTranslations
                WHERE SourceLanguage = $source AND TargetLanguage = $target AND NormalizedSourceText = $text;
                """;
            command.Parameters.AddWithValue("$source", (int)source);
            command.Parameters.AddWithValue("$target", (int)target);
            command.Parameters.AddWithValue("$text", normalizedSentence);

            return command.ExecuteScalar() as string;
        }, cancellationToken);

    public Task SaveAsync(Language source, Language target, string normalizedSentence, string translatedText, CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            using var command = connection.CreateCommand();
            command.CommandText = """
                INSERT OR IGNORE INTO SentenceTranslations (SourceLanguage, TargetLanguage, NormalizedSourceText, TranslatedText, CreatedAtUtc)
                VALUES ($source, $target, $text, $translated, $createdAtUtc);
                """;
            command.Parameters.AddWithValue("$source", (int)source);
            command.Parameters.AddWithValue("$target", (int)target);
            command.Parameters.AddWithValue("$text", normalizedSentence);
            command.Parameters.AddWithValue("$translated", translatedText);
            command.Parameters.AddWithValue("$createdAtUtc", clock.UtcNow.ToString("o", CultureInfo.InvariantCulture));
            command.ExecuteNonQuery();
        }, cancellationToken);
}

using System.Globalization;
using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Curriculum;

namespace AllItems.Translation.Infrastructure.Persistence;

public sealed class SqlVocabularyImportRepository(SqliteConnectionFactory connectionFactory, IClock clock) : IVocabularyImportRepository
{
    public Task<bool> IsLevelImportedAsync(CefrLevel level, CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT 1 FROM VocabularyLevelImports WHERE Level = $level;";
            command.Parameters.AddWithValue("$level", (int)level);
            return command.ExecuteScalar() is not null;
        }, cancellationToken);

    public Task MarkLevelImportedAsync(CefrLevel level, CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            using var command = connection.CreateCommand();
            command.CommandText = """
                INSERT OR IGNORE INTO VocabularyLevelImports (Level, ImportedAtUtc)
                VALUES ($level, $importedAtUtc);
                """;
            command.Parameters.AddWithValue("$level", (int)level);
            command.Parameters.AddWithValue("$importedAtUtc", clock.UtcNow.ToString("o", CultureInfo.InvariantCulture));
            command.ExecuteNonQuery();
        }, cancellationToken);
}

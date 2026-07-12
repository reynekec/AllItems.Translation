using AllItems.Translation.Core.Abstractions;

namespace AllItems.Translation.Infrastructure.Persistence;

public sealed class ApiUsageTracker(SqliteConnectionFactory connectionFactory, IClock clock) : IApiUsageTracker
{
    public Task RecordCharactersAsync(int characterCount, CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            var yearMonth = clock.UtcNow.ToString("yyyy-MM");

            using (var insert = connection.CreateCommand())
            {
                insert.CommandText = "INSERT OR IGNORE INTO ApiUsageRecords (YearMonth, CharacterCount) VALUES ($yearMonth, 0);";
                insert.Parameters.AddWithValue("$yearMonth", yearMonth);
                insert.ExecuteNonQuery();
            }

            using var update = connection.CreateCommand();
            update.CommandText = "UPDATE ApiUsageRecords SET CharacterCount = CharacterCount + $count WHERE YearMonth = $yearMonth;";
            update.Parameters.AddWithValue("$count", characterCount);
            update.Parameters.AddWithValue("$yearMonth", yearMonth);
            update.ExecuteNonQuery();
        }, cancellationToken);

    public Task<long> GetCurrentMonthUsageAsync(CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            var yearMonth = clock.UtcNow.ToString("yyyy-MM");

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT CharacterCount FROM ApiUsageRecords WHERE YearMonth = $yearMonth;";
            command.Parameters.AddWithValue("$yearMonth", yearMonth);

            var result = command.ExecuteScalar();
            return result is null ? 0L : Convert.ToInt64(result);
        }, cancellationToken);
}

using Microsoft.Data.Sqlite;

namespace AllItems.Translation.Infrastructure.Persistence;

/// <summary>
/// Opens configured SQLite connections and runs work against them on a thread-pool thread.
/// SQLite's native driver is synchronous under the hood, so every unit of work is wrapped in
/// <see cref="Task.Run(Action)"/> here rather than relying on ADO.NET's async surface - this is
/// what actually keeps the UI thread free, regardless of how faithfully the provider implements
/// its *Async methods.
/// </summary>
public sealed class SqliteConnectionFactory(string connectionString)
{
    public Task<T> RunAsync<T>(Func<SqliteConnection, T> work, CancellationToken cancellationToken = default) =>
        Task.Run(() =>
        {
            using var connection = Open();
            return work(connection);
        }, cancellationToken);

    public Task RunAsync(Action<SqliteConnection> work, CancellationToken cancellationToken = default) =>
        Task.Run(() =>
        {
            using var connection = Open();
            work(connection);
        }, cancellationToken);

    private SqliteConnection Open()
    {
        var connection = new SqliteConnection(connectionString);
        connection.Open();

        using var pragma = connection.CreateCommand();
        pragma.CommandText = """
            PRAGMA foreign_keys = ON;
            PRAGMA journal_mode = WAL;
            PRAGMA synchronous = NORMAL;
            """;
        pragma.ExecuteNonQuery();

        return connection;
    }
}

using System.Globalization;
using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Domain;

namespace AllItems.Translation.Infrastructure.Persistence;

public sealed class SqlReviewStateRepository(SqliteConnectionFactory connectionFactory) : IReviewStateRepository
{
    public Task<IReadOnlyDictionary<int, WordReviewState>> GetStatesAsync(
        Language targetLanguage,
        IReadOnlyCollection<int> wordEntryIds,
        CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            var result = new Dictionary<int, WordReviewState>();
            if (wordEntryIds.Count == 0)
            {
                return (IReadOnlyDictionary<int, WordReviewState>)result;
            }

            var ids = wordEntryIds.ToList();
            var parameterNames = ids.Select((_, index) => $"$id{index}").ToList();

            using var command = connection.CreateCommand();
            command.CommandText = $"""
                SELECT Id, WordEntryId, EasinessFactor, IntervalDays, Repetitions, DueDateUtc, LastReviewedUtc
                FROM ReviewStates
                WHERE TargetLanguage = $targetLanguage AND WordEntryId IN ({string.Join(",", parameterNames)});
                """;
            command.Parameters.AddWithValue("$targetLanguage", (int)targetLanguage);
            for (var i = 0; i < ids.Count; i++)
            {
                command.Parameters.AddWithValue(parameterNames[i], ids[i]);
            }

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var wordEntryId = reader.GetInt32(1);
                result[wordEntryId] = new WordReviewState
                {
                    Id = reader.GetInt32(0),
                    WordEntryId = wordEntryId,
                    TargetLanguage = targetLanguage,
                    EasinessFactor = reader.GetDouble(2),
                    IntervalDays = reader.GetInt32(3),
                    Repetitions = reader.GetInt32(4),
                    DueDateUtc = reader.IsDBNull(5) ? null : ParseUtc(reader.GetString(5)),
                    LastReviewedUtc = reader.IsDBNull(6) ? null : ParseUtc(reader.GetString(6))
                };
            }

            return (IReadOnlyDictionary<int, WordReviewState>)result;
        }, cancellationToken);

    public Task UpsertAsync(WordReviewState state, CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            using var command = connection.CreateCommand();
            command.CommandText = """
                INSERT INTO ReviewStates (WordEntryId, TargetLanguage, EasinessFactor, IntervalDays, Repetitions, DueDateUtc, LastReviewedUtc)
                VALUES ($wordEntryId, $targetLanguage, $easinessFactor, $intervalDays, $repetitions, $dueDateUtc, $lastReviewedUtc)
                ON CONFLICT (WordEntryId, TargetLanguage) DO UPDATE SET
                    EasinessFactor = excluded.EasinessFactor,
                    IntervalDays = excluded.IntervalDays,
                    Repetitions = excluded.Repetitions,
                    DueDateUtc = excluded.DueDateUtc,
                    LastReviewedUtc = excluded.LastReviewedUtc;
                """;
            command.Parameters.AddWithValue("$wordEntryId", state.WordEntryId);
            command.Parameters.AddWithValue("$targetLanguage", (int)state.TargetLanguage);
            command.Parameters.AddWithValue("$easinessFactor", state.EasinessFactor);
            command.Parameters.AddWithValue("$intervalDays", state.IntervalDays);
            command.Parameters.AddWithValue("$repetitions", state.Repetitions);
            command.Parameters.AddWithValue("$dueDateUtc", (object?)state.DueDateUtc?.ToString("o", CultureInfo.InvariantCulture) ?? DBNull.Value);
            command.Parameters.AddWithValue("$lastReviewedUtc", (object?)state.LastReviewedUtc?.ToString("o", CultureInfo.InvariantCulture) ?? DBNull.Value);
            command.ExecuteNonQuery();
        }, cancellationToken);

    private static DateTime ParseUtc(string value) =>
        DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
}

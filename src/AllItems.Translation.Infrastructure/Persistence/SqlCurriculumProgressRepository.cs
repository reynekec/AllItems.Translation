using System.Globalization;
using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Curriculum;

namespace AllItems.Translation.Infrastructure.Persistence;

public sealed class SqlCurriculumProgressRepository(SqliteConnectionFactory connectionFactory, IClock clock) : ICurriculumProgressRepository
{
    public Task<IReadOnlySet<string>> GetCompletedExerciseIdsAsync(IReadOnlyCollection<string> exerciseIds, CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            var completed = new HashSet<string>();
            if (exerciseIds.Count == 0)
            {
                return (IReadOnlySet<string>)completed;
            }

            var ids = exerciseIds.ToList();
            var parameterNames = ids.Select((_, index) => $"$id{index}").ToList();

            using var command = connection.CreateCommand();
            command.CommandText = $"""
                SELECT ExerciseId FROM CurriculumExerciseProgress
                WHERE ExerciseId IN ({string.Join(",", parameterNames)});
                """;
            for (var i = 0; i < ids.Count; i++)
            {
                command.Parameters.AddWithValue(parameterNames[i], ids[i]);
            }

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                completed.Add(reader.GetString(0));
            }

            return (IReadOnlySet<string>)completed;
        }, cancellationToken);

    public Task<IReadOnlyDictionary<string, CurriculumExerciseReviewState>> GetReviewStatesAsync(
        IReadOnlyCollection<string> exerciseIds,
        CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            var result = new Dictionary<string, CurriculumExerciseReviewState>(StringComparer.Ordinal);
            if (exerciseIds.Count == 0)
            {
                return (IReadOnlyDictionary<string, CurriculumExerciseReviewState>)result;
            }

            var ids = exerciseIds.ToList();
            var parameterNames = ids.Select((_, index) => $"$id{index}").ToList();

            using var command = connection.CreateCommand();
            command.CommandText = $"""
                SELECT ExerciseId, EasinessFactor, IntervalDays, Repetitions, LapseCount, CorrectAttempts, IncorrectAttempts, DueDateUtc, LastReviewedUtc
                FROM CurriculumExerciseReviewStates
                WHERE ExerciseId IN ({string.Join(",", parameterNames)});
                """;

            for (var i = 0; i < ids.Count; i++)
            {
                command.Parameters.AddWithValue(parameterNames[i], ids[i]);
            }

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var exerciseId = reader.GetString(0);
                result[exerciseId] = new CurriculumExerciseReviewState
                {
                    ExerciseId = exerciseId,
                    EasinessFactor = reader.GetDouble(1),
                    IntervalDays = reader.GetInt32(2),
                    Repetitions = reader.GetInt32(3),
                    LapseCount = reader.GetInt32(4),
                    CorrectAttempts = reader.GetInt32(5),
                    IncorrectAttempts = reader.GetInt32(6),
                    DueDateUtc = reader.IsDBNull(7) ? null : ParseUtc(reader.GetString(7)),
                    LastReviewedUtc = reader.IsDBNull(8) ? null : ParseUtc(reader.GetString(8))
                };
            }

            return (IReadOnlyDictionary<string, CurriculumExerciseReviewState>)result;
        }, cancellationToken);

    public Task<IReadOnlySet<string>> GetAttemptedExerciseIdsAsync(CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            var attempted = new HashSet<string>(StringComparer.Ordinal);

            using var command = connection.CreateCommand();
            command.CommandText = """
                SELECT DISTINCT ExerciseId
                FROM CurriculumExerciseAttempts;
                """;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                attempted.Add(reader.GetString(0));
            }

            return (IReadOnlySet<string>)attempted;
        }, cancellationToken);

    public Task UpsertReviewStateAsync(CurriculumExerciseReviewState state, CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            using var command = connection.CreateCommand();
            command.CommandText = """
                INSERT INTO CurriculumExerciseReviewStates (ExerciseId, EasinessFactor, IntervalDays, Repetitions, LapseCount, CorrectAttempts, IncorrectAttempts, DueDateUtc, LastReviewedUtc)
                VALUES ($exerciseId, $easinessFactor, $intervalDays, $repetitions, $lapseCount, $correctAttempts, $incorrectAttempts, $dueDateUtc, $lastReviewedUtc)
                ON CONFLICT (ExerciseId) DO UPDATE SET
                    EasinessFactor = excluded.EasinessFactor,
                    IntervalDays = excluded.IntervalDays,
                    Repetitions = excluded.Repetitions,
                    LapseCount = excluded.LapseCount,
                    CorrectAttempts = excluded.CorrectAttempts,
                    IncorrectAttempts = excluded.IncorrectAttempts,
                    DueDateUtc = excluded.DueDateUtc,
                    LastReviewedUtc = excluded.LastReviewedUtc;
                """;
            command.Parameters.AddWithValue("$exerciseId", state.ExerciseId);
            command.Parameters.AddWithValue("$easinessFactor", state.EasinessFactor);
            command.Parameters.AddWithValue("$intervalDays", state.IntervalDays);
            command.Parameters.AddWithValue("$repetitions", state.Repetitions);
            command.Parameters.AddWithValue("$lapseCount", state.LapseCount);
            command.Parameters.AddWithValue("$correctAttempts", state.CorrectAttempts);
            command.Parameters.AddWithValue("$incorrectAttempts", state.IncorrectAttempts);
            command.Parameters.AddWithValue("$dueDateUtc", (object?)state.DueDateUtc?.ToString("o", CultureInfo.InvariantCulture) ?? DBNull.Value);
            command.Parameters.AddWithValue("$lastReviewedUtc", (object?)state.LastReviewedUtc?.ToString("o", CultureInfo.InvariantCulture) ?? DBNull.Value);
            command.ExecuteNonQuery();
        }, cancellationToken);

    public Task MarkExerciseCompletedAsync(string exerciseId, CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            using var command = connection.CreateCommand();
            command.CommandText = """
                INSERT INTO CurriculumExerciseProgress (ExerciseId, CompletedAtUtc)
                VALUES ($exerciseId, $completedAtUtc)
                ON CONFLICT (ExerciseId) DO UPDATE SET CompletedAtUtc = excluded.CompletedAtUtc;
                """;
            command.Parameters.AddWithValue("$exerciseId", exerciseId);
            command.Parameters.AddWithValue("$completedAtUtc", clock.UtcNow.ToString("o", CultureInfo.InvariantCulture));
            command.ExecuteNonQuery();
        }, cancellationToken);

    public Task RecordAttemptAsync(string exerciseId, bool isCorrect, CancellationToken cancellationToken = default) =>
        connectionFactory.RunAsync(connection =>
        {
            using var command = connection.CreateCommand();
            command.CommandText = """
                INSERT INTO CurriculumExerciseAttempts (ExerciseId, IsCorrect, AttemptedAtUtc)
                VALUES ($exerciseId, $isCorrect, $attemptedAtUtc);
                """;
            command.Parameters.AddWithValue("$exerciseId", exerciseId);
            command.Parameters.AddWithValue("$isCorrect", isCorrect ? 1 : 0);
            command.Parameters.AddWithValue("$attemptedAtUtc", clock.UtcNow.ToString("o", CultureInfo.InvariantCulture));
            command.ExecuteNonQuery();
        }, cancellationToken);

    private static DateTime ParseUtc(string value) => DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
}

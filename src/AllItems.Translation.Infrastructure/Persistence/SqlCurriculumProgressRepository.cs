using System.Globalization;
using AllItems.Translation.Core.Abstractions;

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
}

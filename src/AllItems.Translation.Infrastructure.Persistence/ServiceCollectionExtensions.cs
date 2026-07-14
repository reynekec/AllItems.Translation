using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Study;
using Microsoft.Extensions.DependencyInjection;

namespace AllItems.Translation.Infrastructure.Persistence;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers just the SQLite-backed flashcard study stack against the database at <paramref name="databasePath"/>:
    /// the connection factory, schema initializer, clock, word/review repositories, the SM-2 scheduler, and the study
    /// session service. Deliberately Google/translation-free so it can be reused by platforms (iOS/MAUI) that must not
    /// drag the gRPC translation dependency into an AOT-trimmed build. The full desktop registration composes this.
    /// </summary>
    public static IServiceCollection AddAllItemsTranslationStudy(this IServiceCollection services, string databasePath)
    {
        services.AddSingleton(new SqliteConnectionFactory($"Data Source={databasePath}"));
        services.AddSingleton<DatabaseInitializer>();

        services.AddSingleton<IClock, SystemClock>();
        services.AddSingleton<IWordRepository, WordRepository>();
        services.AddSingleton<IReviewStateRepository, SqlReviewStateRepository>();
        services.AddSingleton<ISpacedRepetitionScheduler, Sm2Scheduler>();
        services.AddSingleton<IStudySessionService, StudySessionService>();

        return services;
    }
}

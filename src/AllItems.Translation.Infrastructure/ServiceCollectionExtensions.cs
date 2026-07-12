using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Curriculum;
using AllItems.Translation.Core.Study;
using AllItems.Translation.Core.Tokenization;
using AllItems.Translation.Core.Translation;
using AllItems.Translation.Infrastructure.Credentials;
using AllItems.Translation.Infrastructure.GoogleTranslation;
using AllItems.Translation.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace AllItems.Translation.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAllItemsTranslationInfrastructure(this IServiceCollection services)
    {
        AppPaths.EnsureDirectoriesExist();

        services.AddSingleton(new SqliteConnectionFactory($"Data Source={AppPaths.DatabaseFilePath}"));
        services.AddSingleton<DatabaseInitializer>();

        services.AddSingleton<IClock, SystemClock>();
        services.AddSingleton<ICredentialStore, FileCredentialStore>();
        services.AddSingleton<ISentenceTokenizer, SentenceTokenizer>();
        services.AddSingleton<IWordAligner, PositionalWordAligner>();
        services.AddSingleton<ITranslationProvider, GoogleTranslationProvider>();

        services.AddSingleton<IWordRepository, WordRepository>();
        services.AddSingleton<ISentenceTranslationRepository, SentenceTranslationRepository>();
        services.AddSingleton<IApiUsageTracker, ApiUsageTracker>();
        services.AddSingleton<ISentenceTranslationService, SentenceTranslationService>();

        services.AddSingleton<IReviewStateRepository, SqlReviewStateRepository>();
        services.AddSingleton<ISpacedRepetitionScheduler, Sm2Scheduler>();
        services.AddSingleton<IStudySessionService, StudySessionService>();

        services.AddSingleton<ICurriculumCatalog, StaticCurriculumCatalog>();
        services.AddSingleton<IExerciseGrader, ExerciseGrader>();
        services.AddSingleton<ICurriculumProgressRepository, SqlCurriculumProgressRepository>();
        services.AddSingleton<ICurriculumService, CurriculumService>();

        return services;
    }
}

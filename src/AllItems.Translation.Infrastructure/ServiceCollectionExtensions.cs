using AllItems.Translation.Core.Abstractions;
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

        return services;
    }
}

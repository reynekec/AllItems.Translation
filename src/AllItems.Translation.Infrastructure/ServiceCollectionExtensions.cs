using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Tokenization;
using AllItems.Translation.Core.Translation;
using AllItems.Translation.Infrastructure.Credentials;
using AllItems.Translation.Infrastructure.GoogleTranslation;
using AllItems.Translation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AllItems.Translation.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAllItemsTranslationInfrastructure(this IServiceCollection services)
    {
        AppPaths.EnsureDirectoriesExist();

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={AppPaths.DatabaseFilePath}"));

        services.AddSingleton<IClock, SystemClock>();
        services.AddSingleton<ICredentialStore, FileCredentialStore>();
        services.AddSingleton<ISentenceTokenizer, SentenceTokenizer>();
        services.AddSingleton<IWordAligner, PositionalWordAligner>();
        services.AddSingleton<ITranslationProvider, GoogleTranslationProvider>();

        services.AddScoped<IWordRepository, WordRepository>();
        services.AddScoped<ISentenceTranslationRepository, SentenceTranslationRepository>();
        services.AddScoped<IApiUsageTracker, ApiUsageTracker>();
        services.AddScoped<ISentenceTranslationService, SentenceTranslationService>();

        return services;
    }
}

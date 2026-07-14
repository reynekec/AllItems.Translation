using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Curriculum;
using AllItems.Translation.Core.Study;
using AllItems.Translation.Core.Tokenization;
using AllItems.Translation.Core.Translation;
using AllItems.Translation.Infrastructure.Credentials;
using AllItems.Translation.Infrastructure.GoogleTranslation;
using AllItems.Translation.Infrastructure.Persistence;
using AllItems.Translation.Infrastructure.Settings;
using AllItems.Translation.Infrastructure.Sync;
using AllItems.Translation.Infrastructure.Vocabulary;
using Microsoft.Extensions.DependencyInjection;

namespace AllItems.Translation.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAllItemsTranslationInfrastructure(this IServiceCollection services)
    {
        AppPaths.EnsureDirectoriesExist();

        // Shared SQLite study stack (connection factory, initializer, clock, word/review repos, scheduler,
        // study session service) - the same registrations the iOS/MAUI app uses, pointed at the desktop DB.
        services.AddAllItemsTranslationStudy(AppPaths.DatabaseFilePath);

        services.AddSingleton<ICredentialStore, FileCredentialStore>();
        services.AddSingleton<IGitHubTokenStore, FileGitHubTokenStore>();
        services.AddSingleton<IStudyPreferenceStore, FileStudyPreferenceStore>();
        services.AddSingleton<ISentenceTokenizer, SentenceTokenizer>();
        services.AddSingleton<IWordAligner, PositionalWordAligner>();
        services.AddSingleton<ITranslationProvider, GoogleTranslationProvider>();

        services.AddSingleton<ISentenceTranslationRepository, SentenceTranslationRepository>();
        services.AddSingleton<IApiUsageTracker, ApiUsageTracker>();
        services.AddSingleton<ISentenceTranslationService, SentenceTranslationService>();

        services.AddSingleton<IFlashcardExportService, GitHubFlashcardExportService>();

        services.AddSingleton<ICurriculumCatalog, StaticCurriculumCatalog>();
        services.AddSingleton<IExerciseGrader, ExerciseGrader>();
        services.AddSingleton<ICurriculumProgressRepository, SqlCurriculumProgressRepository>();
        services.AddSingleton<ICurriculumService, CurriculumService>();

        services.AddSingleton<IVocabularySeedRepository, EmbeddedVocabularySeedRepository>();
        services.AddSingleton<IVocabularyImportRepository, SqlVocabularyImportRepository>();
        services.AddSingleton<IVocabularyImportService, VocabularyImportService>();

        return services;
    }
}

using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Infrastructure.Persistence;
using AllItems.Translation.Maui.Services;
using AllItems.Translation.Maui.ViewModels;
using AllItems.Translation.Maui.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AllItems.Translation.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // The flashcard database lives in the app's private sandbox. It starts empty and is filled by
        // importing the desktop export; running flashcards is fully offline once imported.
        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "allitems.db");

        builder.Services.AddAllItemsTranslationStudy(databasePath);
        builder.Services.AddSingleton<IStudyPreferenceStore, MauiStudyPreferenceStore>();
        builder.Services.AddSingleton(new HttpClient());
        builder.Services.AddSingleton<IFlashcardImportService>(sp =>
            new GitHubFlashcardImportService(sp.GetRequiredService<HttpClient>(), databasePath));

        builder.Services.AddSingleton<StudyPageViewModel>();
        builder.Services.AddSingleton<ImportPageViewModel>();
        builder.Services.AddSingleton<StudyPage>();
        builder.Services.AddSingleton<ImportPage>();

        var app = builder.Build();

        // Idempotent CREATE TABLE IF NOT EXISTS - safe on an empty first-run DB and on a freshly imported one.
        app.Services.GetRequiredService<DatabaseInitializer>().InitializeAsync().GetAwaiter().GetResult();

        return app;
    }
}

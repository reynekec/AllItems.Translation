using System.Windows;
using AllItems.Translation.App.ViewModels;
using AllItems.Translation.App.ViewModels.Training;
using AllItems.Translation.App.Views;
using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Infrastructure;
using AllItems.Translation.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wpf.Ui.Appearance;

namespace AllItems.Translation.App;

public partial class App : Application
{
    private IHost? _host;

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddAllItemsTranslationInfrastructure();
                services.AddTransient<MainViewModel>();
                services.AddTransient<MainWindow>();
                services.AddTransient<DictionaryManagerViewModel>();
                services.AddTransient<DictionaryManagerWindow>();
                services.AddTransient<CredentialSetupViewModel>();
                services.AddTransient<CredentialSetupWindow>();
                services.AddTransient<StudySessionViewModel>();
                services.AddTransient<StudySessionWindow>();
                services.AddTransient<TrainingViewModel>();
                services.AddTransient<TrainingWindow>();
                services.AddTransient<StartWindow>();
            })
            .Build();

        await _host.StartAsync();

        ApplicationThemeManager.Apply(ApplicationTheme.Dark);

        var databaseInitializer = _host.Services.GetRequiredService<DatabaseInitializer>();
        await databaseInitializer.InitializeAsync();

        var credentialStore = _host.Services.GetRequiredService<ICredentialStore>();
        if (!credentialStore.HasCredential)
        {
            var setupWindow = _host.Services.GetRequiredService<CredentialSetupWindow>();
            var result = setupWindow.ShowDialog();
            if (result != true)
            {
                Shutdown();
                return;
            }
        }

        var startWindow = _host.Services.GetRequiredService<StartWindow>();
        MainWindow = startWindow;
        startWindow.Show();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        if (_host is not null)
        {
            await _host.StopAsync();
            _host.Dispose();
        }

        base.OnExit(e);
    }
}

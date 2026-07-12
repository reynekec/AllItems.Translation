using System.Windows;
using System.Windows.Threading;
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

        DispatcherUnhandledException += OnDispatcherUnhandledException;
        AppDomain.CurrentDomain.UnhandledException += OnAppDomainUnhandledException;
        TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;

        try
        {
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
        catch (Exception ex)
        {
            MessageBox.Show(
                $"AllItems.Translation failed to start:\n\n{ex.Message}",
                "Startup error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            Shutdown(-1);
        }
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        try
        {
            if (_host is not null)
            {
                await _host.StopAsync();
                _host.Dispose();
            }
        }
        catch
        {
            // Best-effort shutdown; the process is exiting regardless.
        }

        base.OnExit(e);
    }

    private static void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        MessageBox.Show(
            $"An unexpected error occurred:\n\n{e.Exception.Message}\n\nThe application will try to continue.",
            "Unexpected error",
            MessageBoxButton.OK,
            MessageBoxImage.Error);
        e.Handled = true;
    }

    private static void OnAppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        var message = e.ExceptionObject is Exception ex ? ex.Message : e.ExceptionObject.ToString();
        MessageBox.Show(
            $"A fatal error occurred and the application must close:\n\n{message}",
            "Fatal error",
            MessageBoxButton.OK,
            MessageBoxImage.Error);
    }

    private static void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        e.SetObserved();
    }
}

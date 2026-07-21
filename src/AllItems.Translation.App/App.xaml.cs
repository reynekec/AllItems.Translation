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
using Drawing = System.Drawing;
using WinForms = System.Windows.Forms;
using Wpf.Ui.Appearance;

namespace AllItems.Translation.App;

public partial class App : System.Windows.Application
{
    private const string UiAutomationModeEnvironmentVariable = "ALLITEMS_TRANSLATION_UI_AUTOMATION";
    private IHost? _host;
    private WinForms.NotifyIcon? _notifyIcon;
    private bool _isExiting;
    private IStartupPreferenceStore? _startupPreferenceStore;

    internal static bool IsUiAutomationModeEnabled =>
        string.Equals(
            Environment.GetEnvironmentVariable(UiAutomationModeEnvironmentVariable),
            "1",
            StringComparison.Ordinal);

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
            ConfigureMainWindowBehavior(startWindow);
            _startupPreferenceStore = _host.Services.GetRequiredService<IStartupPreferenceStore>();
            InitializeNotifyIcon();
            startWindow.Show();
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show(
                $"AllItems.Translation failed to start:\n\n{ex.Message}",
                "Startup error",
                System.Windows.MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Error);
            Shutdown(-1);
        }
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        try
        {
            DisposeNotifyIcon();

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

    private void ConfigureMainWindowBehavior(Window mainWindow)
    {
        mainWindow.Closing += (_, e) =>
        {
            if (_isExiting || IsUiAutomationModeEnabled)
            {
                return;
            }

            e.Cancel = true;
            HideMainWindowToTray();
        };
    }

    private void InitializeNotifyIcon()
    {
        if (IsUiAutomationModeEnabled)
        {
            return;
        }

        var trayMenu = new WinForms.ContextMenuStrip();
        
        var runAtStartupItem = new WinForms.ToolStripMenuItem("Start on Windows startup", null, OnRunAtStartupToggled)
        {
            Checked = _startupPreferenceStore?.IsRunAtStartupEnabled ?? false,
            CheckOnClick = true
        };
        trayMenu.Items.Add(runAtStartupItem);
        trayMenu.Items.Add(new WinForms.ToolStripSeparator());
        trayMenu.Items.Add("Exit", null, (_, _) => ExitApplicationFromTray());

        _notifyIcon = new WinForms.NotifyIcon
        {
            Text = "AllItems.Translation",
            Icon = ResolveTrayIcon(),
            ContextMenuStrip = trayMenu,
            Visible = true
        };

        _notifyIcon.DoubleClick += (_, _) => ShowAndFocusMainWindow();
    }

    private void OnRunAtStartupToggled(object? sender, EventArgs e)
    {
        if (sender is not WinForms.ToolStripMenuItem item || _startupPreferenceStore is null)
        {
            return;
        }

        _ = _startupPreferenceStore.SetRunAtStartupAsync(item.Checked);
    }

    private void HideMainWindowToTray()
    {
        if (MainWindow is null)
        {
            return;
        }

        MainWindow.WindowState = WindowState.Minimized;
        MainWindow.ShowInTaskbar = false;
        MainWindow.Hide();
    }

    private void ShowAndFocusMainWindow()
    {
        Dispatcher.Invoke(() =>
        {
            if (MainWindow is null)
            {
                return;
            }

            MainWindow.ShowInTaskbar = true;
            MainWindow.Show();
            MainWindow.WindowState = WindowState.Normal;
            MainWindow.Activate();
            MainWindow.Topmost = true;
            MainWindow.Topmost = false;
            MainWindow.Focus();
        });
    }

    private void ExitApplicationFromTray()
    {
        Dispatcher.Invoke(() =>
        {
            _isExiting = true;
            DisposeNotifyIcon();
            Shutdown();
        });
    }

    private static Drawing.Icon ResolveTrayIcon()
    {
        var processIcon = Environment.ProcessPath is not null
            ? Drawing.Icon.ExtractAssociatedIcon(Environment.ProcessPath)
            : null;

        return processIcon ?? Drawing.SystemIcons.Application;
    }

    private void DisposeNotifyIcon()
    {
        if (_notifyIcon is null)
        {
            return;
        }

        _notifyIcon.Visible = false;
        _notifyIcon.Dispose();
        _notifyIcon = null;
    }

    private static void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        System.Windows.MessageBox.Show(
            $"An unexpected error occurred:\n\n{e.Exception.Message}\n\nThe application will try to continue.",
            "Unexpected error",
            System.Windows.MessageBoxButton.OK,
            System.Windows.MessageBoxImage.Error);
        e.Handled = true;
    }

    private static void OnAppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        var message = e.ExceptionObject is Exception ex ? ex.Message : e.ExceptionObject.ToString();
        System.Windows.MessageBox.Show(
            $"A fatal error occurred and the application must close:\n\n{message}",
            "Fatal error",
            System.Windows.MessageBoxButton.OK,
            System.Windows.MessageBoxImage.Error);
    }

    private static void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        e.SetObserved();
    }
}

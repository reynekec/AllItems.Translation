using System.Windows;
using System.Windows.Controls.Primitives;
using AllItems.Translation.App.Theming;
using AllItems.Translation.Core.Study;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;

namespace AllItems.Translation.App.Views;

public partial class StartWindow : FluentWindow
{
    private readonly IServiceProvider _services;
    private readonly IAppThemeController _appThemeController;

    public StartWindow(IServiceProvider services, IAppThemeController appThemeController)
    {
        InitializeComponent();
        _services = services;
        _appThemeController = appThemeController;

        ThemeToggle.IsChecked = _appThemeController.CurrentMode == ThemePreferenceMode.Dark;
    }

    private void OnTranslationsClick(object sender, RoutedEventArgs e) =>
        OpenSection(_services.GetRequiredService<MainWindow>());

    private void OnFlashcardsClick(object sender, RoutedEventArgs e) =>
        OpenSection(_services.GetRequiredService<StudySessionWindow>());

    private void OnTrainingClick(object sender, RoutedEventArgs e) =>
        OpenSection(_services.GetRequiredService<TrainingWindow>());

    private void OnSettingsClick(object sender, RoutedEventArgs e)
    {
        var window = _services.GetRequiredService<CredentialSetupWindow>();
        window.ShowDialog();
    }

    private void OnThemeToggleChanged(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleButton toggle)
        {
            return;
        }

        _appThemeController.SetMode(toggle.IsChecked == true ? ThemePreferenceMode.Dark : ThemePreferenceMode.Light);
    }

    private void OpenSection(Window window)
    {
        window.Closed += (_, _) => Show();
        Hide();
        window.Show();
    }
}

using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;

namespace AllItems.Translation.App.Views;

public partial class StartWindow : FluentWindow
{
    private readonly IServiceProvider _services;

    public StartWindow(IServiceProvider services)
    {
        InitializeComponent();
        _services = services;
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

    private void OpenSection(Window window)
    {
        window.Closed += (_, _) => Show();
        Hide();
        window.Show();
    }
}

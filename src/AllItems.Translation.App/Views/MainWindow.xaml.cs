using System.Windows;
using AllItems.Translation.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;

namespace AllItems.Translation.App.Views;

public partial class MainWindow : FluentWindow
{
    private readonly IServiceProvider _services;

    public MainWindow(MainViewModel viewModel, IServiceProvider services)
    {
        InitializeComponent();
        DataContext = viewModel;
        _services = services;
    }

    private void OnOpenStudyClick(object sender, RoutedEventArgs e)
    {
        var window = _services.GetRequiredService<StudySessionWindow>();
        window.Owner = this;
        window.Show();
    }

    private void OnOpenDictionaryClick(object sender, RoutedEventArgs e)
    {
        var window = _services.GetRequiredService<DictionaryManagerWindow>();
        window.Owner = this;
        window.Show();
    }

    private void OnOpenSettingsClick(object sender, RoutedEventArgs e)
    {
        var window = _services.GetRequiredService<CredentialSetupWindow>();
        window.ShowDialog();
    }
}

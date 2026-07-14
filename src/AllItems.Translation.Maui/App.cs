using AllItems.Translation.Maui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace AllItems.Translation.Maui;

/// <summary>
/// Code-only application root (no App.xaml needed). Presents two tabs - Study and Import - each in its own
/// navigation frame so iOS shows a title bar. Pages are resolved from DI so they get their view models.
/// </summary>
public sealed class App : Application
{
    private readonly IServiceProvider _services;

    public App(IServiceProvider services)
    {
        _services = services;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var study = new NavigationPage(_services.GetRequiredService<StudyPage>()) { Title = "Study" };
        var import = new NavigationPage(_services.GetRequiredService<ImportPage>()) { Title = "Import" };

        var tabs = new TabbedPage();
        tabs.Children.Add(study);
        tabs.Children.Add(import);

        return new Window(tabs) { Title = "AllItems Flashcards" };
    }
}

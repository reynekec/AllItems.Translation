using System.Windows.Threading;
using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Domain;
using AllItems.Translation.Core.Translation;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AllItems.Translation.App.ViewModels;

public sealed partial class MainViewModel : ObservableObject
{
    private readonly ISentenceTranslationService _translationService;
    private readonly IApiUsageTracker _usageTracker;
    private readonly DispatcherTimer _debounceTimer;
    private CancellationTokenSource? _translationCts;

    public IReadOnlyList<Language> AvailableSourceLanguages { get; } = Enum.GetValues<Language>();

    [ObservableProperty]
    private Language sourceLanguage = Language.German;

    [ObservableProperty]
    private string sourceText = string.Empty;

    [ObservableProperty]
    private string usageCounterText = "0 characters used this month";

    [ObservableProperty]
    private string? errorMessage;

    public TranslationPanelViewModel PanelA { get; }
    public TranslationPanelViewModel PanelB { get; }

    public MainViewModel(ISentenceTranslationService translationService, IApiUsageTracker usageTracker)
    {
        _translationService = translationService;
        _usageTracker = usageTracker;

        var targets = TargetLanguagesFor(SourceLanguage);
        PanelA = new TranslationPanelViewModel(translationService, targets[0]);
        PanelB = new TranslationPanelViewModel(translationService, targets[1]);

        _debounceTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) };
        _debounceTimer.Tick += async (_, _) =>
        {
            _debounceTimer.Stop();
            await TranslateAsync();
        };

        _ = RefreshUsageAsync();
    }

    partial void OnSourceTextChanged(string value)
    {
        _debounceTimer.Stop();
        _debounceTimer.Start();
    }

    partial void OnSourceLanguageChanged(Language value)
    {
        var targets = TargetLanguagesFor(value);
        PanelA.TargetLanguage = targets[0];
        PanelB.TargetLanguage = targets[1];
        _debounceTimer.Stop();
        _debounceTimer.Start();
    }

    private static Language[] TargetLanguagesFor(Language source) =>
        Enum.GetValues<Language>().Where(l => l != source).ToArray();

    private async Task TranslateAsync()
    {
        _translationCts?.Cancel();
        var cts = new CancellationTokenSource();
        _translationCts = cts;

        ErrorMessage = null;

        if (string.IsNullOrWhiteSpace(SourceText))
        {
            return;
        }

        try
        {
            await Task.WhenAll(
                PanelA.TranslateAsync(SourceText, SourceLanguage, cts.Token),
                PanelB.TranslateAsync(SourceText, SourceLanguage, cts.Token));

            await RefreshUsageAsync();
        }
        catch (OperationCanceledException)
        {
            // Superseded by a newer keystroke; ignore.
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("credential", StringComparison.OrdinalIgnoreCase))
        {
            ErrorMessage = "Google Cloud credential isn't set up yet. Open Settings to add your service-account key.";
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Translation failed: {ex.Message}";
        }
    }

    private async Task RefreshUsageAsync()
    {
        var used = await _usageTracker.GetCurrentMonthUsageAsync();
        UsageCounterText = $"{used:N0} characters used this month";
    }
}

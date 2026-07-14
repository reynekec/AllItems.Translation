using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Domain;
using AllItems.Translation.Core.Study;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AllItems.Translation.Maui.ViewModels;

/// <summary>
/// Drives the offline flashcard review loop on the phone, reusing the exact same <see cref="IStudySessionService"/>
/// (SM-2 scheduler + SQLite repositories) as the desktop. Grades are recorded to the local database; because
/// sync is one-way they are overwritten the next time the desktop export is imported.
/// </summary>
public sealed partial class StudyPageViewModel : ObservableObject
{
    private const int SessionSize = 20;

    private readonly IStudySessionService _studySessionService;
    private readonly IStudyPreferenceStore _preferenceStore;

    private IReadOnlyList<StudyCard> _cards = [];
    private int _currentIndex;

    public StudyPageViewModel(IStudySessionService studySessionService, IStudyPreferenceStore preferenceStore)
    {
        _studySessionService = studySessionService;
        _preferenceStore = preferenceStore;

        var preferences = preferenceStore.Load();
        sourceLanguage = preferences.SourceLanguage;
        targetLanguage = preferences.TargetLanguage;
    }

    public IReadOnlyList<Language> AvailableLanguages { get; } = Enum.GetValues<Language>();

    [ObservableProperty]
    private Language sourceLanguage;

    [ObservableProperty]
    private Language targetLanguage;

    partial void OnSourceLanguageChanged(Language value)
    {
        SavePreferences();
        _ = RefreshAvailabilityAsync();
    }

    partial void OnTargetLanguageChanged(Language value)
    {
        SavePreferences();
        _ = RefreshAvailabilityAsync();
    }

    private void SavePreferences() =>
        _preferenceStore.Save(new StudyPreferences(SourceLanguage, TargetLanguage));

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsSetupVisible))]
    private bool isSessionActive;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsSetupVisible))]
    private bool isSessionComplete;

    public bool IsSetupVisible => !IsSessionActive && !IsSessionComplete;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsAnswerHidden))]
    private bool isAnswerShown;

    public bool IsAnswerHidden => !IsAnswerShown;

    [ObservableProperty]
    private string frontText = string.Empty;

    [ObservableProperty]
    private string backText = string.Empty;

    [ObservableProperty]
    private string? exampleSentence;

    [ObservableProperty]
    private string? backExampleSentence;

    [ObservableProperty]
    private string progressText = string.Empty;

    [ObservableProperty]
    private string? statusMessage;

    [ObservableProperty]
    private int reviewedCount;

    [ObservableProperty]
    private string availableWordsText = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(StartSessionCommand))]
    [NotifyCanExecuteChangedFor(nameof(GradeCommand))]
    [NotifyCanExecuteChangedFor(nameof(RestartCommand))]
    private bool isBusy;

    public async Task InitializeAsync() => await RefreshAvailabilityAsync();

    [RelayCommand(CanExecute = nameof(CanStart))]
    private async Task StartSessionAsync()
    {
        if (SourceLanguage == TargetLanguage)
        {
            StatusMessage = "Source and target language must be different.";
            return;
        }

        IsBusy = true;
        StatusMessage = null;
        try
        {
            IsSessionComplete = false;
            ReviewedCount = 0;
            _currentIndex = 0;
            _cards = await _studySessionService.BuildSessionAsync(SourceLanguage, TargetLanguage, SessionSize);

            if (_cards.Count == 0)
            {
                IsSessionActive = false;
                StatusMessage = "No due or new cards for this language pair. Import the latest export, or pick another pair.";
                await RefreshAvailabilityAsync();
                return;
            }

            IsSessionActive = true;
            ShowCurrentCard();
        }
        catch (Exception ex)
        {
            IsSessionActive = false;
            StatusMessage = $"Couldn't start the session: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private bool CanStart() => !IsBusy;

    [RelayCommand]
    private void ShowAnswer() => IsAnswerShown = true;

    [RelayCommand(CanExecute = nameof(CanGrade))]
    private async Task GradeAsync(ReviewGrade grade)
    {
        IsBusy = true;
        try
        {
            var currentCard = _cards[_currentIndex];
            await _studySessionService.RecordAnswerAsync(currentCard, grade);

            ReviewedCount++;
            _currentIndex++;

            if (_currentIndex >= _cards.Count)
            {
                IsSessionActive = false;
                IsSessionComplete = true;
                await RefreshAvailabilityAsync();
                return;
            }

            ShowCurrentCard();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Couldn't record that answer: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private bool CanGrade(ReviewGrade grade) => !IsBusy;

    [RelayCommand(CanExecute = nameof(CanStart))]
    private Task RestartAsync() => StartSessionAsync();

    private void ShowCurrentCard()
    {
        var card = _cards[_currentIndex];
        FrontText = card.Article is null ? card.FrontText : $"{card.Article} {card.FrontText}";
        BackText = card.BackText;
        ExampleSentence = card.ExampleSentence;
        BackExampleSentence = card.BackExampleSentence;

        IsAnswerShown = false;
        ProgressText = $"{_currentIndex + 1} / {_cards.Count}";
    }

    private async Task RefreshAvailabilityAsync()
    {
        if (SourceLanguage == TargetLanguage)
        {
            AvailableWordsText = "Pick two different languages.";
            return;
        }

        try
        {
            var availableWordCount = await _studySessionService.GetAvailableWordCountAsync(SourceLanguage, TargetLanguage);
            AvailableWordsText = $"{availableWordCount} card(s) available";
        }
        catch (Exception ex)
        {
            AvailableWordsText = $"Couldn't load cards: {ex.Message}";
        }
    }
}

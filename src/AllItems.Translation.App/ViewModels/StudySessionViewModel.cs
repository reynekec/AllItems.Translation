using AllItems.Translation.Core.Domain;
using AllItems.Translation.Core.Study;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AllItems.Translation.App.ViewModels;

public sealed partial class StudySessionViewModel(IStudySessionService studySessionService) : ObservableObject
{
    private const int SessionSize = 20;

    private IReadOnlyList<StudyCard> _cards = [];
    private int _currentIndex;

    public IReadOnlyList<Language> AvailableLanguages { get; } = Enum.GetValues<Language>();

    [ObservableProperty]
    private Language sourceLanguage = Language.German;

    [ObservableProperty]
    private Language targetLanguage = Language.Afrikaans;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsSetupVisible))]
    private bool isSessionActive;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsSetupVisible))]
    private bool isSessionComplete;

    public bool IsSetupVisible => !IsSessionActive && !IsSessionComplete;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GradeCommand))]
    private bool isAnswerShown;

    [ObservableProperty]
    private string frontText = string.Empty;

    [ObservableProperty]
    private string backText = string.Empty;

    [ObservableProperty]
    private string progressText = string.Empty;

    [ObservableProperty]
    private string? statusMessage;

    [ObservableProperty]
    private int reviewedCount;

    [RelayCommand]
    private async Task StartSessionAsync()
    {
        if (SourceLanguage == TargetLanguage)
        {
            StatusMessage = "Source and target language must be different.";
            return;
        }

        StatusMessage = null;
        IsSessionComplete = false;
        ReviewedCount = 0;
        _currentIndex = 0;
        _cards = await studySessionService.BuildSessionAsync(SourceLanguage, TargetLanguage, SessionSize);

        if (_cards.Count == 0)
        {
            IsSessionActive = false;
            StatusMessage = "No due or new words for this language pair yet - keep translating sentences to build up your dictionary.";
            return;
        }

        IsSessionActive = true;
        ShowCurrentCard();
    }

    [RelayCommand]
    private void ShowAnswer() => IsAnswerShown = true;

    [RelayCommand(CanExecute = nameof(CanGrade))]
    private async Task GradeAsync(ReviewGrade grade)
    {
        await studySessionService.RecordAnswerAsync(_cards[_currentIndex], grade);
        ReviewedCount++;
        _currentIndex++;

        if (_currentIndex >= _cards.Count)
        {
            IsSessionActive = false;
            IsSessionComplete = true;
            return;
        }

        ShowCurrentCard();
    }

    private bool CanGrade(ReviewGrade grade) => IsAnswerShown;

    private void ShowCurrentCard()
    {
        var card = _cards[_currentIndex];
        FrontText = card.FrontText;
        BackText = card.BackText;
        IsAnswerShown = false;
        ProgressText = $"{_currentIndex + 1} / {_cards.Count}";
    }
}

using System.Collections.ObjectModel;
using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Domain;
using AllItems.Translation.Core.Study;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AllItems.Translation.App.ViewModels;

public sealed partial class StudySessionViewModel : ObservableObject
{
    private const int SessionSize = 20;

    private readonly IStudySessionService _studySessionService;
    private readonly IStudyPreferenceStore _preferenceStore;

    private IReadOnlyList<StudyCard> _cards = [];
    private int _currentIndex;

    public StudySessionViewModel(IStudySessionService studySessionService, IStudyPreferenceStore preferenceStore)
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

    partial void OnSourceLanguageChanged(Language value) => SavePreferences();

    partial void OnTargetLanguageChanged(Language value) => SavePreferences();

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
    [NotifyCanExecuteChangedFor(nameof(GradeCommand))]
    private bool isAnswerShown;

    [ObservableProperty]
    private string frontText = string.Empty;

    [ObservableProperty]
    private string backText = string.Empty;

    [ObservableProperty]
    private string? exampleSentence;

    public ObservableCollection<SentenceWordViewModel> SentenceWords { get; } = [];

    [ObservableProperty]
    private string? backExampleSentence;

    public ObservableCollection<SentenceWordViewModel> BackSentenceWords { get; } = [];

    [ObservableProperty]
    private string progressText = string.Empty;

    [ObservableProperty]
    private string? statusMessage;

    [ObservableProperty]
    private int reviewedCount;

    [ObservableProperty]
    private bool isLeechMode;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GradeCommand))]
    [NotifyCanExecuteChangedFor(nameof(StartSessionCommand))]
    [NotifyCanExecuteChangedFor(nameof(StartLeechSessionCommand))]
    [NotifyCanExecuteChangedFor(nameof(RestartCommand))]
    private bool isBusy;

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
            IsLeechMode = false;
            ReviewedCount = 0;
            _currentIndex = 0;
            _cards = await _studySessionService.BuildSessionAsync(SourceLanguage, TargetLanguage, SessionSize);

            if (_cards.Count == 0)
            {
                IsSessionActive = false;
                StatusMessage = "No due or new words for this language pair yet - keep translating sentences to build up your dictionary.";
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

    [RelayCommand(CanExecute = nameof(CanStart))]
    private async Task StartLeechSessionAsync()
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
            IsLeechMode = true;
            ReviewedCount = 0;
            _currentIndex = 0;
            _cards = await _studySessionService.BuildLeechSessionAsync(SourceLanguage, TargetLanguage, SessionSize);

            if (_cards.Count == 0)
            {
                IsSessionActive = false;
                StatusMessage = "No trouble words yet - keep studying and any tricky words will show up here.";
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

    [RelayCommand(CanExecute = nameof(CanStart))]
    private Task RestartAsync() => IsLeechMode ? StartLeechSessionAsync() : StartSessionAsync();

    [RelayCommand]
    private void ShowAnswer() => IsAnswerShown = true;

    [RelayCommand(CanExecute = nameof(CanGrade))]
    private async Task GradeAsync(ReviewGrade grade)
    {
        IsBusy = true;
        try
        {
            await _studySessionService.RecordAnswerAsync(_cards[_currentIndex], grade);
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
        catch (Exception ex)
        {
            StatusMessage = $"Couldn't record that answer: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private bool CanGrade(ReviewGrade grade) => IsAnswerShown && !IsBusy;

    private void ShowCurrentCard()
    {
        var card = _cards[_currentIndex];
        FrontText = card.Article is null ? card.FrontText : $"{card.Article} {card.FrontText}";
        BackText = card.BackText;

        // The front example sentence is always in the prompt's language, so it never leaks the answer
        // and can be shown immediately.
        ExampleSentence = card.ExampleSentence;
        SentenceWords.Clear();
        foreach (var word in BuildSentenceWords(card.ExampleSentence, card.Highlights))
        {
            SentenceWords.Add(word);
        }

        BackExampleSentence = card.BackExampleSentence;
        BackSentenceWords.Clear();
        foreach (var word in BuildSentenceWords(card.BackExampleSentence, card.BackHighlights))
        {
            BackSentenceWords.Add(word);
        }

        IsAnswerShown = false;
        ProgressText = $"{_currentIndex + 1} / {_cards.Count}";
    }

    /// <summary>
    /// Some generated highlights describe a multi-word phrase (e.g. "hat ... gebracht" or
    /// "nächsten Montag") instead of a single token. Splitting on whitespace and dropping
    /// ellipsis placeholders lets each real word in the phrase match independently in
    /// <see cref="BuildSentenceWords"/>, which otherwise only compares whole tokens.
    /// </summary>
    private static List<SentenceHighlight> ExpandHighlights(IReadOnlyList<SentenceHighlight> highlights)
    {
        var expanded = new List<SentenceHighlight>(highlights.Count);
        foreach (var highlight in highlights)
        {
            var normalized = highlight.Word.Replace("...", " ", StringComparison.Ordinal).Replace('…', ' ');
            foreach (var part in normalized.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                if (part.Trim('.', ',', '!', '?', ';', ':', '-').Length == 0)
                {
                    continue;
                }

                expanded.Add(highlight with { Word = part });
            }
        }

        return expanded;
    }

    private static IReadOnlyList<SentenceWordViewModel> BuildSentenceWords(string? sentence, IReadOnlyList<SentenceHighlight> highlights)
    {
        if (string.IsNullOrWhiteSpace(sentence))
        {
            return [];
        }

        var remaining = ExpandHighlights(highlights);
        var tokens = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var result = new List<SentenceWordViewModel>(tokens.Length);

        foreach (var token in tokens)
        {
            var stripped = token.Trim('.', ',', '!', '?', ';', ':');
            var matchIndex = remaining.FindIndex(h => string.Equals(h.Word, stripped, StringComparison.OrdinalIgnoreCase));
            if (matchIndex >= 0)
            {
                result.Add(new SentenceWordViewModel(token, true, remaining[matchIndex].Reason));
                remaining.RemoveAt(matchIndex);
            }
            else
            {
                result.Add(new SentenceWordViewModel(token, false, null));
            }
        }

        return result;
    }
}

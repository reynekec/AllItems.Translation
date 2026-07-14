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
    private readonly List<int> _currentSessionAgainWordIds = [];
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

    partial void OnSourceLanguageChanged(Language oldValue, Language newValue) => _ = RefreshAvailabilityAsync();

    partial void OnTargetLanguageChanged(Language oldValue, Language newValue) => _ = RefreshAvailabilityAsync();

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
    private string activeProgressDetail = string.Empty;

    [ObservableProperty]
    private string? statusMessage;

    [ObservableProperty]
    private int reviewedCount;

    [ObservableProperty]
    private bool isLeechMode;

    [ObservableProperty]
    private bool isRetrainCurrentMode;

    [ObservableProperty]
    private int availableCardCount;

    [ObservableProperty]
    private string retrainMissedAvailabilityText = string.Empty;

    [ObservableProperty]
    private string retrainTroubleAvailabilityText = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(StartLeechSessionCommand))]
    private int retrainTroubleCount;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(StartRetrainCurrentSessionCommand))]
    private int retrainMissedCount;

    [ObservableProperty]
    private string availableWordsText = string.Empty;

    [ObservableProperty]
    private string retrainMissedEmptyText = string.Empty;

    [ObservableProperty]
    private string retrainTroubleEmptyText = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GradeCommand))]
    [NotifyCanExecuteChangedFor(nameof(StartSessionCommand))]
    [NotifyCanExecuteChangedFor(nameof(StartLeechSessionCommand))]
    [NotifyCanExecuteChangedFor(nameof(StartRetrainCurrentSessionCommand))]
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
            IsLeechMode = false;
            IsRetrainCurrentMode = false;
            ReviewedCount = 0;
            _currentSessionAgainWordIds.Clear();
            _currentIndex = 0;
            _cards = await _studySessionService.BuildSessionAsync(SourceLanguage, TargetLanguage, SessionSize);
            AvailableCardCount = _cards.Count;

            if (_cards.Count == 0)
            {
                IsSessionActive = false;
                StatusMessage = "No due or new words for this language pair yet - keep translating sentences to build up your dictionary.";
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

    [RelayCommand(CanExecute = nameof(CanStartLeech))]
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
            IsRetrainCurrentMode = false;
            ReviewedCount = 0;
            _currentIndex = 0;
            _cards = await _studySessionService.BuildLeechSessionAsync(SourceLanguage, TargetLanguage, SessionSize);
            AvailableCardCount = _cards.Count;

            if (_cards.Count == 0)
            {
                IsSessionActive = false;
                StatusMessage = "No trouble words yet - keep studying and any tricky words will show up here.";
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

    [RelayCommand(CanExecute = nameof(CanStartRetrainCurrent))]
    private async Task StartRetrainCurrentSessionAsync()
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
            IsRetrainCurrentMode = true;
            ReviewedCount = 0;
            _currentIndex = 0;

            _cards = await _studySessionService.BuildRetrainSessionAsync(
                SourceLanguage,
                TargetLanguage,
                _currentSessionAgainWordIds,
                SessionSize);
            AvailableCardCount = _cards.Count;

            if (_cards.Count == 0)
            {
                IsSessionActive = false;
                StatusMessage = "No missed cards from your latest study session yet. Rate cards as Again, then retrain them here.";
                await RefreshAvailabilityAsync();
                return;
            }

            IsSessionActive = true;
            ShowCurrentCard();
        }
        catch (Exception ex)
        {
            IsSessionActive = false;
            StatusMessage = $"Couldn't start the retrain session: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private bool CanStart() => !IsBusy;

    private bool CanStartLeech() => CanStart() && RetrainTroubleCount > 0;

    private bool CanStartRetrainCurrent() => CanStart() && RetrainMissedCount > 0;

    [RelayCommand(CanExecute = nameof(CanStart))]
    private Task RestartAsync() =>
        IsLeechMode
            ? StartLeechSessionAsync()
            : IsRetrainCurrentMode
                ? StartRetrainCurrentSessionAsync()
                : StartSessionAsync();

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

            if (grade == ReviewGrade.Again && !_currentSessionAgainWordIds.Contains(currentCard.WordEntryId))
            {
                _currentSessionAgainWordIds.Add(currentCard.WordEntryId);
                await RefreshAvailabilityAsync();
            }

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
        ActiveProgressDetail = $"({ReviewedCount} / {AvailableCardCount})";
    }

    private async Task RefreshAvailabilityAsync()
    {
        if (SourceLanguage == TargetLanguage)
        {
            AvailableWordsText = string.Empty;
            RetrainMissedAvailabilityText = string.Empty;
            RetrainTroubleAvailabilityText = string.Empty;
            return;
        }

        var availableWordCount = await _studySessionService.GetAvailableWordCountAsync(SourceLanguage, TargetLanguage);
        var missedCount = await _studySessionService.GetRetrainCountAsync(SourceLanguage, TargetLanguage, _currentSessionAgainWordIds);
        var troubleCount = await _studySessionService.GetLeechCountAsync(TargetLanguage);

        RetrainMissedCount = missedCount;
        RetrainTroubleCount = troubleCount;

        AvailableWordsText = $"{availableWordCount} word(s) available";
        RetrainMissedAvailabilityText = $"{missedCount} card(s) ready";
        RetrainTroubleAvailabilityText = $"{troubleCount} card(s) ready";
        RetrainMissedEmptyText = missedCount == 0 ? "No cards ready yet" : string.Empty;
        RetrainTroubleEmptyText = troubleCount == 0 ? "No cards ready yet" : string.Empty;
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

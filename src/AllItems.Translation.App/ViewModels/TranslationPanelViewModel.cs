using System.Collections.ObjectModel;
using AllItems.Translation.Core.Domain;
using AllItems.Translation.Core.Translation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AllItems.Translation.App.ViewModels;

/// <summary>
/// One destination-language panel: the word-by-word reassembled sentence (clickable slots)
/// plus the fluent reference translation, for a single fixed target language.
/// </summary>
public sealed partial class TranslationPanelViewModel : ObservableObject
{
    private readonly ISentenceTranslationService _translationService;
    private SentenceTranslationResult? _current;

    [ObservableProperty]
    private Language targetLanguage;

    [ObservableProperty]
    private ObservableCollection<SlotViewModel> slots = [];

    [ObservableProperty]
    private string referenceTranslation = string.Empty;

    [ObservableProperty]
    private bool isBusy;

    public TranslationPanelViewModel(ISentenceTranslationService translationService, Language targetLanguage)
    {
        _translationService = translationService;
        this.targetLanguage = targetLanguage;
    }

    public async Task TranslateAsync(string sourceText, Language sourceLanguage, CancellationToken cancellationToken)
    {
        IsBusy = true;
        try
        {
            _current = await _translationService.TranslateAsync(sourceText, sourceLanguage, TargetLanguage, cancellationToken);
            Rebuild();
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task CycleWordAsync(SlotViewModel? slot)
    {
        if (slot is null || _current is null)
        {
            return;
        }

        _current = await _translationService.CycleWordMeaningAsync(_current, slot.TokenIndex);
        Rebuild();
    }

    private void Rebuild()
    {
        if (_current is null)
        {
            return;
        }

        Slots = new ObservableCollection<SlotViewModel>(_current.Slots.Select(s => new SlotViewModel(s)));
        ReferenceTranslation = _current.ReferenceTranslation;
    }
}

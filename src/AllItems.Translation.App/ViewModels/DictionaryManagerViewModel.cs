using System.Collections.ObjectModel;
using AllItems.Translation.Core.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AllItems.Translation.App.ViewModels;

/// <summary>Lets the user review, correct, or remove cached word translations.</summary>
public sealed partial class DictionaryManagerViewModel : ObservableObject
{
    private readonly IWordRepository _wordRepository;

    [ObservableProperty]
    private ObservableCollection<DictionaryRowViewModel> rows = [];

    [ObservableProperty]
    private bool isBusy;

    public DictionaryManagerViewModel(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
        _ = LoadAsync();
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        IsBusy = true;
        try
        {
            var words = await _wordRepository.GetAllWithTranslationsAsync();
            var list = words
                .SelectMany(word => word.Translations
                    .OrderBy(t => t.TargetLanguage)
                    .Select(t => new DictionaryRowViewModel(t.Id, word.Language, word.NormalizedText, t.TargetLanguage, t.TargetText, t.IsPreferred)))
                .ToList();

            Rows = new ObservableCollection<DictionaryRowViewModel>(list);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task SaveRowAsync(DictionaryRowViewModel? row)
    {
        if (row is null)
        {
            return;
        }

        await _wordRepository.UpdateTranslationTextAsync(row.TranslationId, row.TargetText);
    }

    [RelayCommand]
    private async Task DeleteRowAsync(DictionaryRowViewModel? row)
    {
        if (row is null)
        {
            return;
        }

        await _wordRepository.DeleteTranslationAsync(row.TranslationId);
        Rows.Remove(row);
    }
}

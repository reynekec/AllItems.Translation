using AllItems.Translation.Core.Domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AllItems.Translation.App.ViewModels;

public sealed partial class DictionaryRowViewModel(
    int translationId,
    Language sourceLanguage,
    string sourceWord,
    Language targetLanguage,
    string targetText,
    bool isPreferred) : ObservableObject
{
    public int TranslationId { get; } = translationId;
    public Language SourceLanguage { get; } = sourceLanguage;
    public string SourceWord { get; } = sourceWord;
    public Language TargetLanguage { get; } = targetLanguage;
    public bool IsPreferred { get; } = isPreferred;

    [ObservableProperty]
    private string targetText = targetText;
}

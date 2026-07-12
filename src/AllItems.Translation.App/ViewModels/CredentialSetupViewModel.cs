using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Curriculum;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AllItems.Translation.App.ViewModels;

public sealed partial class CredentialSetupViewModel(ICredentialStore credentialStore, IVocabularyImportService vocabularyImportService) : ObservableObject
{
    [ObservableProperty]
    private string jsonInput = string.Empty;

    [ObservableProperty]
    private string? errorMessage;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ImportAllVocabularyCommand))]
    private bool isImportingVocabulary;

    [ObservableProperty]
    private string? vocabularyImportStatus;

    public event Action? SavedSuccessfully;

    [RelayCommand]
    private async Task SaveAsync()
    {
        ErrorMessage = null;
        try
        {
            await credentialStore.SaveCredentialAsync(JsonInput);
            SavedSuccessfully?.Invoke();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    [RelayCommand(CanExecute = nameof(CanImportAllVocabulary))]
    private async Task ImportAllVocabularyAsync()
    {
        IsImportingVocabulary = true;
        VocabularyImportStatus = "Importing all vocabulary levels...";
        try
        {
            await vocabularyImportService.ImportAllLevelsAsync();
            VocabularyImportStatus = "All vocabulary levels have been imported into the Dictionary.";
        }
        catch (Exception ex)
        {
            VocabularyImportStatus = $"Couldn't import vocabulary: {ex.Message}";
        }
        finally
        {
            IsImportingVocabulary = false;
        }
    }

    private bool CanImportAllVocabulary() => !IsImportingVocabulary;
}

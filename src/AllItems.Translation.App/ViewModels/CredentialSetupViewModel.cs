using AllItems.Translation.Core.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AllItems.Translation.App.ViewModels;

public sealed partial class CredentialSetupViewModel(ICredentialStore credentialStore) : ObservableObject
{
    [ObservableProperty]
    private string jsonInput = string.Empty;

    [ObservableProperty]
    private string? errorMessage;

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
}

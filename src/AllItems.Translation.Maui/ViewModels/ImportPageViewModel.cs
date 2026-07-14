using AllItems.Translation.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AllItems.Translation.Maui.ViewModels;

/// <summary>
/// Pulls the latest desktop export from GitHub and installs it locally. One-way sync: importing replaces the
/// on-device database, so any grades done on the phone since the last import are discarded.
/// </summary>
public sealed partial class ImportPageViewModel(IFlashcardImportService importService) : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ImportCommand))]
    [NotifyCanExecuteChangedFor(nameof(CheckForUpdateCommand))]
    private bool isBusy;

    [ObservableProperty]
    private string statusMessage =
        "Tap Import to download the latest flashcards exported from your desktop. This replaces the cards on this phone.";

    [ObservableProperty]
    private string? lastExportText;

    [RelayCommand(CanExecute = nameof(CanRun))]
    private async Task CheckForUpdateAsync()
    {
        IsBusy = true;
        StatusMessage = "Checking GitHub for the latest export...";
        try
        {
            var manifest = await importService.TryGetRemoteManifestAsync();
            if (manifest is null)
            {
                StatusMessage = "No export found yet. Run \"Export to phone\" on the desktop first.";
                LastExportText = null;
            }
            else
            {
                LastExportText = FormatManifest(manifest);
                StatusMessage = "An export is available. Tap Import to download it.";
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"Couldn't check for updates: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand(CanExecute = nameof(CanRun))]
    private async Task ImportAsync()
    {
        IsBusy = true;
        StatusMessage = "Downloading and installing the latest flashcards...";
        try
        {
            var manifest = await importService.ImportAsync();
            LastExportText = manifest is null ? null : FormatManifest(manifest);
            StatusMessage = "Import complete. Switch to the Study tab to review your cards.";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Import failed: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private bool CanRun() => !IsBusy;

    private static string FormatManifest(Core.Sync.FlashcardSyncManifest manifest)
    {
        var kilobytes = Math.Max(1, manifest.SizeBytes / 1024);
        return $"Last exported {manifest.ExportedUtc:yyyy-MM-dd HH:mm} UTC ({kilobytes} KB).";
    }
}

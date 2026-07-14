using AllItems.Translation.Core.Sync;

namespace AllItems.Translation.Maui.Services;

/// <summary>Pulls the desktop's flashcard export from the public GitHub repo and installs it as the local database.</summary>
public interface IFlashcardImportService
{
    /// <summary>Reads the manifest (last export time / size) without downloading the whole database. Null if none exists yet.</summary>
    Task<FlashcardSyncManifest?> TryGetRemoteManifestAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Downloads the exported database over the public raw URL (no auth) and atomically replaces the local one,
    /// discarding any progress graded on the phone. Returns the manifest that was in effect, if available.
    /// </summary>
    Task<FlashcardSyncManifest?> ImportAsync(CancellationToken cancellationToken = default);
}

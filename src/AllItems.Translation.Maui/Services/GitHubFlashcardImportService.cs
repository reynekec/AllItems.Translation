using System.Text.Json;
using AllItems.Translation.Core.Sync;
using AllItems.Translation.Infrastructure.Persistence;
using Microsoft.Data.Sqlite;

namespace AllItems.Translation.Maui.Services;

/// <summary>
/// Downloads the exported SQLite database from the public repo (raw URL, no auth) and installs it as the
/// local database. Because sync is one-way, importing overwrites the local file wholesale - the phone's own
/// grades are intentionally discarded and replaced by the desktop's source-of-truth state.
/// </summary>
public sealed class GitHubFlashcardImportService(
    HttpClient httpClient,
    string databasePath,
    DatabaseInitializer databaseInitializer) : IFlashcardImportService
{
    private static readonly JsonSerializerOptions ManifestJsonOptions = new() { PropertyNameCaseInsensitive = true };

    public async Task<FlashcardSyncManifest?> TryGetRemoteManifestAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            // Cache-bust so we don't get a stale CDN copy right after an export.
            var url = $"{FlashcardSync.RawManifestUrl}?t={Guid.NewGuid():N}";
            var json = await httpClient.GetStringAsync(url, cancellationToken);
            return JsonSerializer.Deserialize<FlashcardSyncManifest>(json, ManifestJsonOptions);
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }

    public async Task<FlashcardSyncManifest?> ImportAsync(CancellationToken cancellationToken = default)
    {
        var manifest = await TryGetRemoteManifestAsync(cancellationToken);

        // Refuse an export produced by a newer app than this one - its schema may not be understood here.
        if (manifest is not null && manifest.SchemaVersion > FlashcardSync.SchemaVersion)
        {
            throw new InvalidOperationException(
                $"This export was made by a newer version of the app (sync format v{manifest.SchemaVersion}, " +
                $"this app supports v{FlashcardSync.SchemaVersion}). Update the app, then import again.");
        }

        var url = $"{FlashcardSync.RawDatabaseUrl}?t={Guid.NewGuid():N}";
        var bytes = await httpClient.GetByteArrayAsync(url, cancellationToken);

        if (bytes.Length == 0)
        {
            throw new InvalidOperationException("The downloaded flashcard database was empty.");
        }

        var tempPath = databasePath + ".download";
        await File.WriteAllBytesAsync(tempPath, bytes, cancellationToken);

        // Release any pooled SQLite handles to the old file before swapping it out from under them.
        SqliteConnection.ClearAllPools();

        // Stale write-ahead-log sidecars would otherwise shadow the freshly imported database.
        foreach (var sidecar in new[] { databasePath + "-wal", databasePath + "-shm" })
        {
            if (File.Exists(sidecar))
            {
                File.Delete(sidecar);
            }
        }

        File.Move(tempPath, databasePath, overwrite: true);

        // Reconcile the schema in case this app build is newer than the desktop that produced the export
        // (CREATE TABLE IF NOT EXISTS + additive column checks); safe and cheap on an already-current DB.
        await databaseInitializer.InitializeAsync(cancellationToken);

        return manifest;
    }
}

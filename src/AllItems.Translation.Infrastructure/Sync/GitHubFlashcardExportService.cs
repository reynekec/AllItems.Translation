using System.Text.Json;
using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Sync;
using AllItems.Translation.Infrastructure.Persistence;
using Octokit;

namespace AllItems.Translation.Infrastructure.Sync;

/// <summary>
/// Publishes the local flashcard database to the public GitHub repo via the contents API. Checkpoints the
/// SQLite WAL first (so the single .db file is complete), then creates-or-updates <c>sync/allitems.db</c> and
/// a small <c>sync/manifest.json</c> on the default branch. The phone reads these back over the raw URL.
/// </summary>
public sealed class GitHubFlashcardExportService(
    SqliteConnectionFactory connectionFactory,
    IGitHubTokenStore tokenStore,
    IClock clock) : IFlashcardExportService
{
    private static readonly JsonSerializerOptions ManifestJsonOptions = new() { WriteIndented = true };

    public bool IsConfigured => tokenStore.HasToken;

    public async Task<FlashcardExportResult> ExportAsync(CancellationToken cancellationToken = default)
    {
        var token = await tokenStore.GetTokenAsync(cancellationToken)
            ?? throw new InvalidOperationException(
                "No GitHub token is configured. Add a Personal Access Token in Settings before exporting.");

        // Flush the WAL into the main database file so the bytes we upload are complete.
        await connectionFactory.RunAsync(connection =>
        {
            using var pragma = connection.CreateCommand();
            pragma.CommandText = "PRAGMA wal_checkpoint(TRUNCATE);";
            pragma.ExecuteNonQuery();
        }, cancellationToken);

        var databaseBytes = await File.ReadAllBytesAsync(AppPaths.DatabaseFilePath, cancellationToken);
        var exportedUtc = clock.UtcNow;

        var manifest = new FlashcardSyncManifest(exportedUtc, FlashcardSync.SchemaVersion, databaseBytes.LongLength);
        var manifestJson = JsonSerializer.Serialize(manifest, ManifestJsonOptions);

        var client = new GitHubClient(new ProductHeaderValue("AllItems-Translation"))
        {
            Credentials = new Octokit.Credentials(token)
        };

        var commitMessage = $"Flashcard export {exportedUtc:yyyy-MM-dd HH:mm:ss}Z";

        // The DB is binary: base64-encode ourselves and tell Octokit not to re-encode.
        var databaseCommit = await CreateOrUpdateAsync(
            client, FlashcardSync.DatabasePath, Convert.ToBase64String(databaseBytes), commitMessage,
            convertContentToBase64: false, cancellationToken);

        // The manifest is text; let Octokit base64-encode the raw JSON.
        await CreateOrUpdateAsync(
            client, FlashcardSync.ManifestPath, manifestJson, commitMessage,
            convertContentToBase64: true, cancellationToken);

        var commitUrl = $"https://github.com/{FlashcardSync.Owner}/{FlashcardSync.Repo}/commit/{databaseCommit.Commit.Sha}";
        return new FlashcardExportResult(exportedUtc, databaseBytes.LongLength, commitUrl);
    }

    private static async Task<RepositoryContentChangeSet> CreateOrUpdateAsync(
        GitHubClient client,
        string path,
        string content,
        string message,
        bool convertContentToBase64,
        CancellationToken cancellationToken)
    {
        string? existingSha = null;
        try
        {
            var existing = await client.Repository.Content.GetAllContentsByRef(
                FlashcardSync.Owner, FlashcardSync.Repo, path, FlashcardSync.Branch);
            existingSha = existing.Count > 0 ? existing[0].Sha : null;
        }
        catch (NotFoundException)
        {
            // First export - the file doesn't exist yet, so create it below.
        }

        cancellationToken.ThrowIfCancellationRequested();

        if (existingSha is null)
        {
            return await client.Repository.Content.CreateFile(
                FlashcardSync.Owner, FlashcardSync.Repo, path,
                new CreateFileRequest(message, content, FlashcardSync.Branch, convertContentToBase64));
        }

        return await client.Repository.Content.UpdateFile(
            FlashcardSync.Owner, FlashcardSync.Repo, path,
            new UpdateFileRequest(message, content, existingSha, FlashcardSync.Branch, convertContentToBase64));
    }
}

using System.Text.Json;
using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Sync;
using AllItems.Translation.Infrastructure.Persistence;
using Octokit;

namespace AllItems.Translation.Infrastructure.Sync;

/// <summary>
/// Publishes the local flashcard database to the public GitHub repo via the Git Data API (blob -> tree ->
/// commit -> ref update). Checkpoints the SQLite WAL first (so the single .db file is complete), then writes
/// <c>sync/allitems.db</c> and <c>sync/manifest.json</c> in one atomic commit. The Git Data API accepts blobs
/// far larger than the Contents API's ~1 MB limit, so a full vocabulary database exports fine, and both files
/// land in a single commit rather than two. The phone reads these back over the raw URL.
/// </summary>
public sealed class GitHubFlashcardExportService(
    SqliteConnectionFactory connectionFactory,
    IGitHubTokenStore tokenStore,
    IClock clock) : IFlashcardExportService
{
    private static readonly JsonSerializerOptions ManifestJsonOptions = new() { WriteIndented = true };

    /// <summary>Git mode for a normal, non-executable file.</summary>
    private const string BlobFileMode = "100644";

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

        var owner = FlashcardSync.Owner;
        var repo = FlashcardSync.Repo;
        var reference = $"heads/{FlashcardSync.Branch}";

        // 1. Upload both files as blobs (base64 for the binary DB, UTF-8 for the manifest text).
        var databaseBlob = await client.Git.Blob.Create(owner, repo, new NewBlob
        {
            Encoding = EncodingType.Base64,
            Content = Convert.ToBase64String(databaseBytes)
        });
        var manifestBlob = await client.Git.Blob.Create(owner, repo, new NewBlob
        {
            Encoding = EncodingType.Utf8,
            Content = manifestJson
        });

        cancellationToken.ThrowIfCancellationRequested();

        // 2. Base a new tree on the branch's current commit so we don't drop the rest of the repo.
        var branchReference = await client.Git.Reference.Get(owner, repo, reference);
        var parentCommitSha = branchReference.Object.Sha;
        var parentCommit = await client.Git.Commit.Get(owner, repo, parentCommitSha);

        var newTree = new NewTree { BaseTree = parentCommit.Tree.Sha };
        newTree.Tree.Add(new NewTreeItem
        {
            Path = FlashcardSync.DatabasePath,
            Mode = BlobFileMode,
            Type = TreeType.Blob,
            Sha = databaseBlob.Sha
        });
        newTree.Tree.Add(new NewTreeItem
        {
            Path = FlashcardSync.ManifestPath,
            Mode = BlobFileMode,
            Type = TreeType.Blob,
            Sha = manifestBlob.Sha
        });
        var createdTree = await client.Git.Tree.Create(owner, repo, newTree);

        // 3. Commit the tree and move the branch to it - one atomic commit for both files.
        var commitMessage = $"Flashcard export {exportedUtc:yyyy-MM-dd HH:mm:ss}Z";
        var createdCommit = await client.Git.Commit.Create(owner, repo, new NewCommit(commitMessage, createdTree.Sha, parentCommitSha));
        await client.Git.Reference.Update(owner, repo, reference, new ReferenceUpdate(createdCommit.Sha));

        var commitUrl = $"https://github.com/{owner}/{repo}/commit/{createdCommit.Sha}";
        return new FlashcardExportResult(exportedUtc, databaseBytes.LongLength, commitUrl);
    }
}

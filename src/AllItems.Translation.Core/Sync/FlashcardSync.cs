namespace AllItems.Translation.Core.Sync;

/// <summary>
/// Shared, single source of truth for where the one-way flashcard sync lives in the GitHub repo.
/// The desktop pushes to these paths; the phone reads the database over the public raw URL (no auth,
/// because the repo is public).
/// </summary>
public static class FlashcardSync
{
    public const string Owner = "reynekec";
    public const string Repo = "AllItems.Translation";
    public const string Branch = "main";

    public const string DatabasePath = "sync/allitems.db";
    public const string ManifestPath = "sync/manifest.json";

    /// <summary>Bumped when the exported payload's shape changes, so the phone can warn on a mismatch.</summary>
    public const int SchemaVersion = 1;

    /// <summary>Public, unauthenticated download URL for the exported database.</summary>
    public static string RawDatabaseUrl { get; } =
        $"https://raw.githubusercontent.com/{Owner}/{Repo}/{Branch}/{DatabasePath}";

    /// <summary>Public, unauthenticated download URL for the export manifest.</summary>
    public static string RawManifestUrl { get; } =
        $"https://raw.githubusercontent.com/{Owner}/{Repo}/{Branch}/{ManifestPath}";
}

/// <summary>Small metadata document written alongside the database so a client can show "last exported" and detect no-ops.</summary>
public sealed record FlashcardSyncManifest(DateTime ExportedUtc, int SchemaVersion, long SizeBytes);

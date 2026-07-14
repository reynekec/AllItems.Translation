namespace AllItems.Translation.Core.Abstractions;

/// <summary>The outcome of a flashcard export - what was written and when, for a "last exported" confirmation.</summary>
public sealed record FlashcardExportResult(DateTime ExportedUtc, long SizeBytes, string CommitUrl);

/// <summary>
/// Publishes the local flashcard database to the shared GitHub repo so the phone can import it.
/// One-way: desktop is the source of truth; the phone only ever reads what this writes.
/// </summary>
public interface IFlashcardExportService
{
    /// <summary>True once a GitHub token has been configured, so the UI can enable/disable the Export action.</summary>
    bool IsConfigured { get; }

    /// <summary>
    /// Checkpoints and uploads the local SQLite database (plus a small manifest) to the repo's sync folder.
    /// Throws <see cref="InvalidOperationException"/> if no GitHub token is configured.
    /// </summary>
    Task<FlashcardExportResult> ExportAsync(CancellationToken cancellationToken = default);
}

namespace AllItems.Translation.Core.Abstractions;

/// <summary>
/// Manages the GitHub Personal Access Token used to push the flashcard export to the repo, stored on disk
/// outside the repo/source tree - the same isolation the Google service-account key gets, so the secret is
/// never embedded in code or committed to version control.
/// </summary>
public interface IGitHubTokenStore
{
    bool HasToken { get; }

    Task<string?> GetTokenAsync(CancellationToken cancellationToken = default);

    Task SaveTokenAsync(string token, CancellationToken cancellationToken = default);
}

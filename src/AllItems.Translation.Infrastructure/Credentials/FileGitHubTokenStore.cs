using AllItems.Translation.Core.Abstractions;

namespace AllItems.Translation.Infrastructure.Credentials;

/// <summary>
/// Stores the GitHub Personal Access Token under %LOCALAPPDATA%\AllItems.Translation\credentials, well outside
/// the repo, so it's never at risk of being committed - the same isolation <see cref="FileCredentialStore"/>
/// gives the Google service-account key.
/// </summary>
public sealed class FileGitHubTokenStore : IGitHubTokenStore
{
    public bool HasToken => File.Exists(AppPaths.GitHubTokenFilePath);

    public async Task<string?> GetTokenAsync(CancellationToken cancellationToken = default)
    {
        if (!HasToken)
        {
            return null;
        }

        var token = (await File.ReadAllTextAsync(AppPaths.GitHubTokenFilePath, cancellationToken)).Trim();
        return string.IsNullOrEmpty(token) ? null : token;
    }

    public async Task SaveTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new FormatException("The GitHub token is empty.");
        }

        AppPaths.EnsureDirectoriesExist();
        await File.WriteAllTextAsync(AppPaths.GitHubTokenFilePath, token.Trim(), cancellationToken);
    }
}

using System.Text.Json;
using AllItems.Translation.Core.Abstractions;

namespace AllItems.Translation.Infrastructure.Credentials;

/// <summary>
/// Stores the Google Cloud service-account JSON under %LOCALAPPDATA%\AllItems.Translation\credentials,
/// well outside the repo, so it's never at risk of being committed to source control.
/// </summary>
public sealed class FileCredentialStore : ICredentialStore
{
    public bool HasCredential => File.Exists(AppPaths.CredentialFilePath);

    public string? CredentialFilePath => HasCredential ? AppPaths.CredentialFilePath : null;

    public async Task SaveCredentialAsync(string serviceAccountJson, CancellationToken cancellationToken = default)
    {
        using (var document = JsonDocument.Parse(serviceAccountJson))
        {
            if (!document.RootElement.TryGetProperty("type", out var typeProperty) ||
                typeProperty.GetString() != "service_account")
            {
                throw new FormatException("This does not look like a Google Cloud service-account JSON key (missing \"type\": \"service_account\").");
            }
        }

        AppPaths.EnsureDirectoriesExist();
        await File.WriteAllTextAsync(AppPaths.CredentialFilePath, serviceAccountJson, cancellationToken);
    }
}

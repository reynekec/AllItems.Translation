namespace AllItems.Translation.Core.Abstractions;

/// <summary>
/// Manages the Google Cloud service-account credential on disk, outside the repo/source tree,
/// so the app never needs the secret embedded in code or committed to version control.
/// </summary>
public interface ICredentialStore
{
    bool HasCredential { get; }

    string? CredentialFilePath { get; }

    Task SaveCredentialAsync(string serviceAccountJson, CancellationToken cancellationToken = default);
}

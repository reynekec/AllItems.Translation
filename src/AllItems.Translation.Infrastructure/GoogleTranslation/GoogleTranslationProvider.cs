using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Domain;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Translation.V2;
using Language = AllItems.Translation.Core.Domain.Language;

namespace AllItems.Translation.Infrastructure.GoogleTranslation;

/// <summary>
/// Translates via the official Google Cloud Translation API using a service-account credential
/// loaded from disk through <see cref="ICredentialStore"/> (never embedded in code or config).
/// </summary>
public sealed class GoogleTranslationProvider(ICredentialStore credentialStore) : ITranslationProvider
{
    private readonly SemaphoreSlim _clientLock = new(1, 1);
    private TranslationClient? _client;

    public async Task<string> TranslateWordAsync(string word, Language source, Language target, CancellationToken cancellationToken = default)
    {
        var client = await GetClientAsync(cancellationToken);
        var result = await client.TranslateTextAsync(word, target.ToBcp47(), source.ToBcp47(), cancellationToken: cancellationToken);
        return result.TranslatedText;
    }

    public async Task<string> TranslateSentenceAsync(string sentence, Language source, Language target, CancellationToken cancellationToken = default)
    {
        var client = await GetClientAsync(cancellationToken);
        var result = await client.TranslateTextAsync(sentence, target.ToBcp47(), source.ToBcp47(), cancellationToken: cancellationToken);
        return result.TranslatedText;
    }

    private async Task<TranslationClient> GetClientAsync(CancellationToken cancellationToken)
    {
        if (_client is not null)
        {
            return _client;
        }

        await _clientLock.WaitAsync(cancellationToken);
        try
        {
            if (_client is not null)
            {
                return _client;
            }

            if (credentialStore.CredentialFilePath is not { } path)
            {
                throw new InvalidOperationException(
                    "No Google Cloud credential is configured yet. Add your service-account JSON key in Settings first.");
            }

            var credential = CredentialFactory.FromFile<ServiceAccountCredential>(path).ToGoogleCredential();
            _client = TranslationClient.Create(credential);
            return _client;
        }
        finally
        {
            _clientLock.Release();
        }
    }
}

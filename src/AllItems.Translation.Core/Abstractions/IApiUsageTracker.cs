namespace AllItems.Translation.Core.Abstractions;

/// <summary>Tracks characters sent to the paid translation API, purely for the on-screen usage counter.</summary>
public interface IApiUsageTracker
{
    Task RecordCharactersAsync(int characterCount, CancellationToken cancellationToken = default);

    Task<long> GetCurrentMonthUsageAsync(CancellationToken cancellationToken = default);
}

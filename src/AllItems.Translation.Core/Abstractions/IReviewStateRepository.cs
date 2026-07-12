using AllItems.Translation.Core.Domain;

namespace AllItems.Translation.Core.Abstractions;

public interface IReviewStateRepository
{
    /// <summary>Returns the stored review state for each of the given words that has one (unseen words are simply absent).</summary>
    Task<IReadOnlyDictionary<int, WordReviewState>> GetStatesAsync(
        Language targetLanguage,
        IReadOnlyCollection<int> wordEntryIds,
        CancellationToken cancellationToken = default);

    Task UpsertAsync(WordReviewState state, CancellationToken cancellationToken = default);

    /// <summary>Words with at least <paramref name="minLapses"/> "Again" grades ever recorded - candidates for a dedicated Trouble Words practice session.</summary>
    Task<IReadOnlyList<WordReviewState>> GetLeechesAsync(
        Language targetLanguage,
        int minLapses,
        CancellationToken cancellationToken = default);
}

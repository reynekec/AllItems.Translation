using AllItems.Translation.Core.Tokenization;

namespace AllItems.Translation.Core.Translation;

/// <summary>
/// Naive nth-word-to-nth-word alignment. Cheap and dependency-free, and works reasonably well
/// for German/Afrikaans given their shared word order; degrades gracefully (fewer matches) for
/// languages where order differs more, since it is only ever a ranking hint.
/// </summary>
public sealed class PositionalWordAligner : IWordAligner
{
    public IReadOnlyList<string?> AlignWords(IReadOnlyList<string> sourceWords, string translatedSentence)
    {
        var targetWords = new SentenceTokenizer().Tokenize(translatedSentence)
            .Where(t => t.IsWord)
            .Select(t => t.Text)
            .ToArray();

        var aligned = new string?[sourceWords.Count];
        for (var i = 0; i < sourceWords.Count; i++)
        {
            aligned[i] = i < targetWords.Length ? targetWords[i] : null;
        }

        return aligned;
    }
}

namespace AllItems.Translation.Core.Translation;

/// <summary>
/// Best-effort positional word alignment between a source sentence and its fluent translation,
/// used to guess which cached meaning of a word fits this particular sentence's context.
/// This is a heuristic (German/Afrikaans word order is usually shared, English less so) -
/// it is only ever used to rank/seed candidates, never to hide the fluent reference translation.
/// </summary>
public interface IWordAligner
{
    /// <summary>
    /// Returns, for each source word index, the aligned word from the translated sentence at
    /// the same position (if any).
    /// </summary>
    IReadOnlyList<string?> AlignWords(IReadOnlyList<string> sourceWords, string translatedSentence);
}

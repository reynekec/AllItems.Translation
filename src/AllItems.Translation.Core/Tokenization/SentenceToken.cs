namespace AllItems.Translation.Core.Tokenization;

/// <summary>
/// One piece of a tokenized sentence: either a translatable word, or a verbatim separator
/// (whitespace/punctuation) that is carried through to the reassembled sentence unchanged.
/// </summary>
public sealed record SentenceToken(string Text, bool IsWord);

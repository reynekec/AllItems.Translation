using System.Text;

namespace AllItems.Translation.Core.Tokenization;

/// <summary>
/// Splits a sentence into alternating word / non-word (whitespace, punctuation) runs so the
/// non-word runs can be carried through to the reassembled translation verbatim.
/// </summary>
public sealed class SentenceTokenizer : ISentenceTokenizer
{
    public IReadOnlyList<SentenceToken> Tokenize(string sentence)
    {
        var tokens = new List<SentenceToken>();
        if (string.IsNullOrEmpty(sentence))
        {
            return tokens;
        }

        var buffer = new StringBuilder();
        bool? bufferIsWord = null;

        foreach (var c in sentence)
        {
            var isWordChar = char.IsLetter(c);

            if (bufferIsWord is null || bufferIsWord == isWordChar)
            {
                buffer.Append(c);
                bufferIsWord = isWordChar;
            }
            else
            {
                tokens.Add(new SentenceToken(buffer.ToString(), bufferIsWord.Value));
                buffer.Clear();
                buffer.Append(c);
                bufferIsWord = isWordChar;
            }
        }

        if (buffer.Length > 0 && bufferIsWord is not null)
        {
            tokens.Add(new SentenceToken(buffer.ToString(), bufferIsWord.Value));
        }

        return tokens;
    }
}

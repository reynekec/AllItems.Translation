namespace AllItems.Translation.Core.Tokenization;

public interface ISentenceTokenizer
{
    IReadOnlyList<SentenceToken> Tokenize(string sentence);
}

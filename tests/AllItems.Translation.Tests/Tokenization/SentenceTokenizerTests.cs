using AllItems.Translation.Core.Tokenization;

namespace AllItems.Translation.Tests.Tokenization;

public class SentenceTokenizerTests
{
    private readonly SentenceTokenizer _tokenizer = new();

    [Fact]
    public void Tokenize_SplitsWordsAndSeparators()
    {
        var tokens = _tokenizer.Tokenize("Ich gehe, du gehst.");

        Assert.Equal(
            [
                ("Ich", true), (" ", false), ("gehe", true), (", ", false),
                ("du", true), (" ", false), ("gehst", true), (".", false)
            ],
            tokens.Select(t => (t.Text, t.IsWord)));
    }

    [Fact]
    public void Tokenize_EmptyString_ReturnsNoTokens()
    {
        Assert.Empty(_tokenizer.Tokenize(""));
    }

    [Fact]
    public void Tokenize_PreservesGermanUmlauts_AsWordCharacters()
    {
        var tokens = _tokenizer.Tokenize("Mädchen läuft");

        Assert.Equal(["Mädchen", " ", "läuft"], tokens.Select(t => t.Text));
        Assert.True(tokens[0].IsWord);
        Assert.False(tokens[1].IsWord);
        Assert.True(tokens[2].IsWord);
    }

    [Fact]
    public void Tokenize_Reassembly_RoundTripsOriginalText()
    {
        const string original = "Wie geht es dir, Freund?";
        var tokens = _tokenizer.Tokenize(original);

        Assert.Equal(original, string.Concat(tokens.Select(t => t.Text)));
    }
}

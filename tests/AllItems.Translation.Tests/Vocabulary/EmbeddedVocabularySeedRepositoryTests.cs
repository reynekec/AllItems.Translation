using AllItems.Translation.Core.Curriculum;
using AllItems.Translation.Infrastructure.Vocabulary;

namespace AllItems.Translation.Tests.Vocabulary;

public class EmbeddedVocabularySeedRepositoryTests
{
    private readonly EmbeddedVocabularySeedRepository _repository = new();

    [Theory]
    [InlineData(CefrLevel.A1)]
    [InlineData(CefrLevel.A2)]
    [InlineData(CefrLevel.B1)]
    [InlineData(CefrLevel.B2)]
    [InlineData(CefrLevel.C1)]
    [InlineData(CefrLevel.C2)]
    public void GetWords_EveryLevel_LoadsEmbeddedResourceWithoutThrowing(CefrLevel level)
    {
        var words = _repository.GetWords(level);

        Assert.All(words, w => Assert.False(string.IsNullOrWhiteSpace(w.German)));
        Assert.All(words, w => Assert.False(string.IsNullOrWhiteSpace(w.English)));
    }

    [Fact]
    public void GetWords_CalledTwice_ReturnsCachedInstance()
    {
        var first = _repository.GetWords(CefrLevel.A1);
        var second = _repository.GetWords(CefrLevel.A1);

        Assert.Same(first, second);
    }
}

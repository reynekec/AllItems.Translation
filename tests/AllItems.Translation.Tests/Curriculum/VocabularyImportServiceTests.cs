using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Curriculum;
using AllItems.Translation.Core.Domain;
using Moq;

namespace AllItems.Translation.Tests.Curriculum;

public class VocabularyImportServiceTests
{
    private readonly Mock<IVocabularySeedRepository> _seedRepository = new();
    private readonly Mock<IVocabularyImportRepository> _importRepository = new();
    private readonly Mock<IWordRepository> _wordRepository = new();

    private VocabularyImportService CreateService() => new(_seedRepository.Object, _importRepository.Object, _wordRepository.Object);

    [Fact]
    public async Task ImportAllLevelsAsync_ImportsEveryLevelNotYetImported()
    {
        var words = new List<VocabularyWord> { new("Hund", "dog") };
        _seedRepository.Setup(r => r.GetWords(It.IsAny<CefrLevel>())).Returns(words);
        _importRepository.Setup(r => r.IsLevelImportedAsync(It.IsAny<CefrLevel>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

        await CreateService().ImportAllLevelsAsync();

        var levelCount = Enum.GetValues<CefrLevel>().Length;
        _wordRepository.Verify(r => r.ImportWordsAsync(Language.German, words, It.IsAny<CancellationToken>()), Times.Exactly(levelCount));
        foreach (var level in Enum.GetValues<CefrLevel>())
        {
            _importRepository.Verify(r => r.MarkLevelImportedAsync(level, It.IsAny<CancellationToken>()), Times.Once);
        }
    }

    [Fact]
    public async Task ImportAllLevelsAsync_SkipsLevelsAlreadyImported()
    {
        _importRepository.Setup(r => r.IsLevelImportedAsync(It.IsAny<CefrLevel>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

        await CreateService().ImportAllLevelsAsync();

        _wordRepository.Verify(r => r.ImportWordsAsync(It.IsAny<Language>(), It.IsAny<IReadOnlyList<VocabularyWord>>(), It.IsAny<CancellationToken>()), Times.Never);
        _importRepository.Verify(r => r.MarkLevelImportedAsync(It.IsAny<CefrLevel>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}

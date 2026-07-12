using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Domain;
using AllItems.Translation.Core.Tokenization;
using AllItems.Translation.Core.Translation;
using Moq;

namespace AllItems.Translation.Tests.Translation;

public class SentenceTranslationServiceTests
{
    private readonly Mock<ITranslationProvider> _provider = new();
    private readonly Mock<IWordRepository> _wordRepository = new();
    private readonly Mock<ISentenceTranslationRepository> _sentenceRepository = new();
    private readonly Mock<IApiUsageTracker> _usageTracker = new();
    private readonly Mock<IWordAligner> _wordAligner = new();
    private readonly ISentenceTokenizer _tokenizer = new SentenceTokenizer();

    private SentenceTranslationService CreateService() => new(
        _provider.Object,
        _wordRepository.Object,
        _sentenceRepository.Object,
        _usageTracker.Object,
        _tokenizer,
        _wordAligner.Object);

    private static WordTranslation Translation(int id, Language target, string text, bool preferred) => new()
    {
        Id = id,
        WordEntryId = 1,
        TargetLanguage = target,
        TargetText = text,
        IsPreferred = preferred,
        CreatedAtUtc = DateTime.UtcNow
    };

    [Fact]
    public async Task TranslateAsync_NewWord_NoContextDifference_AddsSingleOption()
    {
        var entry = new WordEntry { Id = 1, Language = Language.German, NormalizedText = "hund" };

        _sentenceRepository.Setup(r => r.FindAsync(Language.German, Language.Afrikaans, "Hund", It.IsAny<CancellationToken>()))
            .ReturnsAsync((string?)null);
        _provider.Setup(p => p.TranslateSentenceAsync("Hund", Language.German, Language.Afrikaans, It.IsAny<CancellationToken>()))
            .ReturnsAsync("hond");
        _wordAligner.Setup(a => a.AlignWords(It.IsAny<IReadOnlyList<string>>(), "hond"))
            .Returns(["hond"]);
        _wordRepository.Setup(r => r.GetOrCreateAsync(Language.German, "hund", It.IsAny<CancellationToken>()))
            .ReturnsAsync(entry);
        _wordRepository.Setup(r => r.GetTranslationsAsync(1, Language.Afrikaans, It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<WordTranslation>)[]);
        _provider.Setup(p => p.TranslateWordAsync("Hund", Language.German, Language.Afrikaans, It.IsAny<CancellationToken>()))
            .ReturnsAsync("hond");
        _wordRepository.Setup(r => r.AddTranslationAsync(1, Language.Afrikaans, "hond", true, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Translation(100, Language.Afrikaans, "hond", true));

        var service = CreateService();
        var result = await service.TranslateAsync("Hund", Language.German, Language.Afrikaans);

        var wordSlot = Assert.Single(result.Slots, s => s.IsWord);
        Assert.Single(wordSlot.Options);
        Assert.Equal("hond", wordSlot.DisplayText);
        _wordRepository.Verify(r => r.AddTranslationAsync(1, Language.Afrikaans, "hond", true, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task TranslateAsync_NewWord_ContextDiffers_PrefersContextGuessOverIsolated()
    {
        var entry = new WordEntry { Id = 1, Language = Language.German, NormalizedText = "schloss" };

        _sentenceRepository.Setup(r => r.FindAsync(It.IsAny<Language>(), It.IsAny<Language>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string?)null);
        _provider.Setup(p => p.TranslateSentenceAsync(It.IsAny<string>(), It.IsAny<Language>(), It.IsAny<Language>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync("die kasteel");
        _wordAligner.Setup(a => a.AlignWords(It.IsAny<IReadOnlyList<string>>(), It.IsAny<string>()))
            .Returns(["kasteel"]);
        _wordRepository.Setup(r => r.GetOrCreateAsync(Language.German, "schloss", It.IsAny<CancellationToken>()))
            .ReturnsAsync(entry);
        _wordRepository.Setup(r => r.GetTranslationsAsync(1, Language.Afrikaans, It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<WordTranslation>)[]);
        _provider.Setup(p => p.TranslateWordAsync("Schloss", Language.German, Language.Afrikaans, It.IsAny<CancellationToken>()))
            .ReturnsAsync("slot");
        _wordRepository.Setup(r => r.AddTranslationAsync(1, Language.Afrikaans, "kasteel", true, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Translation(100, Language.Afrikaans, "kasteel", true));
        _wordRepository.Setup(r => r.AddTranslationAsync(1, Language.Afrikaans, "slot", false, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Translation(101, Language.Afrikaans, "slot", false));

        var service = CreateService();
        var result = await service.TranslateAsync("Schloss", Language.German, Language.Afrikaans);

        var wordSlot = Assert.Single(result.Slots, s => s.IsWord);
        Assert.Equal(2, wordSlot.Options.Count);
        Assert.Equal("kasteel", wordSlot.DisplayText);
        Assert.Equal(0, wordSlot.SelectedOptionIndex);
    }

    [Fact]
    public async Task TranslateAsync_KnownWord_SkipsIsolatedApiCall_ButAddsNewContextCandidate()
    {
        var entry = new WordEntry { Id = 1, Language = Language.German, NormalizedText = "bank" };
        var existing = new List<WordTranslation> { Translation(10, Language.Afrikaans, "bench", true) };

        _sentenceRepository.Setup(r => r.FindAsync(It.IsAny<Language>(), It.IsAny<Language>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string?)null);
        _provider.Setup(p => p.TranslateSentenceAsync(It.IsAny<string>(), It.IsAny<Language>(), It.IsAny<Language>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync("die bank");
        _wordAligner.Setup(a => a.AlignWords(It.IsAny<IReadOnlyList<string>>(), It.IsAny<string>()))
            .Returns(["bank"]);
        _wordRepository.Setup(r => r.GetOrCreateAsync(Language.German, "bank", It.IsAny<CancellationToken>()))
            .ReturnsAsync(entry);
        _wordRepository.Setup(r => r.GetTranslationsAsync(1, Language.Afrikaans, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);
        _wordRepository.Setup(r => r.AddTranslationAsync(1, Language.Afrikaans, "bank", false, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Translation(11, Language.Afrikaans, "bank", false));

        var service = CreateService();
        var result = await service.TranslateAsync("Bank", Language.German, Language.Afrikaans);

        var wordSlot = Assert.Single(result.Slots, s => s.IsWord);
        Assert.Equal(2, wordSlot.Options.Count);
        Assert.Equal("bench", wordSlot.DisplayText); // still the previously preferred meaning
        _provider.Verify(p => p.TranslateWordAsync(It.IsAny<string>(), It.IsAny<Language>(), It.IsAny<Language>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CycleWordMeaningAsync_CyclesToNextOption_AndPersistsPreference()
    {
        var slot = new TranslatedSlot(
            TokenIndex: 0,
            IsWord: true,
            SourceText: "Bank",
            WordEntryId: 5,
            Options: [new WordMeaningOption(10, "bench"), new WordMeaningOption(11, "bank")],
            SelectedOptionIndex: 0);
        var currentResult = new SentenceTranslationResult(Language.German, Language.Afrikaans, "Bank", [slot], "bank");

        var service = CreateService();
        var updated = await service.CycleWordMeaningAsync(currentResult, tokenIndex: 0);

        Assert.Equal(1, updated.Slots[0].SelectedOptionIndex);
        Assert.Equal("bank", updated.Slots[0].DisplayText);
        _wordRepository.Verify(r => r.SetPreferredAsync(5, Language.Afrikaans, 11, It.IsAny<CancellationToken>()), Times.Once);
    }
}

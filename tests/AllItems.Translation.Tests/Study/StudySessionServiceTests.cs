using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Domain;
using AllItems.Translation.Core.Study;
using Moq;

namespace AllItems.Translation.Tests.Study;

public class StudySessionServiceTests
{
    private readonly Mock<IWordRepository> _wordRepository = new();
    private readonly Mock<IReviewStateRepository> _reviewStateRepository = new();
    private readonly Mock<ISpacedRepetitionScheduler> _scheduler = new();
    private readonly Mock<IClock> _clock = new();
    private static readonly DateTime Now = new(2026, 7, 12, 0, 0, 0, DateTimeKind.Utc);

    private StudySessionService CreateService() => new(
        _wordRepository.Object,
        _reviewStateRepository.Object,
        _scheduler.Object,
        _clock.Object);

    private static WordEntry WordWithTranslation(int id, string normalizedText, Language target, string translation) => new()
    {
        Id = id,
        Language = Language.German,
        NormalizedText = normalizedText,
        Translations = [new WordTranslation { Id = id * 10, WordEntryId = id, TargetLanguage = target, TargetText = translation, IsPreferred = true }]
    };

    public StudySessionServiceTests()
    {
        _clock.Setup(c => c.UtcNow).Returns(Now);
    }

    [Fact]
    public async Task BuildSessionAsync_NoWords_ReturnsEmpty()
    {
        _wordRepository.Setup(r => r.GetWordsWithPreferredTranslationAsync(Language.German, Language.Afrikaans, It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<WordEntry>)[]);

        var service = CreateService();
        var session = await service.BuildSessionAsync(Language.German, Language.Afrikaans, 20);

        Assert.Empty(session);
        _reviewStateRepository.Verify(r => r.GetStatesAsync(It.IsAny<Language>(), It.IsAny<IReadOnlyCollection<int>>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task BuildSessionAsync_PutsOverdueCardsBeforeNewCards()
    {
        var overdue = WordWithTranslation(1, "hund", Language.Afrikaans, "hond");
        var fresh = WordWithTranslation(2, "katze", Language.Afrikaans, "kat");

        _wordRepository.Setup(r => r.GetWordsWithPreferredTranslationAsync(Language.German, Language.Afrikaans, It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<WordEntry>)[fresh, overdue]);

        var overdueState = new WordReviewState { WordEntryId = 1, TargetLanguage = Language.Afrikaans, DueDateUtc = Now.AddDays(-3) };
        _reviewStateRepository.Setup(r => r.GetStatesAsync(Language.Afrikaans, It.IsAny<IReadOnlyCollection<int>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Dictionary<int, WordReviewState> { [1] = overdueState });

        var service = CreateService();
        var session = await service.BuildSessionAsync(Language.German, Language.Afrikaans, 20);

        Assert.Equal(2, session.Count);
        Assert.Equal("hund", session[0].FrontText);
        Assert.Equal("katze", session[1].FrontText);
    }

    [Fact]
    public async Task BuildSessionAsync_NotYetDueCard_IsExcluded()
    {
        var word = WordWithTranslation(1, "hund", Language.Afrikaans, "hond");

        _wordRepository.Setup(r => r.GetWordsWithPreferredTranslationAsync(Language.German, Language.Afrikaans, It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<WordEntry>)[word]);

        var notDueState = new WordReviewState { WordEntryId = 1, TargetLanguage = Language.Afrikaans, DueDateUtc = Now.AddDays(3) };
        _reviewStateRepository.Setup(r => r.GetStatesAsync(Language.Afrikaans, It.IsAny<IReadOnlyCollection<int>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Dictionary<int, WordReviewState> { [1] = notDueState });

        var service = CreateService();
        var session = await service.BuildSessionAsync(Language.German, Language.Afrikaans, 20);

        Assert.Empty(session);
    }

    [Fact]
    public async Task BuildSessionAsync_RespectsMaxCards()
    {
        var words = Enumerable.Range(1, 5)
            .Select(i => WordWithTranslation(i, $"word{i}", Language.Afrikaans, $"woord{i}"))
            .ToList();

        _wordRepository.Setup(r => r.GetWordsWithPreferredTranslationAsync(Language.German, Language.Afrikaans, It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<WordEntry>)words);
        _reviewStateRepository.Setup(r => r.GetStatesAsync(Language.Afrikaans, It.IsAny<IReadOnlyCollection<int>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Dictionary<int, WordReviewState>());

        var service = CreateService();
        var session = await service.BuildSessionAsync(Language.German, Language.Afrikaans, 3);

        Assert.Equal(3, session.Count);
    }

    [Fact]
    public async Task BuildSessionAsync_NonGermanSource_SwapsFrontAndBackSoGermanIsTheAnswer()
    {
        var word = WordWithTranslation(1, "hund", Language.English, "dog");

        _wordRepository.Setup(r => r.GetWordsWithPreferredTranslationAsync(Language.English, Language.German, It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<WordEntry>)[word]);
        _reviewStateRepository.Setup(r => r.GetStatesAsync(Language.German, It.IsAny<IReadOnlyCollection<int>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Dictionary<int, WordReviewState>());

        var service = CreateService();
        var session = await service.BuildSessionAsync(Language.English, Language.German, 20);

        var card = Assert.Single(session);
        Assert.Equal("dog", card.FrontText);
        Assert.Equal("hund", card.BackText);
        Assert.False(card.IsGermanFront);
    }

    [Fact]
    public async Task BuildLeechSessionAsync_NoLeeches_ReturnsEmpty()
    {
        _reviewStateRepository.Setup(r => r.GetLeechesAsync(Language.Afrikaans, 3, It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<WordReviewState>)[]);

        var service = CreateService();
        var session = await service.BuildLeechSessionAsync(Language.German, Language.Afrikaans, 20);

        Assert.Empty(session);
        _wordRepository.Verify(r => r.GetWordsByIdsAsync(It.IsAny<IReadOnlyCollection<int>>(), It.IsAny<Language>(), It.IsAny<Language>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task BuildLeechSessionAsync_ReturnsCardsForLeechWords()
    {
        var leechState = new WordReviewState { WordEntryId = 1, TargetLanguage = Language.Afrikaans, LapseCount = 3 };
        _reviewStateRepository.Setup(r => r.GetLeechesAsync(Language.Afrikaans, 3, It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<WordReviewState>)[leechState]);

        var word = WordWithTranslation(1, "hund", Language.Afrikaans, "hond");
        _wordRepository.Setup(r => r.GetWordsByIdsAsync(It.Is<IReadOnlyCollection<int>>(ids => ids.Contains(1)), Language.German, Language.Afrikaans, It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<WordEntry>)[word]);

        var service = CreateService();
        var session = await service.BuildLeechSessionAsync(Language.German, Language.Afrikaans, 20);

        var card = Assert.Single(session);
        Assert.Equal("hund", card.FrontText);
        Assert.Equal(3, card.ReviewState.LapseCount);
    }

    [Fact]
    public async Task BuildLeechSessionAsync_NonGermanSource_SwapsFrontAndBackSoGermanIsTheAnswer()
    {
        var leechState = new WordReviewState { WordEntryId = 1, TargetLanguage = Language.German, LapseCount = 3 };
        _reviewStateRepository.Setup(r => r.GetLeechesAsync(Language.German, 3, It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<WordReviewState>)[leechState]);

        var word = WordWithTranslation(1, "hund", Language.English, "dog");
        _wordRepository.Setup(r => r.GetWordsByIdsAsync(It.Is<IReadOnlyCollection<int>>(ids => ids.Contains(1)), Language.English, Language.German, It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<WordEntry>)[word]);

        var service = CreateService();
        var session = await service.BuildLeechSessionAsync(Language.English, Language.German, 20);

        var card = Assert.Single(session);
        Assert.Equal("dog", card.FrontText);
        Assert.Equal("hund", card.BackText);
        Assert.False(card.IsGermanFront);
    }

    [Fact]
    public async Task RecordAnswerAsync_PersistsSchedulerResult()
    {
        var card = new StudyCard(1, Language.German, "hund", null, null, [], Language.Afrikaans, "hond",
            new WordReviewState { WordEntryId = 1, TargetLanguage = Language.Afrikaans }, IsGermanFront: true);
        var scheduled = new WordReviewState { WordEntryId = 1, TargetLanguage = Language.Afrikaans, IntervalDays = 6 };

        _scheduler.Setup(s => s.Schedule(card.ReviewState, ReviewGrade.Good, Now)).Returns(scheduled);

        var service = CreateService();
        await service.RecordAnswerAsync(card, ReviewGrade.Good);

        _reviewStateRepository.Verify(r => r.UpsertAsync(scheduled, It.IsAny<CancellationToken>()), Times.Once);
    }
}

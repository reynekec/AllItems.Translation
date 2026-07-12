using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Curriculum;
using AllItems.Translation.Core.Domain;
using Moq;

namespace AllItems.Translation.Tests.Curriculum;

public class CurriculumServiceTests
{
    private readonly Mock<ICurriculumCatalog> _catalog = new();
    private readonly Mock<ICurriculumProgressRepository> _progressRepository = new();
    private readonly Mock<IExerciseGrader> _grader = new();
    private readonly Mock<IWordRepository> _wordRepository = new();

    private CurriculumService CreateService() => new(_catalog.Object, _progressRepository.Object, _grader.Object, _wordRepository.Object);

    private static CurriculumUnit UnitWithExercises(CefrLevel level, string id, int exerciseCount) => new()
    {
        Id = id,
        Level = level,
        SortOrder = 1,
        Title = id,
        Description = id,
        Exercises = Enumerable.Range(1, exerciseCount)
            .Select(i => (Exercise)new MultipleChoiceExercise
            {
                Id = $"{id}-e{i}",
                Instruction = "Choose",
                Explanation = "explanation",
                Question = "Q",
                Options = ["a", "b"],
                CorrectOptionIndex = 0
            })
            .ToList()
    };

    private void SetUpLevels(IReadOnlyDictionary<CefrLevel, IReadOnlyList<CurriculumUnit>> unitsByLevel)
    {
        foreach (var level in Enum.GetValues<CefrLevel>())
        {
            var units = unitsByLevel.TryGetValue(level, out var value) ? value : [];
            _catalog.Setup(c => c.GetUnits(level)).Returns(units);
        }
    }

    [Fact]
    public async Task GetLevelSummariesAsync_A1_IsAlwaysUnlocked()
    {
        SetUpLevels(new Dictionary<CefrLevel, IReadOnlyList<CurriculumUnit>>
        {
            [CefrLevel.A1] = [UnitWithExercises(CefrLevel.A1, "a1-u1", 2)]
        });
        _progressRepository.Setup(r => r.GetCompletedExerciseIdsAsync(It.IsAny<IReadOnlyCollection<string>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlySet<string>)new HashSet<string>());

        var service = CreateService();
        var summaries = await service.GetLevelSummariesAsync();

        Assert.True(summaries.Single(s => s.Level == CefrLevel.A1).IsUnlocked);
    }

    [Fact]
    public async Task GetLevelSummariesAsync_NextLevelStaysLocked_WhenPreviousLevelHasNoAuthoredUnitsYet()
    {
        // A1 has no content authored yet (edge case) - A2 must not falsely unlock.
        SetUpLevels(new Dictionary<CefrLevel, IReadOnlyList<CurriculumUnit>>());
        _progressRepository.Setup(r => r.GetCompletedExerciseIdsAsync(It.IsAny<IReadOnlyCollection<string>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlySet<string>)new HashSet<string>());

        var service = CreateService();
        var summaries = await service.GetLevelSummariesAsync();

        Assert.False(summaries.Single(s => s.Level == CefrLevel.A2).IsUnlocked);
    }

    [Fact]
    public async Task GetLevelSummariesAsync_NextLevelLocked_WhenPreviousLevelIncomplete()
    {
        var a1Unit = UnitWithExercises(CefrLevel.A1, "a1-u1", 2);
        SetUpLevels(new Dictionary<CefrLevel, IReadOnlyList<CurriculumUnit>> { [CefrLevel.A1] = [a1Unit] });
        _progressRepository.Setup(r => r.GetCompletedExerciseIdsAsync(It.IsAny<IReadOnlyCollection<string>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlySet<string>)new HashSet<string> { "a1-u1-e1" }); // only 1 of 2 exercises done

        var service = CreateService();
        var summaries = await service.GetLevelSummariesAsync();

        Assert.False(summaries.Single(s => s.Level == CefrLevel.A2).IsUnlocked);
    }

    [Fact]
    public async Task GetLevelSummariesAsync_NextLevelUnlocked_WhenPreviousLevelFullyComplete()
    {
        var a1Unit = UnitWithExercises(CefrLevel.A1, "a1-u1", 2);
        SetUpLevels(new Dictionary<CefrLevel, IReadOnlyList<CurriculumUnit>> { [CefrLevel.A1] = [a1Unit] });
        _progressRepository.Setup(r => r.GetCompletedExerciseIdsAsync(It.IsAny<IReadOnlyCollection<string>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlySet<string>)new HashSet<string> { "a1-u1-e1", "a1-u1-e2" });

        var service = CreateService();
        var summaries = await service.GetLevelSummariesAsync();

        Assert.True(summaries.Single(s => s.Level == CefrLevel.A2).IsUnlocked);
    }

    [Fact]
    public async Task GetUnitSummariesAsync_ReturnsCompletionCounts()
    {
        var unit = UnitWithExercises(CefrLevel.A1, "a1-u1", 3);
        _catalog.Setup(c => c.GetUnits(CefrLevel.A1)).Returns((IReadOnlyList<CurriculumUnit>)[unit]);
        _progressRepository.Setup(r => r.GetCompletedExerciseIdsAsync(It.IsAny<IReadOnlyCollection<string>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlySet<string>)new HashSet<string> { "a1-u1-e1" });

        var service = CreateService();
        var summaries = await service.GetUnitSummariesAsync(CefrLevel.A1);

        var summary = Assert.Single(summaries);
        Assert.Equal(1, summary.CompletedExerciseCount);
        Assert.Equal(3, summary.TotalExerciseCount);
    }

    [Fact]
    public async Task SubmitAnswerAsync_CorrectAnswer_MarksExerciseCompleted()
    {
        var exercise = UnitWithExercises(CefrLevel.A1, "a1-u1", 1).Exercises[0];
        var answer = new ExerciseAnswer(SelectedOptionIndex: 0);
        _grader.Setup(g => g.Grade(exercise, answer)).Returns(new GradingResult(true, "explanation"));

        var service = CreateService();
        await service.SubmitAnswerAsync(exercise, answer);

        _progressRepository.Verify(r => r.MarkExerciseCompletedAsync(exercise.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SubmitAnswerAsync_IncorrectAnswer_DoesNotMarkCompleted()
    {
        var exercise = UnitWithExercises(CefrLevel.A1, "a1-u1", 1).Exercises[0];
        var answer = new ExerciseAnswer(SelectedOptionIndex: 1);
        _grader.Setup(g => g.Grade(exercise, answer)).Returns(new GradingResult(false, "explanation"));

        var service = CreateService();
        await service.SubmitAnswerAsync(exercise, answer);

        _progressRepository.Verify(r => r.MarkExerciseCompletedAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task SubmitAnswerAsync_CorrectVocabularyExercise_AddsWordToDictionary()
    {
        var exercise = new MultipleChoiceExercise
        {
            Id = "a1-u11-farben-e1",
            Instruction = "Choose",
            Explanation = "explanation",
            Question = "\"red\" in German?",
            Options = ["rot", "blau"],
            CorrectOptionIndex = 0,
            Teaches = new VocabularyTeaching("rot", "red")
        };
        var answer = new ExerciseAnswer(SelectedOptionIndex: 0);
        _grader.Setup(g => g.Grade(exercise, answer)).Returns(new GradingResult(true, "explanation"));

        var entry = new WordEntry { Id = 1, Language = Language.German, NormalizedText = "rot" };
        _wordRepository.Setup(r => r.GetOrCreateAsync(Language.German, "rot", It.IsAny<CancellationToken>())).ReturnsAsync(entry);
        _wordRepository.Setup(r => r.GetTranslationsAsync(1, Language.English, It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<WordTranslation>)[]);

        var service = CreateService();
        await service.SubmitAnswerAsync(exercise, answer);

        _wordRepository.Verify(r => r.AddTranslationAsync(1, Language.English, "red", true, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SubmitAnswerAsync_VocabularyWordAlreadyKnown_DoesNotAddDuplicate()
    {
        var exercise = new MultipleChoiceExercise
        {
            Id = "a1-u11-farben-e1",
            Instruction = "Choose",
            Explanation = "explanation",
            Question = "\"red\" in German?",
            Options = ["rot", "blau"],
            CorrectOptionIndex = 0,
            Teaches = new VocabularyTeaching("rot", "red")
        };
        var answer = new ExerciseAnswer(SelectedOptionIndex: 0);
        _grader.Setup(g => g.Grade(exercise, answer)).Returns(new GradingResult(true, "explanation"));

        var entry = new WordEntry { Id = 1, Language = Language.German, NormalizedText = "rot" };
        _wordRepository.Setup(r => r.GetOrCreateAsync(Language.German, "rot", It.IsAny<CancellationToken>())).ReturnsAsync(entry);
        _wordRepository.Setup(r => r.GetTranslationsAsync(1, Language.English, It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<WordTranslation>)[new WordTranslation { Id = 5, WordEntryId = 1, TargetLanguage = Language.English, TargetText = "red", IsPreferred = true }]);

        var service = CreateService();
        await service.SubmitAnswerAsync(exercise, answer);

        _wordRepository.Verify(r => r.AddTranslationAsync(It.IsAny<int>(), It.IsAny<Language>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task SubmitAnswerAsync_GrammarExerciseWithNoTeaches_NeverTouchesWordRepository()
    {
        var exercise = UnitWithExercises(CefrLevel.A1, "a1-u1", 1).Exercises[0];
        var answer = new ExerciseAnswer(SelectedOptionIndex: 0);
        _grader.Setup(g => g.Grade(exercise, answer)).Returns(new GradingResult(true, "explanation"));

        var service = CreateService();
        await service.SubmitAnswerAsync(exercise, answer);

        _wordRepository.Verify(r => r.GetOrCreateAsync(It.IsAny<Language>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}

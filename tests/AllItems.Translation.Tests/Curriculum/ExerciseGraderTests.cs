using AllItems.Translation.Core.Curriculum;

namespace AllItems.Translation.Tests.Curriculum;

public class ExerciseGraderTests
{
    private readonly ExerciseGrader _grader = new();

    private static MultipleChoiceExercise MultipleChoice() => new()
    {
        Id = "mc-1",
        Instruction = "Choose",
        Explanation = "explanation",
        Question = "___ Mann",
        Options = ["der", "die", "das"],
        CorrectOptionIndex = 0
    };

    private static ClozeExercise Cloze() => new()
    {
        Id = "cloze-1",
        Instruction = "Fill in",
        Explanation = "explanation",
        TextBefore = "Ich ",
        TextAfter = " dreißig Jahre alt.",
        CorrectAnswer = "bin"
    };

    private static ClozeExercise ClozeWithSynonyms() => new()
    {
        Id = "cloze-2",
        Instruction = "Fill in",
        Explanation = "explanation",
        TextBefore = "Das ist ",
        TextAfter = " gut.",
        CorrectAnswer = "okay",
        AcceptedAnswers = ["ok", "in ordnung"]
    };

    private static SentenceOrderExercise SentenceOrder() => new()
    {
        Id = "order-1",
        Instruction = "Order",
        Explanation = "explanation",
        ScrambledWords = ["kommen", "Woher", "Sie", "?"],
        CorrectOrder = ["Woher", "kommen", "Sie", "?"]
    };

    private static ConjugationDrillExercise ConjugationDrill() => new()
    {
        Id = "drill-1",
        Instruction = "Conjugate",
        Explanation = "explanation",
        BaseWord = "sein",
        Slots =
        [
            new ConjugationSlot("ich", "bin"),
            new ConjugationSlot("du", "bist")
        ]
    };

    [Fact]
    public void Grade_MultipleChoice_CorrectIndex_IsCorrect()
    {
        var result = _grader.Grade(MultipleChoice(), new ExerciseAnswer(SelectedOptionIndex: 0));
        Assert.True(result.IsCorrect);
    }

    [Fact]
    public void Grade_MultipleChoice_WrongIndex_IsIncorrect()
    {
        var result = _grader.Grade(MultipleChoice(), new ExerciseAnswer(SelectedOptionIndex: 1));
        Assert.False(result.IsCorrect);
    }

    [Fact]
    public void Grade_MultipleChoice_NoSelection_IsIncorrect()
    {
        var result = _grader.Grade(MultipleChoice(), new ExerciseAnswer());
        Assert.False(result.IsCorrect);
    }

    [Theory]
    [InlineData("bin", true)]
    [InlineData("Bin", true)]
    [InlineData("  bin  ", true)]
    [InlineData("bist", false)]
    [InlineData(null, false)]
    public void Grade_Cloze_IsLenientButAccurate(string? typed, bool expectedCorrect)
    {
        var result = _grader.Grade(Cloze(), new ExerciseAnswer(TypedText: typed));
        Assert.Equal(expectedCorrect, result.IsCorrect);
    }

    [Theory]
    [InlineData("okay")]
    [InlineData("OK")]
    [InlineData("in ordnung")]
    [InlineData(" in   ordnung ")]
    public void Grade_Cloze_AcceptsConfiguredSynonyms(string typed)
    {
        var result = _grader.Grade(ClozeWithSynonyms(), new ExerciseAnswer(TypedText: typed));

        Assert.True(result.IsCorrect);
    }

    [Fact]
    public void Grade_Cloze_RejectsUnconfiguredAlternative()
    {
        var result = _grader.Grade(ClozeWithSynonyms(), new ExerciseAnswer(TypedText: "gut"));

        Assert.False(result.IsCorrect);
    }

    [Fact]
    public void Grade_SentenceOrder_CorrectSequence_IsCorrect()
    {
        var result = _grader.Grade(SentenceOrder(), new ExerciseAnswer(WordOrder: ["Woher", "kommen", "Sie", "?"]));
        Assert.True(result.IsCorrect);
    }

    [Fact]
    public void Grade_SentenceOrder_WrongSequence_IsIncorrect()
    {
        var result = _grader.Grade(SentenceOrder(), new ExerciseAnswer(WordOrder: ["kommen", "Woher", "Sie", "?"]));
        Assert.False(result.IsCorrect);
    }

    [Fact]
    public void Grade_SentenceOrder_IncompleteSequence_IsIncorrect()
    {
        var result = _grader.Grade(SentenceOrder(), new ExerciseAnswer(WordOrder: ["Woher", "kommen"]));
        Assert.False(result.IsCorrect);
    }

    [Fact]
    public void Grade_ConjugationDrill_AllSlotsCorrect_IsCorrect()
    {
        var answer = new ExerciseAnswer(ConjugationAnswers: new Dictionary<string, string> { ["ich"] = "bin", ["du"] = "bist" });
        var result = _grader.Grade(ConjugationDrill(), answer);

        Assert.True(result.IsCorrect);
        Assert.True(result.SlotCorrectness!["ich"]);
        Assert.True(result.SlotCorrectness!["du"]);
    }

    [Fact]
    public void Grade_ConjugationDrill_OneSlotWrong_IsIncorrectButIdentifiesWhichSlot()
    {
        var answer = new ExerciseAnswer(ConjugationAnswers: new Dictionary<string, string> { ["ich"] = "bin", ["du"] = "bin" });
        var result = _grader.Grade(ConjugationDrill(), answer);

        Assert.False(result.IsCorrect);
        Assert.True(result.SlotCorrectness!["ich"]);
        Assert.False(result.SlotCorrectness!["du"]);
    }

    [Fact]
    public void Grade_ConjugationDrill_MissingSlot_IsIncorrect()
    {
        var answer = new ExerciseAnswer(ConjugationAnswers: new Dictionary<string, string> { ["ich"] = "bin" });
        var result = _grader.Grade(ConjugationDrill(), answer);

        Assert.False(result.IsCorrect);
        Assert.False(result.SlotCorrectness!["du"]);
    }
}

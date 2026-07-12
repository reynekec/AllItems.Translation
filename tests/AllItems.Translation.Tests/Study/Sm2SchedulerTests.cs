using AllItems.Translation.Core.Domain;
using AllItems.Translation.Core.Study;

namespace AllItems.Translation.Tests.Study;

public class Sm2SchedulerTests
{
    private readonly Sm2Scheduler _scheduler = new();
    private static readonly DateTime Now = new(2026, 7, 12, 0, 0, 0, DateTimeKind.Utc);

    private static WordReviewState NewCard() => new()
    {
        WordEntryId = 1,
        TargetLanguage = Language.Afrikaans,
        EasinessFactor = 2.5,
        IntervalDays = 0,
        Repetitions = 0,
        DueDateUtc = null,
        LastReviewedUtc = null
    };

    [Fact]
    public void Schedule_FirstGood_SetsOneDayIntervalAndOneRepetition()
    {
        var result = _scheduler.Schedule(NewCard(), ReviewGrade.Good, Now);

        Assert.Equal(1, result.Repetitions);
        Assert.Equal(1, result.IntervalDays);
        Assert.Equal(Now.Date.AddDays(1), result.DueDateUtc);
    }

    [Fact]
    public void Schedule_SecondGood_SetsSixDayInterval()
    {
        var afterFirst = _scheduler.Schedule(NewCard(), ReviewGrade.Good, Now);
        var afterSecond = _scheduler.Schedule(afterFirst, ReviewGrade.Good, Now.AddDays(1));

        Assert.Equal(2, afterSecond.Repetitions);
        Assert.Equal(6, afterSecond.IntervalDays);
    }

    [Fact]
    public void Schedule_ThirdGood_MultipliesPreviousIntervalByEasiness()
    {
        var state = NewCard();
        state = _scheduler.Schedule(state, ReviewGrade.Good, Now);
        state = _scheduler.Schedule(state, ReviewGrade.Good, Now);
        var afterThird = _scheduler.Schedule(state, ReviewGrade.Good, Now);

        var expectedInterval = (int)Math.Round(state.IntervalDays * state.EasinessFactor, MidpointRounding.AwayFromZero);
        Assert.Equal(3, afterThird.Repetitions);
        Assert.Equal(expectedInterval, afterThird.IntervalDays);
    }

    [Fact]
    public void Schedule_Again_ResetsRepetitionsAndIntervalRegardlessOfHistory()
    {
        var state = NewCard();
        state = _scheduler.Schedule(state, ReviewGrade.Good, Now);
        state = _scheduler.Schedule(state, ReviewGrade.Good, Now);

        var afterAgain = _scheduler.Schedule(state, ReviewGrade.Again, Now);

        Assert.Equal(0, afterAgain.Repetitions);
        Assert.Equal(1, afterAgain.IntervalDays);
        Assert.Equal(Now.Date.AddDays(1), afterAgain.DueDateUtc);
    }

    [Fact]
    public void Schedule_Again_NeverDropsEasinessBelowMinimum()
    {
        var state = NewCard();
        for (var i = 0; i < 20; i++)
        {
            state = _scheduler.Schedule(state, ReviewGrade.Again, Now);
        }

        Assert.True(state.EasinessFactor >= 1.3);
    }

    [Fact]
    public void Schedule_Easy_IncreasesEasinessMoreThanGood()
    {
        var afterGood = _scheduler.Schedule(NewCard(), ReviewGrade.Good, Now);
        var afterEasy = _scheduler.Schedule(NewCard(), ReviewGrade.Easy, Now);

        Assert.True(afterEasy.EasinessFactor > afterGood.EasinessFactor);
    }

    [Fact]
    public void Schedule_Again_IncrementsLapseCount()
    {
        var state = NewCard();
        state = _scheduler.Schedule(state, ReviewGrade.Again, Now);
        state = _scheduler.Schedule(state, ReviewGrade.Good, Now);
        state = _scheduler.Schedule(state, ReviewGrade.Again, Now);

        Assert.Equal(2, state.LapseCount);
    }

    [Fact]
    public void Schedule_Good_DoesNotChangeLapseCount()
    {
        var state = NewCard();
        state = _scheduler.Schedule(state, ReviewGrade.Again, Now);
        state = _scheduler.Schedule(state, ReviewGrade.Good, Now);

        Assert.Equal(1, state.LapseCount);
    }

    [Fact]
    public void Schedule_PreservesIdentityFields()
    {
        var state = NewCard();
        state.Id = 42;
        state.WordEntryId = 7;
        state.TargetLanguage = Language.English;

        var updated = _scheduler.Schedule(state, ReviewGrade.Good, Now);

        Assert.Equal(42, updated.Id);
        Assert.Equal(7, updated.WordEntryId);
        Assert.Equal(Language.English, updated.TargetLanguage);
    }
}

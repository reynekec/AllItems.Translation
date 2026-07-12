using AllItems.Translation.Core.Domain;

namespace AllItems.Translation.Core.Study;

/// <summary>
/// Classic SM-2 spaced-repetition algorithm, adapted to a 3-button grading scale by mapping
/// Again/Good/Easy onto SM-2's 0-5 quality score (2/4/5 respectively).
/// </summary>
public sealed class Sm2Scheduler : ISpacedRepetitionScheduler
{
    public WordReviewState Schedule(WordReviewState current, ReviewGrade grade, DateTime nowUtc)
    {
        var quality = grade switch
        {
            ReviewGrade.Again => 2,
            ReviewGrade.Good => 4,
            ReviewGrade.Easy => 5,
            _ => throw new ArgumentOutOfRangeException(nameof(grade), grade, null)
        };

        var easinessFactor = Math.Max(
            1.3,
            current.EasinessFactor + (0.1 - (5 - quality) * (0.08 + (5 - quality) * 0.02)));

        int repetitions;
        int intervalDays;

        if (quality < 3)
        {
            repetitions = 0;
            intervalDays = 1;
        }
        else
        {
            repetitions = current.Repetitions + 1;
            intervalDays = repetitions switch
            {
                1 => 1,
                2 => 6,
                _ => (int)Math.Round(current.IntervalDays * easinessFactor, MidpointRounding.AwayFromZero)
            };
        }

        return new WordReviewState
        {
            Id = current.Id,
            WordEntryId = current.WordEntryId,
            TargetLanguage = current.TargetLanguage,
            EasinessFactor = easinessFactor,
            IntervalDays = intervalDays,
            Repetitions = repetitions,
            DueDateUtc = nowUtc.Date.AddDays(intervalDays),
            LastReviewedUtc = nowUtc
        };
    }
}

using AllItems.Translation.Core.Domain;

namespace AllItems.Translation.Core.Study;

/// <summary>Pure scheduling algorithm: given the current review state and a self-graded answer, computes the next state.</summary>
public interface ISpacedRepetitionScheduler
{
    WordReviewState Schedule(WordReviewState current, ReviewGrade grade, DateTime nowUtc);
}

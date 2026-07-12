namespace AllItems.Translation.Core.Abstractions;

/// <summary>Testable seam over the current time (used for "yyyy-MM" usage buckets and record timestamps).</summary>
public interface IClock
{
    DateTime UtcNow { get; }
}

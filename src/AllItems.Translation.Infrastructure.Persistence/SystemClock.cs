using AllItems.Translation.Core.Abstractions;

namespace AllItems.Translation.Infrastructure;

public sealed class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}

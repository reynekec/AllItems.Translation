namespace AllItems.Translation.Core.Abstractions;

public interface IStartupPreferenceStore
{
    /// <summary>
    /// Gets whether the application should start when Windows starts.
    /// </summary>
    bool IsRunAtStartupEnabled { get; }

    /// <summary>
    /// Sets whether the application should start when Windows starts.
    /// </summary>
    Task SetRunAtStartupAsync(bool enabled);
}

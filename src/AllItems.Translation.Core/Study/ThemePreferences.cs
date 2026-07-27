namespace AllItems.Translation.Core.Study;

/// <summary>Stores the user's theme preference for the application UI.</summary>
public sealed record ThemePreferences(ThemePreferenceMode Mode)
{
    public static ThemePreferences Default { get; } = new(ThemePreferenceMode.FollowSystem);
}
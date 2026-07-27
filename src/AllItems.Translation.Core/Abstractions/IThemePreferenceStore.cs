using AllItems.Translation.Core.Study;

namespace AllItems.Translation.Core.Abstractions;

/// <summary>Loads and saves the user's app theme preference.</summary>
public interface IThemePreferenceStore
{
    ThemePreferences Load();

    void Save(ThemePreferences preferences);
}
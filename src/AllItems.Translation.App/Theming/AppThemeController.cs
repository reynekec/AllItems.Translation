using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Study;
using Wpf.Ui.Appearance;

namespace AllItems.Translation.App.Theming;

public sealed class AppThemeController : IAppThemeController
{
    private readonly IThemePreferenceStore _themePreferenceStore;

    public AppThemeController(IThemePreferenceStore themePreferenceStore)
    {
        _themePreferenceStore = themePreferenceStore;
    }

    public ThemePreferenceMode CurrentMode { get; private set; } = ThemePreferenceMode.FollowSystem;

    public void Initialize()
    {
        var preferences = _themePreferenceStore.Load();
        CurrentMode = preferences.Mode;
        Apply(CurrentMode);
    }

    public void SetMode(ThemePreferenceMode mode)
    {
        if (CurrentMode == mode)
        {
            return;
        }

        CurrentMode = mode;
        Apply(mode);
        _themePreferenceStore.Save(new ThemePreferences(mode));
    }

    private static void Apply(ThemePreferenceMode mode)
    {
        switch (mode)
        {
            case ThemePreferenceMode.Dark:
                ApplicationThemeManager.Apply(ApplicationTheme.Dark);
                break;
            case ThemePreferenceMode.Light:
                ApplicationThemeManager.Apply(ApplicationTheme.Light);
                break;
            default:
                ApplicationThemeManager.ApplySystemTheme();
                break;
        }
    }
}
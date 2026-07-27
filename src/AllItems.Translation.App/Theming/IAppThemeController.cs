using AllItems.Translation.Core.Study;

namespace AllItems.Translation.App.Theming;

public interface IAppThemeController
{
    ThemePreferenceMode CurrentMode { get; }

    void Initialize();

    void SetMode(ThemePreferenceMode mode);
}
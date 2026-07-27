using AllItems.Translation.App.Theming;
using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Study;

namespace AllItems.Translation.Tests.Study;

public sealed class AppThemeControllerTests
{
    [Fact]
    public void Initialize_LoadsPersistedThemeMode()
    {
        var store = new InMemoryThemePreferenceStore(new ThemePreferences(ThemePreferenceMode.Light));
        var controller = new AppThemeController(store);

        controller.Initialize();

        Assert.Equal(ThemePreferenceMode.Light, controller.CurrentMode);
    }

    [Fact]
    public void SetMode_PersistsNewMode()
    {
        var store = new InMemoryThemePreferenceStore(ThemePreferences.Default);
        var controller = new AppThemeController(store);
        controller.Initialize();

        controller.SetMode(ThemePreferenceMode.Dark);

        Assert.Equal(ThemePreferenceMode.Dark, controller.CurrentMode);
        Assert.Equal(ThemePreferenceMode.Dark, store.Saved?.Mode);
    }

    private sealed class InMemoryThemePreferenceStore : IThemePreferenceStore
    {
        private ThemePreferences _current;

        public InMemoryThemePreferenceStore(ThemePreferences initial)
        {
            _current = initial;
        }

        public ThemePreferences? Saved { get; private set; }

        public ThemePreferences Load() => _current;

        public void Save(ThemePreferences preferences)
        {
            Saved = preferences;
            _current = preferences;
        }
    }
}
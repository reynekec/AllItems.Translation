using AllItems.Translation.Core.Study;
using AllItems.Translation.Infrastructure;
using AllItems.Translation.Infrastructure.Settings;

namespace AllItems.Translation.Tests.Persistence;

public sealed class FileThemePreferenceStoreTests : IDisposable
{
    private readonly string _originalLocalAppData;
    private readonly string _tempLocalAppData;

    public FileThemePreferenceStoreTests()
    {
        _originalLocalAppData = Environment.GetEnvironmentVariable("LOCALAPPDATA") ?? string.Empty;
        _tempLocalAppData = Path.Combine(Path.GetTempPath(), $"allitems-theme-tests-{Guid.NewGuid():N}");
        Directory.CreateDirectory(_tempLocalAppData);
        Environment.SetEnvironmentVariable("LOCALAPPDATA", _tempLocalAppData);
    }

    public void Dispose()
    {
        Environment.SetEnvironmentVariable("LOCALAPPDATA", string.IsNullOrWhiteSpace(_originalLocalAppData) ? null : _originalLocalAppData);

        if (Directory.Exists(_tempLocalAppData))
        {
            Directory.Delete(_tempLocalAppData, recursive: true);
        }
    }

    [Fact]
    public void Load_NoFile_ReturnsDefault()
    {
        var store = new FileThemePreferenceStore();

        var preferences = store.Load();

        Assert.Equal(ThemePreferenceMode.FollowSystem, preferences.Mode);
    }

    [Fact]
    public void SaveThenLoad_RoundTripsThemeMode()
    {
        var store = new FileThemePreferenceStore();

        store.Save(new ThemePreferences(ThemePreferenceMode.Dark));

        var loaded = store.Load();

        Assert.Equal(ThemePreferenceMode.Dark, loaded.Mode);
        Assert.True(File.Exists(AppPaths.ThemePreferencesFilePath));
    }

    [Fact]
    public void Load_InvalidJson_ReturnsDefault()
    {
        AppPaths.EnsureDirectoriesExist();
        File.WriteAllText(AppPaths.ThemePreferencesFilePath, "{ this is invalid json }");
        var store = new FileThemePreferenceStore();

        var loaded = store.Load();

        Assert.Equal(ThemePreferenceMode.FollowSystem, loaded.Mode);
    }
}
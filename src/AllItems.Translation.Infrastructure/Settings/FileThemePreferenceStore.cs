using System.Text.Json;
using System.Text.Json.Serialization;
using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Study;

namespace AllItems.Translation.Infrastructure.Settings;

public sealed class FileThemePreferenceStore : IThemePreferenceStore
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public ThemePreferences Load()
    {
        try
        {
            if (!File.Exists(AppPaths.ThemePreferencesFilePath))
            {
                return ThemePreferences.Default;
            }

            var json = File.ReadAllText(AppPaths.ThemePreferencesFilePath);
            return JsonSerializer.Deserialize<ThemePreferences>(json, SerializerOptions) ?? ThemePreferences.Default;
        }
        catch
        {
            return ThemePreferences.Default;
        }
    }

    public void Save(ThemePreferences preferences)
    {
        try
        {
            AppPaths.EnsureDirectoriesExist();
            var json = JsonSerializer.Serialize(preferences, SerializerOptions);
            File.WriteAllText(AppPaths.ThemePreferencesFilePath, json);
        }
        catch
        {
            // Best-effort: failing to remember theme preference should not break the UI.
        }
    }
}
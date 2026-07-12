using System.Text.Json;
using System.Text.Json.Serialization;
using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Study;

namespace AllItems.Translation.Infrastructure.Settings;

/// <summary>
/// Persists the study language pair to %LOCALAPPDATA%\AllItems.Translation\settings\study-preferences.json.
/// Remembering the dropdowns is a convenience, so both reading and writing are best-effort:
/// a missing or unreadable file just falls back to <see cref="StudyPreferences.Default"/>, and a
/// failed write is swallowed rather than interrupting the study flow.
/// </summary>
public sealed class FileStudyPreferenceStore : IStudyPreferenceStore
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public StudyPreferences Load()
    {
        try
        {
            if (!File.Exists(AppPaths.StudyPreferencesFilePath))
            {
                return StudyPreferences.Default;
            }

            var json = File.ReadAllText(AppPaths.StudyPreferencesFilePath);
            return JsonSerializer.Deserialize<StudyPreferences>(json, SerializerOptions) ?? StudyPreferences.Default;
        }
        catch
        {
            return StudyPreferences.Default;
        }
    }

    public void Save(StudyPreferences preferences)
    {
        try
        {
            AppPaths.EnsureDirectoriesExist();
            var json = JsonSerializer.Serialize(preferences, SerializerOptions);
            File.WriteAllText(AppPaths.StudyPreferencesFilePath, json);
        }
        catch
        {
            // Best-effort: failing to remember the dropdowns must never break studying.
        }
    }
}

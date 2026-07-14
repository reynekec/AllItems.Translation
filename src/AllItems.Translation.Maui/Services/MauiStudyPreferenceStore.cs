using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Domain;
using AllItems.Translation.Core.Study;

namespace AllItems.Translation.Maui.Services;

/// <summary>
/// Remembers the last-picked study language pair using MAUI <see cref="Preferences"/> (the platform's
/// key-value store), so we don't drag the desktop's file-under-%LOCALAPPDATA% store onto iOS.
/// </summary>
public sealed class MauiStudyPreferenceStore : IStudyPreferenceStore
{
    private const string SourceKey = "study.sourceLanguage";
    private const string TargetKey = "study.targetLanguage";

    public StudyPreferences Load()
    {
        var source = Read(SourceKey, StudyPreferences.Default.SourceLanguage);
        var target = Read(TargetKey, StudyPreferences.Default.TargetLanguage);
        return new StudyPreferences(source, target);
    }

    public void Save(StudyPreferences preferences)
    {
        Preferences.Set(SourceKey, (int)preferences.SourceLanguage);
        Preferences.Set(TargetKey, (int)preferences.TargetLanguage);
    }

    private static Language Read(string key, Language fallback)
    {
        var stored = Preferences.Get(key, (int)fallback);
        return Enum.IsDefined(typeof(Language), stored) ? (Language)stored : fallback;
    }
}

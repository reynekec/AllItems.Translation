using AllItems.Translation.Core.Study;

namespace AllItems.Translation.Core.Abstractions;

/// <summary>
/// Remembers the study setup choices (source/target language) on disk so the flashcard
/// dropdowns come back pre-filled with whatever the user last used.
/// </summary>
public interface IStudyPreferenceStore
{
    /// <summary>Returns the last saved preferences, or <see cref="StudyPreferences.Default"/> if none exist yet.</summary>
    StudyPreferences Load();

    void Save(StudyPreferences preferences);
}

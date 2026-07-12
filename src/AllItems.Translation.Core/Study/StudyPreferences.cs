using AllItems.Translation.Core.Domain;

namespace AllItems.Translation.Core.Study;

/// <summary>The last language pair a user picked on the study setup screen, remembered between sessions.</summary>
public sealed record StudyPreferences(Language SourceLanguage, Language TargetLanguage)
{
    /// <summary>Sensible starting point before the user has ever chosen a pair.</summary>
    public static StudyPreferences Default { get; } = new(Language.German, Language.Afrikaans);
}

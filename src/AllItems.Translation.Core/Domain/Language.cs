namespace AllItems.Translation.Core.Domain;

public enum Language
{
    German,
    Afrikaans,
    English
}

public static class LanguageCodes
{
    public static string ToBcp47(this Language language) => language switch
    {
        Language.German => "de",
        Language.Afrikaans => "af",
        Language.English => "en",
        _ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
    };
}

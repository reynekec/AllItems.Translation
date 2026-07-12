namespace AllItems.Translation.Infrastructure;

/// <summary>Central place for the app's on-disk locations, all outside the repo/install tree.</summary>
public static class AppPaths
{
    private static string DataRoot => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "AllItems.Translation");

    public static string DatabaseFilePath => Path.Combine(DataRoot, "data", "allitems.db");

    public static string CredentialFilePath => Path.Combine(DataRoot, "credentials", "service-account.json");

    public static void EnsureDirectoriesExist()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(DatabaseFilePath)!);
        Directory.CreateDirectory(Path.GetDirectoryName(CredentialFilePath)!);
    }
}

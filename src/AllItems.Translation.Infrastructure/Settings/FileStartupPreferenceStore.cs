using AllItems.Translation.Core.Abstractions;
using Microsoft.Win32;

namespace AllItems.Translation.Infrastructure.Settings;

[System.Runtime.Versioning.SupportedOSPlatform("windows")]
public sealed class FileStartupPreferenceStore : IStartupPreferenceStore
{
    private const string RunRegistryPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
    private const string AppRegistryKeyName = "AllItemsTranslation";
    private bool? _cachedValue;

    public bool IsRunAtStartupEnabled
    {
        get
        {
            _cachedValue ??= ReadFromRegistry();
            return _cachedValue.Value;
        }
    }

    public async Task SetRunAtStartupAsync(bool enabled)
    {
        await Task.Run(() =>
        {
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey(RunRegistryPath, writable: true))
                {
                    if (key is null)
                    {
                        throw new InvalidOperationException($"Could not open registry key: {RunRegistryPath}");
                    }

                    if (enabled)
                    {
                        var executablePath = Environment.ProcessPath ?? throw new InvalidOperationException("Cannot determine application executable path");
                        key.SetValue(AppRegistryKeyName, $"\"{executablePath}\"");
                    }
                    else
                    {
                        key.DeleteValue(AppRegistryKeyName, throwOnMissingValue: false);
                    }
                }

                _cachedValue = enabled;
            }
            catch (Exception ex)
            {
                // Log or handle registry access errors
                System.Diagnostics.Debug.WriteLine($"Failed to set startup preference: {ex.Message}");
            }
        });
    }

    private static bool ReadFromRegistry()
    {
        try
        {
            using (var key = Registry.CurrentUser.OpenSubKey(RunRegistryPath))
            {
                var value = key?.GetValue(AppRegistryKeyName);
                return value is not null;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to read startup preference from registry: {ex.Message}");
            return false;
        }
    }
}

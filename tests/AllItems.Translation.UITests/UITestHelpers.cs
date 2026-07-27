using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Patterns;
using FlaUI.UIA3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using System.Runtime.InteropServices;

namespace AllItems.Translation.UITests;

public class UITestHelpers : IDisposable
{
    private const string UiAutomationModeEnvironmentVariable = "ALLITEMS_TRANSLATION_UI_AUTOMATION";
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(15);
    private static readonly TimeSpan PreflightTimeout = TimeSpan.FromSeconds(5);
    private Application? _app;
    private UIA3Automation? _automation;

    public Application App => _app ?? throw new InvalidOperationException("App not launched");
    public UIA3Automation Automation => _automation ?? throw new InvalidOperationException("Automation not initialized");
    public Window MainWindow => WaitForTopLevelWindow(window => !window.IsOffscreen, DefaultTimeout);

    public Window LaunchToStartWindow()
    {
        LaunchApp();

        var startupWindow = WaitForTopLevelWindow(
            window => IsStartWindow(window) || IsCredentialSetupWindow(window),
            DefaultTimeout);

        if (IsCredentialSetupWindow(startupWindow))
        {
            throw new InvalidOperationException(
                "Startup blocked by CredentialSetupWindow before StartWindow. Credentials must exist before this navigation run can proceed.");
        }

        return startupWindow;
    }

    public void LaunchApp()
    {
        try
        {
            EnsureInteractiveDesktopReady(PreflightTimeout);

            // Try multiple possible paths for the app executable
            var possiblePaths = new[]
            {
                // From test bin directory
                Path.Combine(
                    Environment.CurrentDirectory,
                    "..",
                    "..",
                    "..",
                    "src",
                    "AllItems.Translation.App",
                    "bin",
                    "Debug",
                    "net10.0-windows",
                    "AllItems.Translation.exe"
                ),
                // Absolute path from workspace root
                "c:\\Chris\\Code\\AllItems.Translation-1\\src\\AllItems.Translation.App\\bin\\Debug\\net10.0-windows\\AllItems.Translation.exe",
                // Alternative relative from current directory
                Path.Combine(
                    Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? "",
                    "..",
                    "..",
                    "..",
                    "..",
                    "src",
                    "AllItems.Translation.App",
                    "bin",
                    "Debug",
                    "net10.0-windows",
                    "AllItems.Translation.exe"
                )
            };

            string? appPath = null;
            foreach (var path in possiblePaths)
            {
                var fullPath = Path.GetFullPath(path);
                if (File.Exists(fullPath))
                {
                    appPath = fullPath;
                    break;
                }
            }

            if (string.IsNullOrEmpty(appPath) || !File.Exists(appPath))
            {
                var debugPaths = string.Join("\n  - ", possiblePaths.Select(p => Path.GetFullPath(p)));
                throw new FileNotFoundException($"App executable not found. Searched:\n  - {debugPaths}");
            }

            _automation = new UIA3Automation();

            var startInfo = new ProcessStartInfo(appPath)
            {
                UseShellExecute = false
            };
            startInfo.Environment[UiAutomationModeEnvironmentVariable] = "1";

            _app = Application.Launch(startInfo);

            Thread.Sleep(1000);
        }
        catch (Exception ex)
        {
            Dispose();
            throw new InvalidOperationException("Failed to launch app", ex);
        }
    }

    public void CloseApp()
    {
        if (_app is null)
        {
            return;
        }

        try
        {
            foreach (var window in GetTopLevelWindows())
            {
                try
                {
                    window.Close();
                }
                catch
                {
                    // Best effort; cleanup falls back to process termination below.
                }
            }

            WaitForProcessExit(TimeSpan.FromSeconds(5));

            if (!_app.HasExited)
            {
                using var process = Process.GetProcessById(_app.ProcessId);
                process.Kill(true);
                process.WaitForExit(5000);
            }
        }
        catch (ArgumentException)
        {
            // Process already exited.
        }
        finally
        {
            _app = null;
        }
    }

    public Window WaitForStartWindow() => WaitForTopLevelWindow(IsStartWindow, DefaultTimeout);

    public Window WaitForMainWindow() => WaitForTopLevelWindow(IsMainWindow, DefaultTimeout);

    public Window WaitForStudySessionWindow() => WaitForTopLevelWindow(window => window.Title == "Study", DefaultTimeout);

    public Window WaitForTrainingWindow() => WaitForTopLevelWindow(window => window.Title == "Training", DefaultTimeout);

    public Window WaitForCredentialSetupWindow() =>
        WaitForTopLevelWindow(IsCredentialSetupWindow, DefaultTimeout);

    public AutomationElement FindActionElement(Window window, string text)
    {
        var labelElement = RetryUntil(
            () => window.FindFirstDescendant(cf => cf.ByText(text)),
            DefaultTimeout,
            $"Timed out waiting for action label '{text}'.");

        AutomationElement? candidate = labelElement;
        while (candidate is not null && candidate.ControlType != ControlType.Window)
        {
            if (candidate.Patterns.Invoke.IsSupported || candidate.ControlType == ControlType.Button)
            {
                return candidate;
            }

            candidate = candidate.Parent;
        }

        return labelElement;
    }

    public AutomationElement FindButtonByName(Window window, string name)
    {
        return RetryUntil(
            () => FindButtons(window)
                .FirstOrDefault(button =>
                    string.Equals(button.AutomationId, name, StringComparison.OrdinalIgnoreCase)
                    || string.Equals(button.Name, name, StringComparison.OrdinalIgnoreCase)
                    || string.Equals(button.HelpText, name, StringComparison.OrdinalIgnoreCase))
                ?? window.FindAllDescendants()
                    .FirstOrDefault(element =>
                        element.Patterns.Invoke.IsSupported
                        && (string.Equals(element.AutomationId, name, StringComparison.OrdinalIgnoreCase)
                            || string.Equals(element.Name, name, StringComparison.OrdinalIgnoreCase)
                            || string.Equals(element.HelpText, name, StringComparison.OrdinalIgnoreCase)))
                ?? window.FindAllDescendants(cf => cf.ByControlType(ControlType.TitleBar))
                    .SelectMany(FindButtons)
                    .FirstOrDefault(button =>
                        string.Equals(button.AutomationId, name, StringComparison.OrdinalIgnoreCase)
                        || string.Equals(button.Name, name, StringComparison.OrdinalIgnoreCase)
                        || string.Equals(button.HelpText, name, StringComparison.OrdinalIgnoreCase)),
            DefaultTimeout,
            $"Timed out waiting for button '{name}'.");
    }

    public ToggleButton FindThemeToggle(Window window)
    {
        var toggleElement = RetryUntil(
            () => window.FindFirstDescendant(cf => cf.ByAutomationId("StartWindowThemeToggle")),
            DefaultTimeout,
            "Timed out waiting for Start window theme toggle.");

        var toggle = toggleElement.AsToggleButton();
        if (toggle is null)
        {
            throw new InvalidOperationException("Theme toggle element was found, but it is not a ToggleButton.");
        }

        return toggle;
    }

    public void SetToggleState(ToggleButton toggle, bool isOn)
    {
        EnsureInteractionReady();

        var currentState = toggle.Patterns.Toggle.Pattern.ToggleState;
        var shouldToggle = (isOn && currentState != ToggleState.On) || (!isOn && currentState == ToggleState.On);

        if (shouldToggle)
        {
            toggle.Toggle();
        }
    }

    public void Invoke(AutomationElement element)
    {
        EnsureInteractionReady();

        if (element.Patterns.Invoke.IsSupported)
        {
            element.Patterns.Invoke.Pattern.Invoke();
            return;
        }

        element.Click();
    }

    public void Click(AutomationElement element)
    {
        EnsureInteractionReady();
        element.Click();
    }

    public string[] GetTopLevelWindowTitles()
    {
        try
        {
            return GetTopLevelWindows()
                .Select(window => window.Title)
                .ToArray();
        }
        catch
        {
            return Array.Empty<string>();
        }
    }

    public void WaitForWindowToClose(Window window, TimeSpan? timeout = null)
    {
        var title = window.Title;
        var deadline = DateTime.UtcNow + (timeout ?? DefaultTimeout);
        while (DateTime.UtcNow < deadline)
        {
            var matchingWindowExists = GetTopLevelWindows()
                .Any(candidate => string.Equals(candidate.Title, title, StringComparison.Ordinal));

            if (!matchingWindowExists)
            {
                return;
            }

            Thread.Sleep(100);
        }

        throw new TimeoutException($"Timed out waiting for window '{window.Title}' to close.");
    }

    private Window WaitForTopLevelWindow(Func<Window, bool> predicate, TimeSpan timeout)
    {
        return RetryUntil(
            () => GetTopLevelWindows().FirstOrDefault(window =>
            {
                try
                {
                    return predicate(window);
                }
                catch
                {
                    return false;
                }
            }),
            timeout,
            "Timed out waiting for the expected top-level window.");
    }

    private Window[] GetTopLevelWindows()
    {
        return App.GetAllTopLevelWindows(Automation).ToArray();
    }

    private static bool IsStartWindow(Window window)
    {
        return window.Title == "AllItems.Translation"
            && window.FindFirstDescendant(cf => cf.ByText("Translations")) is not null
            && window.FindFirstDescendant(cf => cf.ByText("Flashcards")) is not null;
    }

    private static bool IsMainWindow(Window window)
    {
        return window.Title == "AllItems.Translation"
            && window.FindFirstDescendant(cf => cf.ByText("I'm typing in:")) is not null;
    }

    private static bool IsCredentialSetupWindow(Window window)
    {
        return window.Title == "Connect Google Cloud Translation";
    }

    private static IEnumerable<AutomationElement> FindButtons(AutomationElement element)
    {
        return element.FindAllDescendants(cf => cf.ByControlType(ControlType.Button));
    }

    private void EnsureInteractionReady()
    {
        EnsureInteractiveDesktopReady(PreflightTimeout);

        if (_app is null || _automation is null)
        {
            throw new InvalidOperationException("UI automation readiness preflight failed: app and automation must be initialized before interaction.");
        }

        var topLevelWindows = GetTopLevelWindows();
        if (topLevelWindows.Length == 0)
        {
            throw new InvalidOperationException("UI automation readiness preflight failed: no top-level app windows were found before interaction.");
        }

        var visibleWindow = topLevelWindows.FirstOrDefault(window => !window.IsOffscreen);
        if (visibleWindow is not null)
        {
            try
            {
                visibleWindow.Focus();
            }
            catch
            {
                // Focus is best-effort; interactive desktop checks above remain authoritative.
            }
        }
    }

    private static void EnsureInteractiveDesktopReady(TimeSpan timeout)
    {
        if (!OperatingSystem.IsWindows())
        {
            throw new InvalidOperationException("UI automation readiness preflight failed: WPF automation requires Windows.");
        }

        if (!Environment.UserInteractive)
        {
            throw new InvalidOperationException("UI automation readiness preflight failed: current session is not interactive.");
        }

        var currentSessionId = Process.GetCurrentProcess().SessionId;
        var deadline = DateTime.UtcNow + timeout;

        while (DateTime.UtcNow < deadline)
        {
            var hasExplorerInCurrentSession = Process
                .GetProcessesByName("explorer")
                .Any(process =>
                {
                    try
                    {
                        return !process.HasExited && process.SessionId == currentSessionId;
                    }
                    catch
                    {
                        return false;
                    }
                });

            if (hasExplorerInCurrentSession && GetForegroundWindow() != IntPtr.Zero)
            {
                return;
            }

            Thread.Sleep(200);
        }

        throw new InvalidOperationException(
            "UI automation readiness preflight failed: interactive desktop foreground/focus was not ready within timeout.");
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    private void WaitForProcessExit(TimeSpan timeout)
    {
        if (_app is null)
        {
            return;
        }

        var deadline = DateTime.UtcNow + timeout;
        while (DateTime.UtcNow < deadline)
        {
            if (_app.HasExited)
            {
                return;
            }

            Thread.Sleep(100);
        }
    }

    private static T RetryUntil<T>(Func<T?> action, TimeSpan timeout, string timeoutMessage) where T : class
    {
        var deadline = DateTime.UtcNow + timeout;
        Exception? lastException = null;

        while (DateTime.UtcNow < deadline)
        {
            try
            {
                var result = action();
                if (result is not null)
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                lastException = ex;
            }

            Thread.Sleep(200);
        }

        throw new TimeoutException(timeoutMessage, lastException);
    }

    public void Dispose()
    {
        CloseApp();
        _automation?.Dispose();
        _automation = null;
    }
}

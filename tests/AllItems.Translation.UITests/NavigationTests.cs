using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using System.Linq;
using System;

namespace AllItems.Translation.UITests;

public class NavigationTests : IDisposable
{
    private readonly UITestHelpers _helpers = new();

    public void Dispose()
    {
        _helpers.Dispose();
    }

    [Fact]
    public void StartWindow_ToTranslations_OpensMainWindow_AndReturnsToStart()
    {
        var startWindow = _helpers.LaunchToStartWindow();

        _helpers.Invoke(_helpers.FindActionElement(startWindow, "Translations"));

        var mainWindow = _helpers.WaitForMainWindow();
        Assert.True(mainWindow.IsOffscreen == false, "MainWindow should be visible");

        mainWindow.Close();
        _helpers.WaitForWindowToClose(mainWindow);

        startWindow = _helpers.WaitForStartWindow();
        Assert.True(startWindow.IsOffscreen == false, "StartWindow should be visible after MainWindow closes");
    }

    [Fact]
    public void StartWindow_ToFlashcards_OpensStudyWindow_AndReturnsToStart()
    {
        var startWindow = _helpers.LaunchToStartWindow();

        _helpers.Invoke(_helpers.FindActionElement(startWindow, "Flashcards"));

        var studyWindow = _helpers.WaitForStudySessionWindow();
        Assert.True(studyWindow.IsOffscreen == false, "StudySessionWindow should be visible");

        studyWindow.Close();
        _helpers.WaitForWindowToClose(studyWindow);

        startWindow = _helpers.WaitForStartWindow();
        Assert.True(startWindow.IsOffscreen == false, "StartWindow should be visible after StudySessionWindow closes");
    }

    [Fact]
    public void StartWindow_ToTraining_OpensTrainingWindow_AndReturnsToStart()
    {
        var startWindow = _helpers.LaunchToStartWindow();

        _helpers.Invoke(_helpers.FindActionElement(startWindow, "Training"));

        var trainingWindow = _helpers.WaitForTrainingWindow();
        Assert.True(trainingWindow.IsOffscreen == false, "TrainingWindow should be visible");

        trainingWindow.Close();
        _helpers.WaitForWindowToClose(trainingWindow);

        startWindow = _helpers.WaitForStartWindow();
        Assert.True(startWindow.IsOffscreen == false, "StartWindow should be visible after TrainingWindow closes");
    }

    [Fact]
    public void StartWindow_Settings_OpensCredentialSetupDialog_AndReturnsToStart()
    {
        var startWindow = _helpers.LaunchToStartWindow();

        _helpers.Invoke(_helpers.FindActionElement(startWindow, "Settings"));

        var windowTitlesAfterClick = _helpers.GetTopLevelWindowTitles();

        Window credentialWindow;
        try
        {
            credentialWindow = _helpers.WaitForCredentialSetupWindow();
        }
        catch (TimeoutException ex)
        {
            throw new Xunit.Sdk.XunitException(
                $"Credential setup window did not appear. Visible top-level windows: {string.Join(", ", windowTitlesAfterClick)}",
                ex);
        }

        Assert.True(credentialWindow.IsOffscreen == false, "CredentialSetupWindow should be visible");

        credentialWindow.Close();

        startWindow = _helpers.WaitForStartWindow();
        Assert.True(startWindow.IsOffscreen == false, "StartWindow should remain visible after dialog closes");
    }

}

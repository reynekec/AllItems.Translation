namespace AllItems.Translation.UITests;

public class SmokeTests : IDisposable
{
    private readonly UITestHelpers _helpers = new();

    public void Dispose()
    {
        _helpers.Dispose();
    }

    [Fact]
    public void AppLaunches_WindowVisible()
    {
        // Arrange & Act
        _helpers.LaunchApp();
        var mainWindow = _helpers.MainWindow;

        // Assert
        Assert.NotNull(mainWindow);
        Assert.True(mainWindow.IsOffscreen == false, "Main window should be visible");
    }

    [Fact]
    public void AppLaunches_WindowHasTitle()
    {
        // Arrange & Act
        _helpers.LaunchApp();
        var mainWindow = _helpers.MainWindow;

        // Assert
        Assert.NotNull(mainWindow.Title);
        Assert.NotEmpty(mainWindow.Title);
    }
}

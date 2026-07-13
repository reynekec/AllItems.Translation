using AllItems.Translation.App.ViewModels.Training;
using System.Windows;
using System.Windows.Input;
using Wpf.Ui.Controls;

namespace AllItems.Translation.App.Views;

public partial class TrainingWindow : FluentWindow
{
    private const double MouseWheelScrollDelta = 48;
    private const int CaptionButtonCount = 3;

    public TrainingWindow(TrainingViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        SizeChanged += (_, _) => UpdateTitleBarCentering();
        Loaded += (_, _) => UpdateTitleBarCentering();
        TitleBarLeftContentRoot.SizeChanged += (_, _) => UpdateTitleBarCentering();
        TitleBarRightContentRoot.SizeChanged += (_, _) => UpdateTitleBarCentering();
        viewModel.Initialize();
    }

    private void UpdateTitleBarCentering()
    {
        if (TrainingTitleBar is null)
        {
            return;
        }

        var captionButtonsWidth = SystemParameters.CaptionWidth * CaptionButtonCount;
        var leftWidth = TitleBarLeftContentRoot.ActualWidth;
        var rightWidth = TitleBarRightContentRoot.ActualWidth + captionButtonsWidth;
        var largerMargin = Math.Max(leftWidth, rightWidth);
        var availableWidth = Math.Max(0, TrainingTitleBar.ActualWidth - (largerMargin * 2));

        TitleBarCenterContentRoot.Width = availableWidth;
    }

    private void OnWindowPreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (ContentScrollViewer is null)
        {
            return;
        }

        var newOffset = ContentScrollViewer.VerticalOffset - (e.Delta / 120.0 * MouseWheelScrollDelta);
        ContentScrollViewer.ScrollToVerticalOffset(newOffset);
        e.Handled = true;
    }

    private void OnWindowPreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (ContentScrollViewer is null)
        {
            return;
        }

        switch (e.Key)
        {
            case Key.Down:
                ContentScrollViewer.LineDown();
                e.Handled = true;
                break;
            case Key.Up:
                ContentScrollViewer.LineUp();
                e.Handled = true;
                break;
            case Key.PageDown:
                ContentScrollViewer.PageDown();
                e.Handled = true;
                break;
            case Key.PageUp:
                ContentScrollViewer.PageUp();
                e.Handled = true;
                break;
            case Key.Home:
                ContentScrollViewer.ScrollToHome();
                e.Handled = true;
                break;
            case Key.End:
                ContentScrollViewer.ScrollToEnd();
                e.Handled = true;
                break;
            case Key.Space:
                if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                {
                    ContentScrollViewer.PageUp();
                }
                else
                {
                    ContentScrollViewer.PageDown();
                }

                e.Handled = true;
                break;
        }
    }
}

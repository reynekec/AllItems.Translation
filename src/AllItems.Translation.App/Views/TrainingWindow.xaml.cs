using AllItems.Translation.App.ViewModels.Training;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;
using System.Windows.Media;
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
        viewModel.PropertyChanged += OnViewModelPropertyChanged;
        viewModel.Initialize();
    }

    private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (sender is not TrainingViewModel viewModel)
        {
            return;
        }

        if (e.PropertyName == nameof(TrainingViewModel.CurrentExercise) ||
            e.PropertyName == nameof(TrainingViewModel.Screen))
        {
            TryFocusFirstExerciseTextBox(viewModel);
        }
    }

    private void TryFocusFirstExerciseTextBox(TrainingViewModel viewModel)
    {
        if (viewModel.Screen != TrainingScreen.Exercise || ExerciseContentHost is null)
        {
            return;
        }

        // Defer to ensure the exercise template has been instantiated.
        Dispatcher.BeginInvoke(() =>
        {
            var textBox = FindFirstChild<System.Windows.Controls.TextBox>(ExerciseContentHost);
            if (textBox is null)
            {
                return;
            }

            textBox.Focus();
            textBox.SelectAll();
            Keyboard.Focus(textBox);
        }, System.Windows.Threading.DispatcherPriority.Loaded);
    }

    private static T? FindFirstChild<T>(DependencyObject parent) where T : DependencyObject
    {
        var childCount = VisualTreeHelper.GetChildrenCount(parent);
        for (var i = 0; i < childCount; i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            if (child is T match)
            {
                return match;
            }

            var nestedMatch = FindFirstChild<T>(child);
            if (nestedMatch is not null)
            {
                return nestedMatch;
            }
        }

        return null;
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
        if (HandleExerciseEnterKey(e))
        {
            return;
        }

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

    private bool HandleExerciseEnterKey(System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
        {
            return false;
        }

        if (Keyboard.FocusedElement is not System.Windows.Controls.TextBox)
        {
            return false;
        }

        if (DataContext is not TrainingViewModel viewModel)
        {
            return false;
        }

        if (viewModel.Screen != TrainingScreen.Exercise ||
            viewModel.IsUnitComplete ||
            viewModel.CurrentExercise is null ||
            !viewModel.CurrentExercise.ShowCheckAnswerButton ||
            !viewModel.CheckAnswerCommand.CanExecute(null))
        {
            return false;
        }

        viewModel.CheckAnswerCommand.Execute(null);
        e.Handled = true;
        return true;
    }
}

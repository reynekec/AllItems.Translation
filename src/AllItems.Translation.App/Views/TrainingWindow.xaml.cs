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

    public TrainingWindow(TrainingViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
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

        // When focus is in a text box, let the standard editing keys (arrows, Home, End,
        // Page Up/Down) work normally inside the box rather than driving the scroll viewer.
        var isTextBoxFocused = Keyboard.FocusedElement is System.Windows.Controls.TextBox;

        switch (e.Key)
        {
            case Key.Down:
                if (isTextBoxFocused) break;
                ContentScrollViewer.LineDown();
                e.Handled = true;
                break;
            case Key.Up:
                if (isTextBoxFocused) break;
                ContentScrollViewer.LineUp();
                e.Handled = true;
                break;
            case Key.PageDown:
                if (isTextBoxFocused) break;
                ContentScrollViewer.PageDown();
                e.Handled = true;
                break;
            case Key.PageUp:
                if (isTextBoxFocused) break;
                ContentScrollViewer.PageUp();
                e.Handled = true;
                break;
            case Key.Home:
                if (isTextBoxFocused) break;
                ContentScrollViewer.ScrollToHome();
                e.Handled = true;
                break;
            case Key.End:
                if (isTextBoxFocused) break;
                ContentScrollViewer.ScrollToEnd();
                e.Handled = true;
                break;
            case Key.Space:
                if (isTextBoxFocused) break;

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
            viewModel.CurrentExercise is null)
        {
            return false;
        }

        if (viewModel.CurrentExercise.ShowIncorrectFeedback &&
            viewModel.TryAgainCommand.CanExecute(null))
        {
            viewModel.TryAgainCommand.Execute(null);
            e.Handled = true;
            return true;
        }

        if (viewModel.CurrentExercise.ShowCheckAnswerButton &&
            viewModel.CheckAnswerCommand.CanExecute(null))
        {
            viewModel.CheckAnswerCommand.Execute(null);
            e.Handled = true;
            return true;
        }

        return false;
    }
}

using AllItems.Translation.App.ViewModels;
using Wpf.Ui.Controls;

namespace AllItems.Translation.App.Views;

public partial class StudySessionWindow : FluentWindow
{
    public StudySessionWindow(StudySessionViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}

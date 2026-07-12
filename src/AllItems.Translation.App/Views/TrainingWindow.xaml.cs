using AllItems.Translation.App.ViewModels;
using Wpf.Ui.Controls;

namespace AllItems.Translation.App.Views;

public partial class TrainingWindow : FluentWindow
{
    public TrainingWindow(TrainingViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}

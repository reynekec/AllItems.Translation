using AllItems.Translation.App.ViewModels;
using Wpf.Ui.Controls;

namespace AllItems.Translation.App.Views;

public partial class DictionaryManagerWindow : FluentWindow
{
    public DictionaryManagerWindow(DictionaryManagerViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}

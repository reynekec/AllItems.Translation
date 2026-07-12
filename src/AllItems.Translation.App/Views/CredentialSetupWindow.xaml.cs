using AllItems.Translation.App.ViewModels;
using Wpf.Ui.Controls;

namespace AllItems.Translation.App.Views;

public partial class CredentialSetupWindow : FluentWindow
{
    public CredentialSetupWindow(CredentialSetupViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        viewModel.SavedSuccessfully += () => DialogResult = true;
    }
}

using AllItems.Translation.Maui.ViewModels;

namespace AllItems.Translation.Maui.Views;

public partial class ImportPage : ContentPage
{
    public ImportPage(ImportPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

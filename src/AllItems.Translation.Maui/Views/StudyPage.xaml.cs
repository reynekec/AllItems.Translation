using AllItems.Translation.Maui.ViewModels;

namespace AllItems.Translation.Maui.Views;

public partial class StudyPage : ContentPage
{
    private readonly StudyPageViewModel _viewModel;

    public StudyPage(StudyPageViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Refresh the available-card count each time the tab is shown (it changes after an import).
        await _viewModel.InitializeAsync();
    }
}

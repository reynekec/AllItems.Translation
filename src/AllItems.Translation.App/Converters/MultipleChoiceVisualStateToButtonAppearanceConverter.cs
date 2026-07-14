using System.Globalization;
using AllItems.Translation.App.ViewModels.Training;
using System.Windows.Data;
using Wpf.Ui.Controls;

namespace AllItems.Translation.App.Converters;

/// <summary>Maps multiple-choice option result state to button appearance colors.</summary>
public sealed class MultipleChoiceVisualStateToButtonAppearanceConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not MultipleChoiceOptionVisualState state)
        {
            return ControlAppearance.Secondary;
        }

        return state switch
        {
            MultipleChoiceOptionVisualState.Selected => ControlAppearance.Primary,
            MultipleChoiceOptionVisualState.Correct => ControlAppearance.Success,
            MultipleChoiceOptionVisualState.Incorrect => ControlAppearance.Danger,
            _ => ControlAppearance.Secondary
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

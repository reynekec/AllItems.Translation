using System.Globalization;
using System.Windows.Data;
using Wpf.Ui.Controls;

namespace AllItems.Translation.App.Converters;

/// <summary>Primary appearance when selected, Secondary otherwise - used for multiple-choice option buttons.</summary>
public sealed class BoolToButtonAppearanceConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture) =>
        value is true ? ControlAppearance.Primary : ControlAppearance.Secondary;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

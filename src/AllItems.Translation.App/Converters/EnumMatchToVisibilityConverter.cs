using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AllItems.Translation.App.Converters;

/// <summary>Visible when the bound enum's string form equals the converter parameter.</summary>
public sealed class EnumMatchToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture) =>
        value is not null && string.Equals(value.ToString(), parameter?.ToString(), StringComparison.Ordinal)
            ? Visibility.Visible
            : Visibility.Collapsed;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

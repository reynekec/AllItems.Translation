using System.Globalization;
using System.Windows.Data;

namespace AllItems.Translation.App.Converters;

/// <summary>False when the bound string is null/empty, true otherwise.</summary>
public sealed class NullOrEmptyToBoolConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture) =>
        !string.IsNullOrEmpty(value as string);

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

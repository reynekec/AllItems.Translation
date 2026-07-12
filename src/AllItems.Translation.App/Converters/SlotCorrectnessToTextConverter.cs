using System.Globalization;
using System.Windows.Data;

namespace AllItems.Translation.App.Converters;

/// <summary>A quiet checkmark for a correct slot, nothing for wrong/unanswered - no red X anywhere.</summary>
public sealed class SlotCorrectnessToTextConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture) =>
        value is true ? "✓" : string.Empty;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

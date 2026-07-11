using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace CoperativeTelouet.UI.Converters;

public sealed class DecimalDhConverter : IValueConverter
{
    public static readonly DecimalDhConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is decimal d && d != 0)
            return d.ToString("N2", culture) + " DH";
        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

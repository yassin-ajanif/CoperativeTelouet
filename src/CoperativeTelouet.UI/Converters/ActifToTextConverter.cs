using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace CoperativeTelouet.UI.Converters;

public sealed class ActifToTextConverter : IValueConverter
{
    public static readonly ActifToTextConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is true ? "Actif" : "Inactif";

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        BindingOperations.DoNothing;
}

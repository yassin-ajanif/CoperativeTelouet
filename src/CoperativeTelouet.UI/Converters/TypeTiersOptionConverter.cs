using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace CoperativeTelouet.UI.Converters;

public sealed class TypeTiersOptionConverter : IValueConverter
{
    public static readonly TypeTiersOptionConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value?.ToString() switch
        {
            "LesDeux" => "Client & Fournisseur",
            "Client" => "Client",
            "Fournisseur" => "Fournisseur",
            _ => value?.ToString(),
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        BindingOperations.DoNothing;
}

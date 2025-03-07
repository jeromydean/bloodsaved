using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace BloodSaved.Converters
{
  public class StringIsNotNullOrEmptyToBooleanConverter : IValueConverter
  {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
      return !(value == null || string.IsNullOrEmpty(value.ToString()));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}

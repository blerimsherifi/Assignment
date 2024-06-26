﻿using System.Globalization;
using System.Windows.Data;

namespace Assignment.UI;

public class NullableToBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not null)
        {
            return true;
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not null)
        {
            return true;
        }

        return false;
    }
}

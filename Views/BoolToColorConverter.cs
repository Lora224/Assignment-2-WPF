﻿using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Assignment_2_WPF.Views { 
public class BoolToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isGood)
        {
            return isGood ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
        }
        return new SolidColorBrush(Colors.Black);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
}
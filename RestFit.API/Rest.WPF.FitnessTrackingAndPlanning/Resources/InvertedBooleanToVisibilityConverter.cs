using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Rest.WPF.FitnessTrackingAndPlanning.Resources;

[Localizability(LocalizationCategory.NeverLocalize)]
public sealed class InvertedBooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool bValue = false;
        if (value is bool boolValue)
        {
            bValue = boolValue;
        }
        else if (value is bool?)
        {
            bool? tmp = (bool?)value;
            bValue = tmp.Value;
        }

        return bValue ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visibility)
        {
            return visibility == Visibility.Collapsed;
        }

        return false;
    }
}
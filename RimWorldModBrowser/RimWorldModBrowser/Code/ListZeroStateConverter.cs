using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RimWorldModBrowser.Code
{
    public class ListZeroStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) 
                return Visibility.Collapsed;

            if (value is int numEntries)
                return numEntries > 0 ? Visibility.Visible : Visibility.Collapsed;

            if (value is ModConcept mod)
                return Visibility.Visible;
            
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Don't need to implement this
            return null;
        }
    }
}
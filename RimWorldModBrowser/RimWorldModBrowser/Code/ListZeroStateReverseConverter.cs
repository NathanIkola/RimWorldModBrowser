using System;
using System.Globalization;
using System.Windows;

namespace RimWorldModBrowser.Code
{
    public class ListZeroStateReverseConverter : ListZeroStateConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)base.Convert(value, targetType, parameter, culture);
            return visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
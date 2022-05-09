using System;
using System.Globalization;
using System.Windows;

namespace RimWorldModBrowser.Code.Converters
{
    /// <summary>
    /// A class that returns the opposite output of <see cref="ListZeroStateConverter"/>
    /// </summary>
    public class ListZeroStateReverseConverter : ListZeroStateConverter
    {
        /// <summary>
        /// Gets the value from <see cref="ListZeroStateConverter"/> and returns the opposite
        /// </summary>
        /// <param name="value">See <see cref="ListZeroStateConverter.Convert"/></param>
        /// <param name="targetType">See <see cref="ListZeroStateConverter.Convert"/></param>
        /// <param name="parameter">See <see cref="ListZeroStateConverter.Convert"/></param>
        /// <param name="culture">See <see cref="ListZeroStateConverter.Convert"/></param>
        /// <returns>The opposite of what <see cref="ListZeroStateConverter.Convert"/> returns</returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)base.Convert(value, targetType, parameter, culture);
            return visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
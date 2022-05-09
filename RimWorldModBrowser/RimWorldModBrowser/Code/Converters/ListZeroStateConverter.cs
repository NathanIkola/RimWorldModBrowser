using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RimWorldModBrowser.Code.Converters
{
    /// <summary>
    /// A class that helps conditionally render controls
    /// </summary>
    public class ListZeroStateConverter : IValueConverter
    {
        /// <summary>
        /// Checks the input object to conditionally determine if a control should render
        /// </summary>
        /// <remarks>It's named poorly, this will return <see cref="Visibility.Collapsed"/> if the ZeroState should show</remarks>
        /// <param name="value"></param>
        /// <param name="targetType">Unused</param>
        /// <param name="parameter">Unused</param>
        /// <param name="culture">Unused</param>
        /// <returns>A <see cref="Visibility"/> value depending on the state of <paramref name="value"/></returns>
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) 
                return Visibility.Collapsed;

            if (value is int numEntries)
                return numEntries > 0 ? Visibility.Visible : Visibility.Collapsed;

            if (value is ModConcept)
                return Visibility.Visible;
            
            return Visibility.Collapsed;
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Don't need to implement this
            return null;
        }
    }
}
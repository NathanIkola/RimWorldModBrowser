using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RimWorldModBrowser.Code.Converters
{
    /// <summary>
    /// Used to convert paths to images in a way that doesn't break WPF bindings on failure
    /// </summary>
    public class StringToPngConverter : IValueConverter
    {
        /// <summary>
        /// Attempts to turn a string path into its image
        /// </summary>
        /// <param name="value">The <see cref="string"/> representing the path to the image</param>
        /// <param name="targetType">Unused</param>
        /// <param name="parameter">Unused</param>
        /// <param name="culture">Unused</param>
        /// <returns>The corresponding <see cref="Image"/> or <see langword="null"/> if conversion failed</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource imageSource = null;
            if (value is string path)
                try { imageSource = new BitmapImage(new Uri(path)); }
                catch (Exception) { }
            return imageSource;
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
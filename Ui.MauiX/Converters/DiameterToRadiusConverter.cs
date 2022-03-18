using System;
using System.Globalization;
using Xamarin.Forms;

namespace Ui.MauiX.Converters
{
    /// <summary>
    /// Converts a diameter into a radius.
    /// </summary>
    public class DiameterToRadiusConverter : IValueConverter
    {
        #region Fields
        private static DiameterToRadiusConverter _instance;
        #endregion

        #region Properties
        /// <summary>
        /// A static <see cref="DiameterToRadiusConverter"/> instance.
        /// </summary>
        public static DiameterToRadiusConverter Instance 
        { 
            get
            {
                if (_instance == null)
                {
                    _instance = new DiameterToRadiusConverter();
                }
                return _instance;
            } 
        }
        #endregion

        #region Methods
        /// <summary>
        /// Converts a diameter into a radius.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value / 2;
        }

        /// <summary>
        /// Converts a radius back into a diameter.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value * 2;
        }
        #endregion
    }
}

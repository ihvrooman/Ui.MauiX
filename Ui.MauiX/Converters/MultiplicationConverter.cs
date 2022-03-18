using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.Converters
{
    public class MultiplicationConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return null;
            }

            if (double.TryParse(value.ToString(), out double valueDouble) && double.TryParse(parameter.ToString(), out double multiplicationFactor))
            {
                return valueDouble * multiplicationFactor;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return null;
            }

            if (double.TryParse(value.ToString(), out double valueDouble) && double.TryParse(parameter.ToString(), out double multiplicationFactor))
            {
                return valueDouble / multiplicationFactor;
            }

            return value;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}

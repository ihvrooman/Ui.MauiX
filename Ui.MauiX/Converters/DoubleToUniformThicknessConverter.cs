using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.Converters
{
    public class DoubleToUniformThicknessConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null && parameter.ToString().ToLower() == "invert")
            {
                return ConvertBack(value, targetType, null, culture);
            }

            if (double.TryParse(value.ToString(), out double uniformValue))
            {
                return new Thickness(uniformValue);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null && parameter.ToString().ToLower() == "invert")
            {
                return Convert(value, targetType, null, culture);
            }

            if (value is Thickness t)
            {
                return t.Left;
            }

            return null;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}

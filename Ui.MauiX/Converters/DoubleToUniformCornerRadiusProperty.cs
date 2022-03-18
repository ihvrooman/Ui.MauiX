using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.Converters
{
    public class DoubleToUniformCornerRadiusProperty : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null && parameter.ToString().ToLower() == "invert")
            {
                return ConvertBack(value, targetType, null, culture);
            }
            
            if (double.TryParse(value.ToString(), out double uniformValue))
            {
                return new CornerRadius(uniformValue);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null && parameter.ToString().ToLower() == "invert")
            {
                return Convert(value, targetType, null, culture);
            }

            if (value is CornerRadius c)
            {
                return c.TopLeft;
            }

            return null;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}

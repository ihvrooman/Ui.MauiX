using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.Converters
{
    public class DialogPaddingToMarginConverter : IValueConverter , IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var padding = (Thickness)value;
            if (parameter.ToString().ToLower() == "top")
            {
                return new Thickness(padding.Left, padding.Top, padding.Right, 0);
            }
            else if (parameter.ToString().ToLower() == "middlemargin")
            {
                return new Thickness(padding.Left, 15, 0, 15);
            }
            else if (parameter.ToString().ToLower() == "middlepadding")
            {
                return new Thickness(0, 0, padding.Right, 0);
            }
            else
            {
                return new Thickness(padding.Left, 0, padding.Right, padding.Bottom);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}

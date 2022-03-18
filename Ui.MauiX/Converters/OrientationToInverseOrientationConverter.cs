using Ui.Enums;
using Ui.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.Converters
{
    public class OrientationToInverseOrientationConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var orientation = (Orientation)value;
            switch (orientation)
            {
                case Orientation.Horizontal:
                    return Orientation.Vertical;
                case Orientation.Vertical:
                    return Orientation.Horizontal;
                default:
                    return Orientation.Vertical;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var orientation = (Orientation)value;
            switch (orientation)
            {
                case Orientation.Horizontal:
                    return Orientation.Vertical;
                case Orientation.Vertical:
                    return Orientation.Horizontal;
                default:
                    return Orientation.Vertical;
            }
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}

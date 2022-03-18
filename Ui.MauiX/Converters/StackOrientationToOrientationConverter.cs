using System;
using Xamarin.Forms;
using System.Globalization;
using Xamarin.Forms.Xaml;
using Ui.Enums;

namespace Ui.MauiX.Converters
{
    /// <summary>
    /// Takes a <see cref="StackOrientation"/>, inverts it, and then converts it to an <see cref="Orientation"/>
    /// </summary>
    public class StackOrientationToOrientationConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isInverted = parameter != null && parameter.ToString().ToLower() == "invert";
            var stackOrientation = (StackOrientation)value;
            switch (stackOrientation)
            {
                case StackOrientation.Vertical:
                    return isInverted ? Orientation.Horizontal : Orientation.Vertical;
                case StackOrientation.Horizontal:
                    return isInverted ? Orientation.Vertical : Orientation.Horizontal;
                default:
                    return Orientation.Vertical;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isInverted = parameter != null && parameter.ToString().ToLower() == "invert";
            var orientation = (Orientation)value;
            switch (orientation)
            {
                case Orientation.Vertical:
                    return isInverted ? StackOrientation.Horizontal : StackOrientation.Vertical;
                case Orientation.Horizontal:
                    return isInverted ? StackOrientation.Vertical : StackOrientation.Horizontal;
                default:
                    return StackOrientation.Vertical;
            }
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}

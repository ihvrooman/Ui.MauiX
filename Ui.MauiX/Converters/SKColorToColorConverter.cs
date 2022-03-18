using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.Converters
{
    public class SKColorToColorConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string s && s.ToLower() == "invert")
            {
                return ConvertBack(value, targetType, null, culture);
            }

            var skColor = (SKColor)value;
            return Color.FromRgb(skColor.Red, skColor.Green, skColor.Blue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string s && s.ToLower() == "invert")
            {
                return Convert(value, targetType, null, culture);
            }

            var color = (Color)value;
            return SKColor.Parse(color.ToHex());
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}

using Ui.Enums;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.Converters
{
    public class BoolToToggleStateConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool)value;
            return boolValue ? ToggleState.Toggled : ToggleState.Untoggled;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var toggleState = (ToggleState)value;
            return toggleState == ToggleState.Toggled;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}

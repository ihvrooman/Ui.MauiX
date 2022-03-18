using Ui.Enums;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.Converters
{
    public class ToggleStateToIsToggledConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var toggleState = (ToggleState)value;
            var invert = parameter != null && parameter.ToString().ToLower() == "invert";
            switch (toggleState)
            {
                case ToggleState.Indeterminate:
                    return false;
                case ToggleState.Toggled:
                    return !invert;
                case ToggleState.Untoggled:
                    return invert;
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isToggled = (bool)value;

            if (isToggled)
            {
                return ToggleState.Toggled;
            }

            return ToggleState.Untoggled;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}

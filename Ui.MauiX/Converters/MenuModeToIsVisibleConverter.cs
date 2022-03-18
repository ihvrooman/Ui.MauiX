using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Ui.Enums;
using Ui.MauiX.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.Converters
{
    /// <summary>
    /// Converts <see cref="MenuMode"/> to a bool representing visibility.
    /// </summary>
    public class MenuModeToIsVisibleConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var menuMode = (MenuMode)value;
            var menuModeWhenIsVisible = (MenuMode)parameter;
            return menuMode == menuModeWhenIsVisible;
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

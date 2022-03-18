using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Ui.MauiX.CustomControls;
using Xamarin.Forms.Xaml;
using System.Globalization;
using System.Linq;
using Ui.MauiX.Models;

namespace Ui.MauiX.Converters
{
    /// <summary>
    /// Determines whether or not an element is visible by whether or not a given view is a <see cref="CustomContentView"/> with <see cref="CustomToolbarItem"/>s of the given <see cref="ToolbarItemOrder"/>.
    /// </summary>
    public class ViewToIsVisibleConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CustomContentView c)
            {
                var order = (ToolbarItemOrder)parameter;
                return c.CustomToolbarItems != null && c.CustomToolbarItems.Where(t => t.Order == order).Count() > 0;
            }

            return false;
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

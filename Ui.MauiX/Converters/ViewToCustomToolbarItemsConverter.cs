using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Ui.MauiX.CustomControls;
using Ui.MauiX.Models;
using Xamarin.Forms.Xaml;
using System.Globalization;
using System.Linq;

namespace Ui.MauiX.Converters
{
    /// <summary>
    /// Converts a <see cref="View"/> to a collection of <see cref="CustomToolbarItem"/>s.
    /// </summary>
    public class ViewToCustomToolbarItemsConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CustomContentView c)
            {
                var order = (ToolbarItemOrder)parameter;
                return c.CustomToolbarItems.Where(t => t.Order == order).OrderBy(t => t.Priority);
            }

            return null;
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

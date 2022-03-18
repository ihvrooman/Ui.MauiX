using System;
using System.Globalization;
using Ui.MauiX.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.Converters
{
    /// <summary>
    /// Converts the <see cref="CustomExpander.IsExpanded"/> property to the header margin.
    /// </summary>
    public class ExpanderIsExpandedToHeaderMarginConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isExpanded = (bool)value;
            if (isExpanded)
            {
                return new Thickness(1, 1, 1, 0);
            }
            return new Thickness(1);
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

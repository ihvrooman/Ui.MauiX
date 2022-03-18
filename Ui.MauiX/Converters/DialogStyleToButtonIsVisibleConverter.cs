using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ui.MauiX.Enums;

namespace Ui.MauiX.Converters
{
    public class DialogStyleToButtonIsVisibleConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dialogButtonStyle = (DialogButtonStyle)value;
            var buttonType = (DialogResult)parameter;

            switch (dialogButtonStyle)
            {
                case DialogButtonStyle.Affirmative:
                    return buttonType == DialogResult.Affirmative;
                case DialogButtonStyle.AffirmativeAndNegative:
                    return buttonType == DialogResult.Affirmative || buttonType == DialogResult.Negative;
                case DialogButtonStyle.AffirmativeAndNegativeAndSingleAuxiliary:
                    return buttonType == DialogResult.Affirmative || buttonType == DialogResult.Negative || buttonType == DialogResult.FirstAuxiliary;
                default:
                    return true;
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

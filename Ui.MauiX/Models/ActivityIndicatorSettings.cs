using Components.Helpers;
using Ui.MauiX.Resources;
using Xamarin.Forms;

namespace Ui.MauiX.Models
{
    public class ActivityIndicatorSettings : NotifyPropertyChanged
    {
        #region Fields
        private volatile bool _isVisible;
        private Color _color = Color.White;
        private Thickness _margin = new Thickness(100);
        private volatile string _message = string.Empty;
        private int _messageFontSize = 16;
        private Color _backgroundFadeColor = CustomColors.BackgroundFadeColor;
        #endregion

        #region Properties
        public bool IsVisible 
        { 
            get => _isVisible; 
            set 
            {
                _isVisible = value;
                RaisePropertyChanged();
            } 
        }
        public Color Color { get => _color; set { SetProperty(ref _color, value); } }
        public Thickness Margin { get => _margin; set { SetProperty(ref _margin, value); } }
        public string Message { get => _message; set { _message = value; RaisePropertyChanged(); } }
        public int MessageFontSize { get => _messageFontSize; set { SetProperty(ref _messageFontSize, value); } }
        public Color BackgroundFadeColor { get => _backgroundFadeColor; set { SetProperty(ref _backgroundFadeColor, value); } }
        #endregion
    }
}

using Components.Helpers;
using Ui.MauiX.Resources;
using Xamarin.Forms;

namespace Ui.MauiX.Models
{
    public class MenuItemSettings : NotifyPropertyChanged
    {
        #region Fields
        private bool _isTextVisible = true;
        private Color _textColor = CustomColors.TextColor;
        private Color _disabledTextColor = CustomColors.DisabledTextColor;
        private int _fontSize = 14;
        private int _iconSize = 50;
        private bool _isIconVisible = true;
        #endregion

        #region Properties
        public bool IsTextVisible { get => _isTextVisible; set { SetProperty(ref _isTextVisible, value); } }
        public Color TextColor { get => _textColor; set { SetProperty(ref _textColor, value); } }
        public Color DisabledTextColor { get => _disabledTextColor; set { SetProperty(ref _disabledTextColor, value); } }
        public int FontSize { get => _fontSize; set { SetProperty(ref _fontSize, value); } }
        public int IconSize { get => _iconSize; set { SetProperty(ref _iconSize, value); } }
        public bool IsIconVisible { get => _isIconVisible; set { SetProperty(ref _isIconVisible, value); } }
        #endregion
    }
}

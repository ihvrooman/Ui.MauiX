using Components.Helpers;
using Ui.MauiX.Enums;
using Ui.MauiX.Helpers;
using Ui.MauiX.Resources;
using Xamarin.Forms;

namespace Ui.MauiX.Models
{
    public class DialogSettings : NotifyPropertyChanged
    {
        #region Fields
        private volatile bool _isVisible;
        private Color _backgroundColor = Color.White;
        private string _message = string.Empty;
        private int _messageFontsize = 16;
        private string _title = string.Empty;
        private int _titleFontSize = 22;
        private DialogButtonStyle _buttonStyle = DialogButtonStyle.Affirmative;
        private string _affirmativeButtonText = "Yes";
        private string _negativeButtonText = "No";
        private string _firstAuxiliaryButtonText = "Maybe";
        private string _secondAuxiliaryButtonText = "Cancel";
        private Color _backgroundFadeColor = CustomColors.BackgroundFadeColor;
        private volatile DialogResult _result = DialogResult.Null;
        private Thickness _padding = new Thickness(20);
        private double _buttonBorderThickness = 2;
        private Color _lightAccentButtonColor = Color.White;
        private Color _affirmativeButtonColor = CustomColors.Green;
        private Color _negativeButtonColor = CustomColors.Red;
        private Color _firstAuxiliaryButtonColor = CustomColors.Blue;
        private Color _secondAuxiliaryButtonColor = CustomColors.Orange;
        private CornerRadius _cornerRadius = new CornerRadius(15);
        private Color _headerColor = CustomColors.Blue;
        private Color _titleColor = Color.White;
        private string _iconResourceId;
        private double _iconSize = 40;
        private DialogResult _defaultButton = DialogResult.Affirmative;
        #endregion

        #region Properties
        public bool IsVisible { get => _isVisible; set { _isVisible = value; RaisePropertyChanged(); } }

        public Color BackgroundColor { get => _backgroundColor; set { SetProperty(ref _backgroundColor, value); } }

        public string Message { get => _message; set { SetProperty(ref _message, value); } }

        public int MessageFontsize { get => _messageFontsize; set { SetProperty(ref _messageFontsize, value); } }

        public string Title { get => _title; set { SetProperty(ref _title, value); } }

        public int TitleFontSize { get => _titleFontSize; set { SetProperty(ref _titleFontSize, value); } }

        public DialogButtonStyle ButtonStyle { get => _buttonStyle; set { SetProperty(ref _buttonStyle, value); } }

        public string AffirmativeButtonText { get => _affirmativeButtonText; set { SetProperty(ref _affirmativeButtonText, value); } }

        public string NegativeButtonText { get => _negativeButtonText; set { SetProperty(ref _negativeButtonText, value); } }

        public string FirstAuxiliaryButtonText { get => _firstAuxiliaryButtonText; set { SetProperty(ref _firstAuxiliaryButtonText, value); } }

        public string SecondAuxiliaryButtonText { get => _secondAuxiliaryButtonText; set { SetProperty(ref _secondAuxiliaryButtonText, value); } }

        public DialogResult Result { get => _result; set { _result = value; RaisePropertyChanged(); } }

        public Color BackgroundFadeColor { get => _backgroundFadeColor; set { SetProperty(ref _backgroundFadeColor, value); } }

        public Thickness Padding { get => _padding; set { SetProperty(ref _padding, value); } }

        public double ButtonBorderThickness { get => _buttonBorderThickness; set { SetProperty(ref _buttonBorderThickness, value); } }

        public Color LightAccentButtonColor { get => _lightAccentButtonColor; set { SetProperty(ref _lightAccentButtonColor, value); } }

        public Color AffirmativeButtonColor { get => _affirmativeButtonColor; set { SetProperty(ref _affirmativeButtonColor, value); } }


        internal Color DefaultAffirmativeButtonColor { get; set; } = CustomColors.Green;

        public Color NegativeButtonColor { get => _negativeButtonColor; set { SetProperty(ref _negativeButtonColor, value); } }

        public Color FirstAuxiliaryButtonColor { get => _firstAuxiliaryButtonColor; set { SetProperty(ref _firstAuxiliaryButtonColor, value); } }

        public Color SecondAuxiliaryButtonColor { get => _secondAuxiliaryButtonColor; set { SetProperty(ref _secondAuxiliaryButtonColor, value); } }

        public CornerRadius CornerRadius { get => _cornerRadius; set { SetProperty(ref _cornerRadius, value); } }

        public Color HeaderColor { get => _headerColor; set { SetProperty(ref _headerColor, value); } }

        public Color TitleColor { get => _titleColor; set { SetProperty(ref _titleColor, value); } }

        public string IconResourceId { get => _iconResourceId; set { SetProperty(ref _iconResourceId, value); } }

        public double IconSize { get => _iconSize; set { SetProperty(ref _iconSize, value); } }

        public DialogResult DefaultButton { get => _defaultButton; set { SetProperty(ref _defaultButton, value); } }
        #endregion
    }
}

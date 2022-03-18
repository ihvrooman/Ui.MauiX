using Components.Helpers;

namespace Ui.MauiX.Models
{
    public class LoginPopupSettings : NotifyPropertyChanged
    {
        #region Fields
        private double _titleFontSize = 30;
        private ActivityIndicatorSettings _activityIndicatorSettings = new ActivityIndicatorSettings();
        private int _toggleLoginModeIconSize = 50;
        private double _usernamePasswordSectionHeight = 60;
        private double _usernamePasswordSectionHeaderFontSize = 16;
        private double _usernamePasswordFontSize = 12;
        #endregion

        #region Properties
        public double TitleFontSize { get => _titleFontSize; set { SetProperty(ref _titleFontSize, value); } }
        public ActivityIndicatorSettings ActivityIndicatorSettings { get => _activityIndicatorSettings; set { SetProperty(ref _activityIndicatorSettings, value); } }
        public int ToggleLoginModeIconSize { get => _toggleLoginModeIconSize; set { SetProperty(ref _toggleLoginModeIconSize, value); } }
        public double UsernamePasswordSectionHeight { get => _usernamePasswordSectionHeight; set { SetProperty(ref _usernamePasswordSectionHeight, value); } }
        public double UsernamePasswordSectionHeaderFontSize { get => _usernamePasswordSectionHeaderFontSize; set { SetProperty(ref _usernamePasswordSectionHeaderFontSize, value); } }
        public double UsernamePasswordFontSize { get => _usernamePasswordFontSize; set { SetProperty(ref _usernamePasswordFontSize, value); } }
        #endregion
    }
}

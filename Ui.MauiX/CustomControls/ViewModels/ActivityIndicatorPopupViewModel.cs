using Components.Helpers;
using Ui.MauiX.Models;

namespace Ui.MauiX.CustomControls.ViewModels
{
    internal class ActivityIndicatorPopupViewModel : NotifyPropertyChanged
    {
        #region Fields
        private ActivityIndicatorSettings _activityIndicatorSettings = new ActivityIndicatorSettings();
        #endregion

        #region Properties
        public ActivityIndicatorSettings ActivityIndicatorSettings { get => _activityIndicatorSettings; set { SetProperty(ref _activityIndicatorSettings, value); } }
        #endregion
    }
}

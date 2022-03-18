using Components.Helpers;
using Ui.MauiX.Models;

namespace Ui.MauiX.CustomControls.ViewModels
{
    internal class CustomActivityIndicatorViewModel : NotifyPropertyChanged
    {
        #region Fields
        private ActivityIndicatorSettings _activityIndicatorSettings;
        #endregion

        #region Properties
        public ActivityIndicatorSettings ActivityIndicatorSettings { get => _activityIndicatorSettings; set { SetProperty(ref _activityIndicatorSettings, value); } }
        #endregion
    }
}

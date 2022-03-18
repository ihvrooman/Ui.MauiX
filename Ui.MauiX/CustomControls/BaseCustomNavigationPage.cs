using Ui.MauiX.Helpers;
using Ui.MauiX.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ui.MauiX.CustomControls
{
    /// <summary>
    /// A class which can be inherited from that provides basic navigation properties and functionality.
    /// <para>Note: Pages inheriting from this class should have their control template set to the CustomNavigationPageControlTemplate.</para>
    /// </summary>
    public class BaseCustomNavigationPage : ContentPageWithSetPropertyMethod
    {
        #region Fields
        private Command _navigateBackCommand;
        private ActivityIndicatorSettings _activityIndicatorSettings = new ActivityIndicatorSettings();
        #endregion

        #region Properties
        /// <summary>
        /// The <see cref="Command"/> used to navigate back to the previous page.
        /// </summary>
        public Command NavigateBackCommand
        {
            get
            {
                if (_navigateBackCommand == null)
                {
                    _navigateBackCommand = new Command(NavigateBack, CanNavigateBack);
                }
                return _navigateBackCommand;
            }
        }

        /// <summary>
        /// The <see cref="View"/> to be displayed in the header to the right of the back button.
        /// </summary>
        public View SupplementalHeaderView
        {
            get => (View)GetValue(SupplementalHeaderViewProperty);
            set => SetValue(SupplementalHeaderViewProperty, value);
        }

        /// <summary>
        /// The supplemental header view property.
        /// </summary>
        public static readonly BindableProperty SupplementalHeaderViewProperty = BindableProperty.Create(nameof(SupplementalHeaderView), typeof(View), typeof(BaseCustomNavigationPage));

        /// <summary>
        /// Determines whether or not the Back button is enabled.
        /// </summary>
        public bool IsBackButtonEnabled
        {
            get => (bool)GetValue(IsBackButtonEnabledProperty);
            set => SetValue(IsBackButtonEnabledProperty, value);
        }

        /// <summary>
        /// The is back button enabled property.
        /// </summary>
        public static readonly BindableProperty IsBackButtonEnabledProperty = BindableProperty.Create(nameof(IsBackButtonEnabled), typeof(bool), typeof(BaseCustomNavigationPage), propertyChanged: IsBackButtonEnabledProperty_Changed, defaultValue: true);

        /// <summary>
        /// The size of the back button.
        /// </summary>
        public double BackButtonSize
        {
            get => (double)GetValue(BackButtonSizeProperty);
            set => SetValue(BackButtonSizeProperty, value);
        }

        /// <summary>
        /// The back button size property.
        /// </summary>
        public static readonly BindableProperty BackButtonSizeProperty = BindableProperty.Create(nameof(BackButtonSize), typeof(double), typeof(BaseCustomNavigationPage), 50.0);

        public ActivityIndicatorSettings ActivityIndicatorSettings { get => _activityIndicatorSettings; set => SetProperty(ref _activityIndicatorSettings, value); }
        #endregion

        #region Events
        /// <summary>
        /// Raised when the back button is pressed.
        /// </summary>
        public event EventHandler Closed;
        #endregion

        #region Methods
        #region Public
        public async Task NavigateBack()
        {
            await ActivityIndicatorController.ShowActivityIndicatorWhileTaskExecutes(Application.Current.MainPage.CloseModalPopup(this), null, ActivityIndicatorSettings);
            Closed?.Invoke(this, new EventArgs());
        }

        public virtual void Dispose()
        {
            //Do nothing in base implementation.
        }
        #endregion

        #region Private
        private bool CanNavigateBack(object arg)
        {
            return IsBackButtonEnabled;
        }

        private async void NavigateBack(object obj)
        {
            await NavigateBack();
        }

        private static void IsBackButtonEnabledProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var page = (BaseCustomNavigationPage)bindable;
            page.NavigateBackCommand.ChangeCanExecute();
        }
        #endregion
        #endregion
    }
}
using Ui.MauiX.CustomControls.ViewModels;
using Ui.MauiX.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomActivityIndicator : ContentView
    {
        #region Properties
        public ActivityIndicatorSettings ActivityIndicatorSettings
        {
            get => (ActivityIndicatorSettings)GetValue(ActivityIndicatorSettingsProperty);
            set => SetValue(ActivityIndicatorSettingsProperty, value);
        }

        public static readonly BindableProperty ActivityIndicatorSettingsProperty =
  BindableProperty.Create(nameof(ActivityIndicatorSettings), typeof(ActivityIndicatorSettings), typeof(CustomActivityIndicator), new ActivityIndicatorSettings(), propertyChanged: ActivityIndicatorSettings_Changed);

        internal CustomActivityIndicatorViewModel ViewModel { get => (CustomActivityIndicatorViewModel)MainGrid.BindingContext; }
        #endregion

        #region Constructor
        public CustomActivityIndicator()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private static void ActivityIndicatorSettings_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            ((CustomActivityIndicator)bindable).ViewModel.ActivityIndicatorSettings = (ActivityIndicatorSettings)newValue;
        }
        #endregion
    }
}
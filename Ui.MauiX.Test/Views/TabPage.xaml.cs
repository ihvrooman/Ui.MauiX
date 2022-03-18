using Ui.MauiX.CustomControls;
using Ui.MauiX.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.Test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabPage : BaseCustomNavigationPage
    {
        #region Properties
        private TabViewModel ViewModel { get => (TabViewModel)BindingContext; }
        #endregion

        #region Constructor
        public TabPage()
        {
            InitializeComponent();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            TabView.CustomMenuItems.ForEach(c => c.IconResourceId = ViewModel.IconResourceId);
        }
        #endregion

        #region Methods
        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BaseViewModel.IconResourceId))
            {
                TabView.CustomMenuItems.ForEach(c => c.IconResourceId = ViewModel.IconResourceId);
            }
            else if (e.PropertyName == nameof(BaseViewModel.IsEnabled))
            {
                TabView.CustomMenuItems.ForEach(c =>
                {
                    if (c.TargetType == TabView.SelectedTabType)
                    {
                        return;
                    }

                    TabView.SetTabIsEnabledProperty(c.TargetType, ViewModel.IsEnabled);
                });
            }
        }
        #endregion
    }
}
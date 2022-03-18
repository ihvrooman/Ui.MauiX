using Ui.MauiX.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.Test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageControlsView : ContentView
    {
        #region Properties
        private BaseViewModel ViewModel { get => (BaseViewModel)BindingContext; }
        #endregion

        #region Constructor
        public PageControlsView()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void UniformCornerRadiusSetter_ValueChanged(object sender, EventArgs e)
        {
            ViewModel.SetIndividualCornerRadiusProperties();
        }

        private void IndividualCornerRadiusSetter_ValueChanged(object sender, EventArgs e)
        {
            ViewModel.SetCornerRadius();
        }
        #endregion
    }
}
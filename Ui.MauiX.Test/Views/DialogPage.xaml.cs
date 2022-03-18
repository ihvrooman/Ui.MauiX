using Ui.MauiX.CustomControls;
using Ui.MauiX.Helpers;
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
    public partial class DialogPage : BaseCustomNavigationPage
    {
        #region Properties
        private DialogViewModel ViewModel { get => (DialogViewModel)BindingContext; }
        #endregion

        #region Constructor
        public DialogPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private async void ShowDialogPopupButton_Clicked(object sender, EventArgs e)
        {
            var ds = ViewModel.DialogSettings;
            ViewModel.DialogResult = await Application.Current.MainPage.ShowMessageAsync(ds.Title, ds.Message, ds.ButtonStyle);
        }
        #endregion
    }
}
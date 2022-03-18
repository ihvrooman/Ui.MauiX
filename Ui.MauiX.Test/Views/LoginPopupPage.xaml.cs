using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Ui.MauiX.Helpers;
using Xamarin.Forms.Xaml;
using Ui.MauiX.CustomControls;

namespace Ui.MauiX.Test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPopupPage : BaseCustomNavigationPage
    {
        #region Constructor
        public LoginPopupPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private async void ShowLoginPopupButton_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.ShowLoginDialog(async (rfid) =>
            {
                await Task.Delay(1000);
                return true;
            },
            async (username, password) =>
            {
                await Task.Delay(1000);
                return !string.IsNullOrWhiteSpace(password);
            },
            async () =>
            {
                await Task.Delay(1000);
            });
        }
        #endregion
    }
}
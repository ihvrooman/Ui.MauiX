using Ui.MauiX.CustomControls.ViewModels;
using Ui.MauiX.Models;
using Ui.MauiX.Test.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Ui.MauiX.Test.ViewModels
{
    public class HeaderViewModel : BaseHeaderViewModel
    {
        #region Constructor
        public HeaderViewModel()
        {
            Username = "Default User";
            PageTitle = "Test Page";
            MessagingCenter.Subscribe<App, CustomMenuItem>(this, MessagingCenterMessages.MasterDetailPageSelectedMenuItemChanged.ToString(), (a,customMenuItem) => PageTitle = customMenuItem?.Title);
        }
        #endregion
    }
}

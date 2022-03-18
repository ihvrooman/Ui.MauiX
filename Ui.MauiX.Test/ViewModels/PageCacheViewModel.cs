using Ui.MauiX.Test.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ui.MauiX.Test.ViewModels
{
    public class PageCacheViewModel : BaseViewModel
    {
        #region Fields
        private bool _arePagesCached = true;
        #endregion

        #region Properties
        public bool ArePagesCached { get => _arePagesCached; set => SetProperty(ref _arePagesCached, value); }
        #endregion

        #region Constructor
        public PageCacheViewModel()
        {
            SupplementalPageControlTypes.Add(typeof(PageCacheSupplementalPageControls));
        }
        #endregion
    }
}

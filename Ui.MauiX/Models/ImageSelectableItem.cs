using Components.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ui.MauiX.Models
{
    public class ImageSelectableItem : BaseSelectableItem
    {
        #region Fields
        private string _selectedImageResourceId;
        private string _unselectedImageResourceId;
        #endregion

        #region Properties
        public string SelectedImageResourceId { get => _selectedImageResourceId; set { SetProperty(ref _selectedImageResourceId, value); } }

        public string UnselectedImageResourceId { get => _unselectedImageResourceId; set { SetProperty(ref _unselectedImageResourceId, value); } }
        #endregion
    }
}

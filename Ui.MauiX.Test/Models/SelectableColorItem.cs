using Components.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Ui.MauiX.Test.Models
{
    public class SelectableColorItem : BaseSelectableItem
    {
        #region Fields
        private Color _color;
        #endregion

        #region Properties
        public Color Color { get => _color; set => SetProperty(ref _color, value); }
        #endregion

        #region Constructors
        public SelectableColorItem()
        {
            
        }

        public SelectableColorItem(Color color, string name)
        {
            Color = color;
            DisplayText = name;
        }
        #endregion
    }
}

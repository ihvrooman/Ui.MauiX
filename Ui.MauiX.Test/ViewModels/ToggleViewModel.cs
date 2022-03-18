using Ui.MauiX.Resources;
using Ui.MauiX.Test.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Ui.MauiX.Test.ViewModels
{
    public class ToggleViewModel : BaseViewModel
    {
        #region Fields
        private string _toggledText = "On";
        private string _untoggledText = "Off";
        private Color _toggledColor = CustomColors.Blue;
        private Color _untoggledColor = Color.White;
        #endregion

        #region Properties
        public string ToggledText { get => _toggledText; set => SetProperty(ref _toggledText, value); }

        public string UntoggledText { get => _untoggledText; set => SetProperty(ref _untoggledText, value); }

        public Color ToggledColor { get => _toggledColor; set => SetProperty(ref _toggledColor, value); }

        public Color UntoggledColor { get => _untoggledColor; set => SetProperty(ref _untoggledColor, value); }
        #endregion

        #region Constructor
        public ToggleViewModel()
        {
            SupplementalPageControlTypes.Add(typeof(ToggleSupplementalPageControls));
        }
        #endregion
    }
}

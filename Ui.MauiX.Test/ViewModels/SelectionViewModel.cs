using Components.Models;
using Ui.Enums;
using Ui.MauiX.Models;
using Ui.MauiX.Resources;
using Ui.MauiX.Test.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ui.MauiX.Test.ViewModels
{
    public class SelectionViewModel : ToggleViewModel
    {
        #region Fields
        private SelectionSettings _settings = new SelectionSettings();
        private List<ImageSelectableItem> _imageToggleItems = new List<ImageSelectableItem>();
        private List<BaseSelectableItem> _selectableItems = new List<BaseSelectableItem>();
        #endregion

        #region Properties
        public List<ButtonShape> AvailableButtonShapes { get; } = new List<ButtonShape>()
        { 
            ButtonShape.Circular,
            ButtonShape.Square,
            ButtonShape.Rectangular,
            ButtonShape.Pill,
        };

        public SelectionSettings Settings { get => _settings; set => SetProperty(ref _settings, value); }

        public List<ImageSelectableItem> ImageToggleItems { get => _imageToggleItems; set => SetProperty(ref _imageToggleItems, value); }

        public List<BaseSelectableItem> SelectableItems { get => _selectableItems; set => SetProperty(ref _selectableItems, value); }
        #endregion

        #region Constructor
        public SelectionViewModel()
        {
            SupplementalPageControlTypes.Add(typeof(SelectionSupplementalPageControls));

            FontSize = (int)Settings.FontSize;
            BackgroundColor = Settings.BackgroundColor;
            BorderThickness = Settings.ButtonBorderThickness;
            ToggledColor = Settings.SelectedItemColor;
            UntoggledColor = Settings.UnselectedItemColor;

            base.PropertyChanged += BaseViewModel_PropertyChanged;

            Task.Run(() =>
            {
                for (int i = 0; i < 50; i++)
                {
                    ImageToggleItems.Add(new ImageSelectableItem()
                    {
                        DisplayText = i.ToString(),
                        SelectedImageResourceId = "DeviceInfo.png",
                        UnselectedImageResourceId = "MenuTest.png",
                    });
                }
                ImageToggleItems[2].IsEnabled = false;
            });

            Task.Run(() =>
            {
                for (int i = 0; i < 50; i++)
                {
                    SelectableItems.Add(new BaseSelectableItem()
                    {
                        DisplayText = i.ToString(),
                    });
                }
                SelectableItems[3].IsEnabled = false;
            });
        }

        private void BaseViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FontSize))
            {
                Settings.FontSize = FontSize;
            }
            else if (e.PropertyName == nameof(BackgroundColor))
            {
                Settings.BackgroundColor = BackgroundColor;
            }
            else if (e.PropertyName == nameof(BorderThickness))
            {
                Settings.ButtonBorderThickness = BorderThickness;
            }
            else if (e.PropertyName == nameof(ToggledColor))
            {
                Settings.SelectedItemColor = ToggledColor;
            }
            else if (e.PropertyName == nameof(UntoggledColor))
            {
                Settings.UnselectedItemColor = UntoggledColor;
            }
        }
        #endregion
    }
}

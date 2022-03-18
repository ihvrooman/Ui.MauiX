using Ui.Enums;
using Ui.MauiX.Models;
using Ui.MauiX.Test.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ui.MauiX.Test.ViewModels
{
    public class TabViewModel : PageCacheViewModel
    {
        #region Fields
        private MenuSettings _menuSettings = new MenuSettings();
        private Alignment _tabAlignment = Alignment.Left;
        #endregion

        #region Properties
        public List<Alignment> AvailableAlignments { get; } = new List<Alignment>()
        { 
            Alignment.Left,
            Alignment.Top,
            Alignment.Right,
            Alignment.Bottom,
            Alignment.Hidden,
        };

        public MenuSettings MenuSettings { get => _menuSettings; set => SetProperty(ref _menuSettings, value); }

        public Alignment TabAlignment { get => _tabAlignment; set => SetProperty(ref _tabAlignment, value); }
        #endregion

        #region Constructor
        public TabViewModel()
        {
            PropertyChanged += BaseViewModel_PropertyChanged;

            SupplementalPageControlTypes.Add(typeof(TabSupplementalPageControls));
        }
        #endregion

        #region Methods
        private void BaseViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TextColor))
            {
                MenuSettings.MenuItemTextColor = TextColor;
            }
            else if (e.PropertyName == nameof(DisabledTextColor))
            {
                MenuSettings.DisabledMenuItemTextColor = DisabledTextColor;
            }
            else if (e.PropertyName == nameof(BackgroundColor))
            {
                MenuSettings.MenuItemBackgroundColor = BackgroundColor;
            }
            else if (e.PropertyName == nameof(DisabledBackgroundColor))
            {
                MenuSettings.DisabledMenuItemBackgroundColor = DisabledBackgroundColor;
            }
            else if (e.PropertyName == nameof(FontSize))
            {
                MenuSettings.MenuItemFontSize = FontSize;
            }
            else if (e.PropertyName == nameof(AreIconsVisible))
            {
                MenuSettings.AreMenuItemIconsVisible = AreIconsVisible;
            }
            else if (e.PropertyName == nameof(IconSize))
            {
                MenuSettings.MenuItemIconSize = (int)IconSize;
            }
        }
        #endregion
    }
}

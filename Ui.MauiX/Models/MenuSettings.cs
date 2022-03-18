using Ui.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Ui.MauiX.CustomControls;
using Xamarin.Forms;
using Ui.MauiX.Resources;
using Components.Helpers;
using Ui.Enums;

namespace Ui.MauiX.Models
{
    /// <summary>
    /// Settings for rendering a <see cref="Menu"/>.
    /// </summary>
    public class MenuSettings : NotifyPropertyChanged
    {
        #region Fields
        private Orientation _orientation = Orientation.Vertical;
        private int _separatorThickness = 1;
        private double _menuItemHeight = 60;
        private double _menuItemWidth = 125;
        private int _menuItemIconSize = 25;
        private bool _areMenuItemIconsVisible = true;
        private double _menuItemFontSize = 14;
        private bool _isMenuItemTextVisible = true;
        private Color _backgroundColor = Color.White;
        private Color _menuItemTextColor = CustomColors.TextColor;
        private Color _disabledMenuItemTextColor = CustomColors.DisabledTextColor;
        private Color _menuItemBackgroundColor = Color.White;
        private Color _selectedMenuItemBackgroundColor = Color.LightBlue;
        private Color _disabledMenuItemBackgroundColor = CustomColors.LightDisabledBackgroundColor;
        #endregion

        #region Properties
        /// <summary>
        /// The <see cref="Ui.Enums.Orientation"/> of the <see cref="Menu"/>.
        /// </summary>
        public Orientation Orientation { get => _orientation; internal set { SetProperty(ref _orientation, value); } }

        /// <summary>
        /// The thickness of the <see cref="Separator"/>s in the <see cref="Menu"/>.
        /// </summary>
        public int SeparatorThickness { get => _separatorThickness; set { SetProperty(ref _separatorThickness, value); } }

        /// <summary>
        /// The height of the menu items.
        /// <para>Note: This property has to be manually set becuase of a bug in the GTK implementation of Xamarin Forms.</para>
        /// </summary>
        public double MenuItemHeight { get => _menuItemHeight; set { SetProperty(ref _menuItemHeight, value); } }

        /// <summary>
        /// The width of the menu items.
        /// <para>Note: This property has to be manually set becuase of a bug in the GTK implementation of Xamarin Forms.</para>
        /// </summary>
        public double MenuItemWidth { get => _menuItemWidth; set { SetProperty(ref _menuItemWidth, value); } }

        /// <summary>
        /// The size of the menu item icons.
        /// </summary>
        public int MenuItemIconSize { get => _menuItemIconSize; set { SetProperty(ref _menuItemIconSize, value); } }

        /// <summary>
        /// Determines whether or not the menu item icons are displayed.
        /// </summary>
        public bool AreMenuItemIconsVisible { get => _areMenuItemIconsVisible; set { SetProperty(ref _areMenuItemIconsVisible, value); } }

        /// <summary>
        /// The font size of the menu items.
        /// </summary>
        public double MenuItemFontSize { get => _menuItemFontSize; set { SetProperty(ref _menuItemFontSize, value); } }

        /// <summary>
        /// Determines whether or not the menu item text is displayed.
        /// </summary>
        public bool IsMenuItemTextVisible { get => _isMenuItemTextVisible; set { SetProperty(ref _isMenuItemTextVisible, value); } }

        /// <summary>
        /// The background <see cref="Color"/> of the <see cref="Menu"/>.
        /// </summary>
        public Color BackgroundColor { get => _backgroundColor; set { SetProperty(ref _backgroundColor, value); } }

        /// <summary>
        /// The text <see cref="Color"/> of menu items.
        /// </summary>
        public Color MenuItemTextColor { get => _menuItemTextColor; set { SetProperty(ref _menuItemTextColor, value); } }

        /// <summary>
        /// The text <see cref="Color"/> of disabled menu items.
        /// </summary>
        public Color DisabledMenuItemTextColor { get => _disabledMenuItemTextColor; set { SetProperty(ref _disabledMenuItemTextColor, value); } }

        /// <summary>
        /// The background <see cref="Color"/> of menu items.
        /// </summary>
        public Color MenuItemBackgroundColor { get => _menuItemBackgroundColor; set { SetProperty(ref _menuItemBackgroundColor, value); } }

        /// <summary>
        /// The background <see cref="Color"/> of selected menu items.
        /// </summary>
        public Color SelectedMenuItemBackgroundColor { get => _selectedMenuItemBackgroundColor; set { SetProperty(ref _selectedMenuItemBackgroundColor, value); } }

        /// <summary>
        /// The background <see cref="Color"/> of disabled menu items.
        /// </summary>
        public Color DisabledMenuItemBackgroundColor { get => _disabledMenuItemBackgroundColor; set { SetProperty(ref _disabledMenuItemBackgroundColor, value); } }
        #endregion
    }
}

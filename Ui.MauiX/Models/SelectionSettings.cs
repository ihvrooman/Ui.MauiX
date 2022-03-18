using Components.Helpers;
using Ui.MauiX.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Ui.MauiX.CustomControls;
using Xamarin.Forms;
using Ui.Enums;

namespace Ui.MauiX.Models
{
    /// <summary>
    /// Settings used in a <see cref="SelectionControl"/>.
    /// </summary>
    public class SelectionSettings : NotifyPropertyChanged
    {
        #region Fields
        private Color _selectedItemColor = CustomColors.Blue;
        private Color _unselectedItemColor = Color.White;
        private Color _selectedItemTextColor = Color.Black;
        private Color _unselectedItemTextColor = Color.Black;
        private StackOrientation _orientation = StackOrientation.Vertical;
        private double _buttonSize = 30;
        private double _buttonPadding = 3;
        private double _buttonBorderThickness = 1.5;
        private double _spacing = 15;
        private double _fontSize = 16;
        private ButtonShape _buttonShape = ButtonShape.Circular;
        private SelectionMode _selectionMode = SelectionMode.Single;
        private Color _backgroundColor = Color.White;
        #endregion

        #region Properties
        /// <summary>
        /// The default <see cref="Models.SelectionSettings"/> used when creating a new <see cref="Models.SelectionSettings"/> object.
        /// </summary>
        public static SelectionSettings Default { get; set; } = new SelectionSettings();

        /// <summary>
        /// The color of selected items.
        /// </summary>
        public Color SelectedItemColor { get => _selectedItemColor; set => SetProperty(ref _selectedItemColor, value); }

        /// <summary>
        /// The color of unselected items.
        /// </summary>
        public Color UnselectedItemColor { get => _unselectedItemColor; set => SetProperty(ref _unselectedItemColor, value); }

        /// <summary>
        /// The text color of selected items.
        /// </summary>
        public Color SelectedItemTextColor { get => _selectedItemTextColor; set => SetProperty(ref _selectedItemTextColor, value); }

        /// <summary>
        /// The text color of unselected items.
        /// </summary>
        public Color UnselectedItemTextColor { get => _unselectedItemTextColor; set => SetProperty(ref _unselectedItemTextColor, value); }

        /// <summary>
        /// The <see cref="StackOrientation"/> used when displaying the items.
        /// </summary>
        public StackOrientation Orientation { get => _orientation; set => SetProperty(ref _orientation, value); }

        /// <summary>
        /// The size of the selection buttons.
        /// </summary>
        public double ButtonSize { get => _buttonSize; set => SetProperty(ref _buttonSize, value); }

        /// <summary>
        /// The internal padding of the selection buttons.
        /// </summary>
        public double ButtonPadding { get => _buttonPadding; set => SetProperty(ref _buttonPadding, value); }

        /// <summary>
        /// The thickness of the selection button's border.
        /// </summary>
        public double ButtonBorderThickness { get => _buttonBorderThickness; set => SetProperty(ref _buttonBorderThickness, value); }

        /// <summary>
        /// The spacing between items.
        /// </summary>
        public double Spacing { get => _spacing; set => SetProperty(ref _spacing, value); }

        /// <summary>
        /// The font size.
        /// </summary>
        public double FontSize { get => _fontSize; set => SetProperty(ref _fontSize, value); }

        /// <summary>
        /// The <see cref="Ui.Enums.ButtonShape"/>.
        /// </summary>
        public ButtonShape ButtonShape { get => _buttonShape; set => SetProperty(ref _buttonShape, value); }

        /// <summary>
        /// The <see cref="Xamarin.Forms.SelectionMode"/> used when displaying the items.
        /// </summary>
        public SelectionMode SelectionMode { get => _selectionMode; set => SetProperty(ref _selectionMode, value); }

        /// <summary>
        /// The background color.
        /// </summary>
        public Color BackgroundColor { get => _backgroundColor; set => SetProperty(ref _backgroundColor, value); }
        #endregion

        #region Constructor
        public SelectionSettings()
        {
            if (Default != null)
            {
                CopyFrom(Default);
            }
        }
        #endregion

        #region Methods
        private void CopyFrom(SelectionSettings selectionSettings)
        {
            SelectedItemColor = selectionSettings.SelectedItemColor;
            UnselectedItemColor = selectionSettings.UnselectedItemColor;
            SelectedItemTextColor = selectionSettings.SelectedItemTextColor;
            UnselectedItemTextColor = selectionSettings.UnselectedItemTextColor;
            Orientation = selectionSettings.Orientation;
            ButtonSize = selectionSettings.ButtonSize;
            ButtonPadding = selectionSettings.ButtonPadding;
            ButtonBorderThickness = selectionSettings.ButtonBorderThickness;
            Spacing = selectionSettings.Spacing;
            FontSize = selectionSettings.FontSize;
            ButtonShape = selectionSettings.ButtonShape;
            SelectionMode = selectionSettings.SelectionMode;
            BackgroundColor = selectionSettings.BackgroundColor;
        }
        #endregion
    }
}

using Components.Models;
using Ui.MauiX.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Ui.Enums;
using System.Collections;
using Ui.MauiX.Models;

namespace Ui.MauiX.CustomControls
{
    /// <summary>
    /// A control used to select an item from a list. Several templates are available in the <see cref="Resources.Style"/> sheet which can be used to customize the appearance of the selection buttons.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectionControl : ContentViewWithSetPropertyMethod
    {
        #region Fields
        private CornerRadius _internalCornerRadius = new CornerRadius(0);
        private Command _selectItemCommand;
        #endregion

        #region Properties
        /// <summary>
        /// The collection of items to be displayed.
        /// </summary>
        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// The items source property.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(SelectionControl), propertyChanged: ItemsSourceProperty_Changed);

        /// <summary>
        /// The <see cref="DataTemplate"/> used to render items.
        /// <para>Note: When providing a custom item template, be sure to bind a gesture recognizer to the <see cref="SelectItemCommand"/> to provide selection functionality.</para>
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        /// <summary>
        /// The item template property.
        /// <para>Note: The item template must start with a <see cref="CustomContentView"/>.</para>
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(SelectionControl), validateValue: (bindable, value) =>
        {
            return value == null || ((DataTemplate)value).CreateContent() is CustomViewCell;
        });

        /// <summary>
        /// The <see cref="SelectionSettings"/>.
        /// </summary>
        public SelectionSettings Settings
        {
            get => (SelectionSettings)GetValue(SettingsProperty);
            set => SetValue(SettingsProperty, value);
        }

        /// <summary>
        /// The settings property.
        /// </summary>
        public static readonly BindableProperty SettingsProperty = BindableProperty.Create(nameof(Settings), typeof(SelectionSettings), typeof(SelectionControl), defaultValue: new SelectionSettings(), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (SelectionControl)bindable;
            if (oldValue is SelectionSettings oldSettings)
            {
                oldSettings.PropertyChanged -= control.Settings_PropertyChanged;
            }
            if (newValue is SelectionSettings newSettings)
            {
                newSettings.PropertyChanged += control.Settings_PropertyChanged;
            }
        });     

        /// <summary>
        /// The corner radius used internally by the <see cref="SelectionControl"/>.
        /// </summary>
        public CornerRadius InternalCornerRadius { get => _internalCornerRadius; private set => SetProperty(ref _internalCornerRadius, value); }

        /// <summary>
        /// The command for selecting an item.
        /// <para>Note: When developers provide a custom item template, be sure to bind a gesture recognizer to this command to provide selection functionality.</para>
        /// </summary>
        public Command SelectItemCommand
        {
            get
            {
                if (_selectItemCommand == null)
                {
                    _selectItemCommand = new Command(SelectItem);
                }
                return _selectItemCommand;
            }
        }

        /// <summary>
        /// Indicates whether or not the <see cref="SelectionControl"/> is enabled.
        /// </summary>
        public new bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        /// <summary>
        /// The is enabled property.
        /// </summary>
        public static readonly new BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(SelectionControl), true);

        public IEnumerable<object> SelectedItems { get => ItemsControl.VisualItems?.Where(i => i is CustomViewCell c && c.IsSelected && c.IsEnabled).Select((view, result) => view.BindingContext); }
        #endregion

        #region Events
        /// <summary>
        /// An event indicating that the selected items have changed.
        /// </summary>
        public event EventHandler<IEnumerable<object>> SelectedItemsChanged;
        #endregion

        #region Constructor
        public SelectionControl()
        {
            InitializeComponent();
            SetDefaultItemTemplate();
            SetInternalCornerRadius();
        }
        #endregion

        #region Methods
        private void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectionSettings.ButtonSize) || e.PropertyName == nameof(SelectionSettings.ButtonPadding) || e.PropertyName == nameof(SelectionSettings.ButtonBorderThickness))
            {
                SetInternalCornerRadius();
            }
            else if (e.PropertyName == nameof(SelectionSettings.SelectionMode))
            {
                SanitizeItemSelections();
            }
        }

        private static void ItemsSourceProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SelectionControl)bindable;
            var item = control.ItemsControl.VisualItems?.FirstOrDefault(i => i is CustomViewCell c && c.IsSelected && c.IsEnabled);
            if (item is CustomViewCell c1)
            {
                control.SetSelectedItem(c1);
            }
        }

        private void SetSelectedItem(CustomViewCell selectedViewCell)
        {
            if (Settings.SelectionMode == SelectionMode.None)
            {
                SanitizeItemSelections();
                return;
            }

            if (selectedViewCell != null && !selectedViewCell.IsEnabled)
            {
                return;
            }

            if (ItemsSource == null)
            {
                return;
            }

            if (Settings.SelectionMode == SelectionMode.Single)
            {
                foreach (var item in ItemsControl.VisualItems)
                {
                    ((CustomViewCell)item).IsSelected = item == selectedViewCell;
                }
            }
            else if (selectedViewCell != null)
            {
                //TODO: If selection mode is multiple and this method is called when items source changes, it will deselect the first selected item. Need to fix that.
                selectedViewCell.IsSelected = !selectedViewCell.IsSelected;
            }
            RaiseSelectedItemsChanged();
        }

        private void SetInternalCornerRadius()
        {
            InternalCornerRadius = new CornerRadius(Settings.ButtonSize / 2 - Settings.ButtonPadding - Settings.ButtonBorderThickness);
        }

        private void RaiseSelectedItemsChanged()
        {
            SelectedItemsChanged?.Invoke(this, SelectedItems);
        }

        private void SelectItem(object obj)
        {
            if (!IsEnabled)
            {
                return;
            }

            SetSelectedItem((CustomViewCell)obj);
        }

        private void SanitizeItemSelections()
        {
            if (ItemsSource == null)
            {
                return;
            }

            if (Settings.SelectionMode == SelectionMode.None)
            {
                ItemsControl.VisualItems.ForEach(i => ((CustomViewCell)i).IsSelected = false);
            }
            else if (Settings.SelectionMode == SelectionMode.Single)
            {
                var firstSelectedItem = ItemsControl.VisualItems.FirstOrDefault(i => i is CustomViewCell c && c.IsSelected && c.IsEnabled);
                ItemsControl.VisualItems.ForEach(i =>
                {
                    var c = (CustomViewCell)i;
                    c.IsSelected = c == firstSelectedItem;
                });
            }
        }

        private void SetDefaultItemTemplate()
        {
            if (ItemTemplate != null)
            {
                return;
            }

            ItemTemplate = (DataTemplate)Resources["SelectionControlRadioButtonDataTemplate"];
        }
        #endregion
    }
}
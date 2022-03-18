using Ui.Enums;
using Ui.Helpers;
using Ui.MauiX.Models;
using Ui.MauiX.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentView
    {
        #region Fields
        private Command _selectCustomMenuItemCommand;
        #endregion

        #region Properties
        public Command SelectCustomMenuItemCommand
        {
            get
            {
                if (_selectCustomMenuItemCommand == null)
                {
                    _selectCustomMenuItemCommand = new Command(SelectCustomMenuItem);
                }
                return _selectCustomMenuItemCommand;
            }
        }

        /// <summary>
        /// The <see cref="MauiX.Models.MenuSettings"/>.
        /// </summary>
        public MenuSettings MenuSettings
        {
            get => (MenuSettings)GetValue(MenuSettingsProperty);
            set => SetValue(MenuSettingsProperty, value);
        }

        /// <summary>
        /// The menu settings property.
        /// </summary>
        public static readonly BindableProperty MenuSettingsProperty = BindableProperty.Create(nameof(MenuSettings), typeof(MenuSettings), typeof(Menu), new MenuSettings(), propertyChanged: MenuSettingsProperty_Changed);

        /// <summary>
        /// The <see cref="CustomMenuItem"/>s from which the <see cref="Menu"/> is created.
        /// </summary>
        public IEnumerable<CustomMenuItem> CustomMenuItems
        {
            get => (IEnumerable<CustomMenuItem>)GetValue(CustomMenuItemsProperty);
            set => SetValue(CustomMenuItemsProperty, value);
        }

        /// <summary>
        /// The custom menu items property.
        /// </summary>
        public static readonly BindableProperty CustomMenuItemsProperty = BindableProperty.Create(nameof(CustomMenuItems), typeof(IEnumerable<CustomMenuItem>), typeof(Menu), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var menu = (Menu)bindable;
            menu.SetDefaultPage();
        });
        #endregion

        #region Events
        public event EventHandler<CustomMenuItem> SelectedMenuItemChanged;
        #endregion

        #region Constructor
        public Menu()
        {
            InitializeComponent();
            MenuSettingsProperty_Changed(this, null, MenuSettings);
        }
        #endregion

        #region Methods
        #region Public / Internal
        /// <summary>
        /// Navigates to the page with the specified <see cref="Type"/>.
        /// </summary>
        /// <param name="targetPageType">The <see cref="Type"/> of the desired page.</param>
        public void NavigateToPage(Type targetPageType)
        {
            if (CustomMenuItems == null)
            {
                throw new Exception("Cannot navigate to page because there are no custom menu items.");
            }

            var customMenuItem = FindMenuItemByPageType(targetPageType);
            if (customMenuItem == null || customMenuItem.IsSelected)
            {
                return;
            }

            SelectCustomMenuItem(customMenuItem);
        }

        /// <summary>
        /// Navigates to the tab specified by the <see cref="CustomMenuItem"/>.
        /// </summary>
        /// <param name="customMenuItem">The <see cref="CustomMenuItem"/> to navigate to.</param>
        internal void NavigateToCustomMenuItem(CustomMenuItem customMenuItem)
        {
            if (CustomMenuItems == null)
            {
                throw new Exception("Cannot navigate to page because there are no custom menu items.");
            }

            if (!CustomMenuItems.Contains(customMenuItem))
            {
                throw new ArgumentException($"The specified {nameof(CustomMenuItem)} is not present in the collection of {nameof(CustomMenuItem)}s.", nameof(customMenuItem));
            }

            if (customMenuItem == null || customMenuItem.IsSelected)
            {
                return;
            }

            SelectCustomMenuItem(customMenuItem);
        }
        #endregion

        #region Private
        private static void MenuSettingsProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var menu = (Menu)bindable;
            if (oldValue is MenuSettings oldMenuSettings)
            {
                oldMenuSettings.PropertyChanged -= menu.MenuSettings_PropertyChanged;
            }
            if (newValue is MenuSettings newMenuSettings)
            {
                newMenuSettings.PropertyChanged += menu.MenuSettings_PropertyChanged;
                menu.MenuSettings_PropertyChanged(menu, new PropertyChangedEventArgs(nameof(Models.MenuSettings.Orientation)));
            }
        }

        private void MenuSettings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MenuSettings.Orientation))
            {
                if (MenuSettings == null)
                {
                    return;
                }

                switch (MenuSettings.Orientation)
                {
                    case Orientation.Horizontal:
                        HorizontalOptions = LayoutOptions.Fill;
                        VerticalOptions = LayoutOptions.Start;
                        FirstSeparator.HorizontalOptions = LayoutOptions.Start;
                        FirstSeparator.VerticalOptions = LayoutOptions.Fill;
                        break;
                    case Orientation.Vertical:
                        HorizontalOptions = LayoutOptions.Start;
                        VerticalOptions = LayoutOptions.Fill;
                        FirstSeparator.HorizontalOptions = LayoutOptions.Fill;
                        FirstSeparator.VerticalOptions = LayoutOptions.Start;
                        break;
                    default:
                        break;
                }
            }
        }

        private void SelectCustomMenuItem(object obj)
        {
            if (obj is CustomMenuItem customMenuItem)
            {
                if (!customMenuItem.IsEnabled || customMenuItem.IsSelected)
                {
                    return;
                }

                CustomMenuItems?.ForEach(m => m.IsSelected = false);
                customMenuItem.IsSelected = true;
                RaiseSelectedMenuItemChanged(customMenuItem);
            }
        }

        private void RaiseSelectedMenuItemChanged(CustomMenuItem customMenuItem)
        {
            SelectedMenuItemChanged?.Invoke(this, customMenuItem);
        }

        private CustomMenuItem FindMenuItemByPageType(Type targetPageType)
        {
            return CustomMenuItems?.FirstOrDefault(c => c.TargetType == targetPageType);
        }

        private void SetDefaultPage()
        {
            SelectCustomMenuItem(CustomMenuItems.FirstOrDefault(c => c.IsEnabled));
        }
        #endregion
        #endregion
    }
}
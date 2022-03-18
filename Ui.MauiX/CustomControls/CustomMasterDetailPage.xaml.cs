using Ui.Enums;
using Ui.Helpers;
using Ui.MauiX.Helpers;
using Ui.MauiX.Models;
using Ui.MauiX.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomMasterDetailPage : ContentPageWithSetPropertyMethod
    {
        #region Fields
        private bool _isPopoverMainMenuVisible;
        private bool _isHeaderVisible;
        private View _selectedPage;
        private bool _isSubmenuVisible;
        #endregion

        #region Properties
        /// <summary>
        /// The collection of <see cref="CustomMenuItem"/>s from which the main menu is created.
        /// </summary>
        public IEnumerable<CustomMenuItem> CustomMenuItems
        {
            get => (IEnumerable<CustomMenuItem>)GetValue(CustomMenuItemsProperty);
            set => SetValue(CustomMenuItemsProperty, value);
        }

        /// <summary>
        /// The custom menu items property.
        /// </summary>
        public static readonly BindableProperty CustomMenuItemsProperty = BindableProperty.Create(nameof(CustomMenuItems), typeof(IEnumerable<CustomMenuItem>), typeof(CustomMasterDetailPage));

        /// <summary>
        /// Determines the behavior of the main menu.
        /// </summary>
        public MenuMode MenuMode
        {
            get => (MenuMode)GetValue(MenuModeProperty);
            set => SetValue(MenuModeProperty, value);
        }

        /// <summary>
        /// The menu mode property.
        /// </summary>
        public static readonly BindableProperty MenuModeProperty = BindableProperty.Create(nameof(MenuMode), typeof(MenuMode), typeof(CustomMasterDetailPage), MenuMode.Popover);

        /// <summary>
        /// The <see cref="Models.MenuSettings"/> that control the appearance of the main menu and the sub menu.
        /// <para>To put menu items in the sub menu, use a <see cref="CustomContentView"/> as one of your pages and add <see cref="CustomToolbarItem"/>s to the <see cref="CustomContentView.CustomToolbarItemsProperty"/> with the <see cref="CustomToolbarItem.Order"/> property set to <see cref="ToolbarItemOrder.Secondary"/>.</para>
        /// </summary>
        public MenuSettings MenuSettings
        {
            get => (MenuSettings)GetValue(MenuSettingsProperty);
            set => SetValue(MenuSettingsProperty, value);
        }

        /// <summary>
        /// The menu settings property.
        /// </summary>
        public static readonly BindableProperty MenuSettingsProperty = BindableProperty.Create(nameof(MenuSettings), typeof(MenuSettings), typeof(CustomMasterDetailPage), new MenuSettings() { MenuItemWidth = 250 });

        /// <summary>
        /// The <see cref="Models.MenuSettings"/> that control the appearance of the header menu.
        /// <para>To put menu items in the header menu, use a <see cref="CustomContentView"/> as one of your pages and add <see cref="CustomToolbarItem"/>s to the <see cref="CustomContentView.CustomToolbarItems"/> with the <see cref="CustomToolbarItem.Order"/> property set to <see cref="ToolbarItemOrder.Primary"/>.</para>
        /// </summary>
        public MenuSettings HeaderMenuSettings
        {
            get => (MenuSettings)GetValue(HeaderMenuSettingsProperty);
            set => SetValue(HeaderMenuSettingsProperty, value);
        }

        /// <summary>
        /// The header menu settings property.
        /// </summary>
        public static readonly BindableProperty HeaderMenuSettingsProperty = BindableProperty.Create(nameof(HeaderMenuSettings), typeof(MenuSettings), typeof(CustomMasterDetailPage), new MenuSettings() { MenuItemWidth = 50, MenuItemHeight = 15, MenuItemFontSize = 10, DisabledMenuItemBackgroundColor = Color.White, });

        /// <summary>
        /// The <see cref="Color"/> used for fading the background when the popover main menu appears.
        /// </summary>
        public Color BackgroundFadeColor
        {
            get => (Color)GetValue(BackgroundFadeColorProperty);
            set => SetValue(BackgroundFadeColorProperty, value);
        }

        /// <summary>
        /// The background fade color property.
        /// </summary>
        public static readonly BindableProperty BackgroundFadeColorProperty = BindableProperty.Create(nameof(BackgroundFadeColor), typeof(Color), typeof(CustomMasterDetailPage), CustomColors.BackgroundFadeColor);

        /// <summary>
        /// Indicates whether or not the popover main menu is visible
        /// </summary>
        public bool IsPopoverMainMenuVisible { get => _isPopoverMainMenuVisible; set => SetProperty(ref _isPopoverMainMenuVisible, value); }

        /// <summary>
        /// The header to display.
        /// </summary>
        public View Header
        {
            get => (View)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        /// <summary>
        /// The header property.
        /// </summary>
        public static readonly BindableProperty HeaderProperty = BindableProperty.Create(nameof(Header), typeof(View), typeof(CustomMasterDetailPage), propertyChanged: (binable, oldValue, newValue) =>
        {
            var page = (CustomMasterDetailPage)binable;
            page.IsHeaderVisible = newValue != null;
        });

        /// <summary>
        /// Indicates whether or not the header is visible.
        /// </summary>
        public bool IsHeaderVisible { get => _isHeaderVisible; set => SetProperty(ref _isHeaderVisible, value); }

        /// <summary>
        /// The background <see cref="Color"/> of the header.
        /// </summary>
        public Color HeaderBackgroundColor
        {
            get => (Color)GetValue(HeaderBackgroundColorProperty);
            set => SetValue(HeaderBackgroundColorProperty, value);
        }

        /// <summary>
        /// The header background color property.
        /// </summary>
        public static readonly BindableProperty HeaderBackgroundColorProperty = BindableProperty.Create(nameof(HeaderBackgroundColor), typeof(Color), typeof(CustomMasterDetailPage), Color.White);

        /// <summary>
        /// The <see cref="View"/> representing the selected page.
        /// </summary>
        public View SelectedPage { get => _selectedPage; set => SetProperty(ref _selectedPage, value); }

        /// <summary>
        /// Indicates whether or not pages are cached.
        /// <para>Caching pages saves user input when switching between pages but could consume large amounts of memory if there are many pages.</para>
        /// </summary>
        public bool ArePagesCached
        {
            get => (bool)GetValue(ArePagesCachedProperty);
            set => SetValue(ArePagesCachedProperty, value);
        }

        /// <summary>
        /// The are pages cached property.
        /// </summary>
        public static readonly BindableProperty ArePagesCachedProperty = BindableProperty.Create(nameof(ArePagesCached), typeof(bool), typeof(CustomMasterDetailPage), true);

        /// <summary>
        /// Indicates whether or not the submenu is visible.
        /// </summary>
        public bool IsSubmenuVisible { get => _isSubmenuVisible; set => SetProperty(ref _isSubmenuVisible, value); }
        #endregion

        #region Events
        public event EventHandler<CustomMenuItem> SelectedMenuItemChanged;
        #endregion

        #region Constructor
        public CustomMasterDetailPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        #region Public
        /// <summary>
        /// Navigates to the page with the specified <see cref="Type"/>.
        /// </summary>
        /// <param name="targetPageType">The <see cref="Type"/> of the desired page.</param>
        public void NavigateToPage(Type targetPageType)
        {
            switch (MenuMode)
            {
                case MenuMode.Popover:
                    PopoverMenu.NavigateToPage(targetPageType);
                    break;
                case MenuMode.SplitOnPortrait:
                    PortraitMenu.NavigateToPage(targetPageType);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Clears the selected page.
        /// </summary>
        public void ClearSelectedPage()
        {
            CustomMenuItems.ForEach(c => c.IsSelected = false);
            SelectedPage = null;
        }

        /// <summary>
        /// Sets the isEnabled property of the page with the specified <see cref="Type"/> to the specified value.
        /// </summary>
        /// <param name="targetPageType">The <see cref="Type"/> of the desired page.</param>
        /// <param name="isEnabled">A <see cref="bool"/> indicating whether or not the page should be enabled.</param>
        public void SetPageIsEnabledProperty(Type targetPageType, bool isEnabled)
        {
            var customMenuItem = FindMenuItemByPageType(targetPageType);
            if (customMenuItem == null)
            {
                return;
            }
            customMenuItem.IsEnabled = isEnabled; 
            if (!isEnabled && customMenuItem.IsSelected)
            {
                ClearSelectedPage();
            }
        }

        /// <summary>
        /// Sets the isEnabled property of all the pages to the specified value.
        /// </summary>
        /// <param name="isEnabled">A <see cref="bool"/> indicating whether or not the pages should be enabled.</param>
        public void SetIsEnabledPropertyOfAllPages(bool isEnabled)
        {
            CustomMenuItems?.ForEach(c => c.IsEnabled = isEnabled);
            if (!isEnabled)
            {
                ClearSelectedPage();
            }
        }
        #endregion

        #region Private
        private void HamburgerMenuIcon_Tapped(object sender, EventArgs e)
        {
            if (MenuMode != MenuMode.Popover)
            {
                return;
            }

            IsPopoverMainMenuVisible = !IsPopoverMainMenuVisible;
        }

        private void ClosePopoverMainMenu_Tapped(object sender, EventArgs e)
        {
            IsPopoverMainMenuVisible = false;
        }

        private void Menu_SelectedMenuItemChanged(object sender, CustomMenuItem e)
        {
            SetSelectedPage(e);
            RaiseSelectedMenuItemChanged(e);
        }

        private void SetSelectedPage(CustomMenuItem customMenuItem)
        {
            if (MenuMode == MenuMode.Popover)
            {
                IsPopoverMainMenuVisible = false;
            }
            SelectedPage = (View)CustomActivator.GetInstance(customMenuItem.TargetType, ArePagesCached);
        }

        private void SubmenuIcon_Tapped(object sender, EventArgs e)
        {
            IsSubmenuVisible = !IsSubmenuVisible;
        }

        private void CloseSubmenu_Tapped(object sender, EventArgs e)
        {
            if (sender is MenuItem menuItem && !menuItem.IsEnabled)
            {
                return;
            }
            IsSubmenuVisible = false;
        }

        private CustomMenuItem FindMenuItemByPageType(Type targetPageType)
        {
            return CustomMenuItems?.FirstOrDefault(c => c.TargetType == targetPageType);
        }

        private void RaiseSelectedMenuItemChanged(CustomMenuItem customMenuItem)
        {
            SelectedMenuItemChanged?.Invoke(this, customMenuItem);
        }
        #endregion
        #endregion
    }
}
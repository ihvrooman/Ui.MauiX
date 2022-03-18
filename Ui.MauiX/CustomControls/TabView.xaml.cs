using Ui.Enums;
using Ui.Helpers;
using Ui.MauiX.Models;
using Ui.MauiX.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabView : ContentView
    {
        #region Properties
        /// <summary>
        /// Determines the <see cref="Alignment"/> of the <see cref="TabView"/>'s tabs.
        /// </summary>
        public Alignment TabAlignment
        {
            get => (Alignment)GetValue(TabAlignmentProperty);
            set => SetValue(TabAlignmentProperty, value);
        }

        /// <summary>
        /// The tab alignment property.
        /// </summary>
        public static readonly BindableProperty TabAlignmentProperty = BindableProperty.Create(nameof(TabAlignment), typeof(Alignment), typeof(TabView), Alignment.Top, propertyChanged: TabAlignmentProperty_Changed);

        /// <summary>
        /// The selected tab menu item.
        /// </summary>
        public CustomMenuItem SelectedTabMenuItem { get => CustomMenuItems?.FirstOrDefault(m => m.IsSelected); }

        /// <summary>
        /// The index of the selected tab menu item.
        /// </summary>
        public int SelectedTabMenuItemIndex
        {
            get
            {
                if (SelectedTabMenuItem == null)
                {
                    return -1;
                }
                return CustomMenuItems.IndexOf(SelectedTabMenuItem);
            }
        }

        /// <summary>
        /// Gets the target <see cref="Type"/> of the selected <see cref="Ui.MauiX.Models.CustomMenuItem"/>.
        /// </summary>
        public Type SelectedTabType
        {
            get => SelectedTabMenuItem?.TargetType;
        }

        /// <summary>
        /// Gets the view of the selected tab.
        /// </summary>
        public View SelectedTabView { get => Content_View.Content; }

        /// <summary>
        /// Determines whether or not the pages are cached.
        /// </summary>
        public bool ArePagesCached
        {
            get => (bool)GetValue(ArePagesCachedProperty);
            set => SetValue(ArePagesCachedProperty, value);
        }

        /// <summary>
        /// The are pages cached property.
        /// </summary>
        public static readonly BindableProperty ArePagesCachedProperty = BindableProperty.Create(nameof(ArePagesCached), typeof(bool), typeof(TabView), true);

        /// <summary>
        /// The <see cref="Models.MenuSettings"/> that control the appearance of the tabs.
        /// </summary>
        public MenuSettings MenuSettings
        {
            get => (MenuSettings)GetValue(MenuSettingsProperty);
            set => SetValue(MenuSettingsProperty, value);
        }

        public static readonly BindableProperty MenuSettingsProperty = BindableProperty.Create(nameof(MenuSettings), typeof(MenuSettings), typeof(TabView), new MenuSettings());

        /// <summary>
        /// The <see cref="CustomMenuItem"/>s from which the tabs are created.
        /// </summary>
        public IEnumerable<CustomMenuItem> CustomMenuItems
        {
            get => (IEnumerable<CustomMenuItem>)GetValue(CustomMenuItemsProperty);
            set => SetValue(CustomMenuItemsProperty, value);
        }

        /// <summary>
        /// The custom menu items property.
        /// </summary>
        public static readonly BindableProperty CustomMenuItemsProperty = BindableProperty.Create(nameof(CustomMenuItems), typeof(IEnumerable<CustomMenuItem>), typeof(TabView));
        #endregion

        #region Events
        public event EventHandler<SelectedTabChangedEventArgs> SelectedTabChanged;
        #endregion

        #region Constructor
        public TabView()
        {
            InitializeComponent();
            TabAlignmentProperty_Changed(this, null, TabAlignment);
        }
        #endregion

        #region Methods
        #region Public / Internal
        /// <summary>
        /// Navigates to the tab with the specified <see cref="Type"/>.
        /// </summary>
        /// <param name="targetTabType">The <see cref="Type"/> of the desired tab.</param>
        public void NavigateToTab(Type targetTabType)
        {
            Menu.NavigateToPage(targetTabType);
        }

        /// <summary>
        /// Navigates to the tab specified by the <see cref="CustomMenuItem"/>.
        /// </summary>
        /// <param name="customMenuItem">The <see cref="CustomMenuItem"/> to navigate to.</param>
        internal void NavigateToCustomMenuItem(CustomMenuItem customMenuItem)
        {
            Menu.NavigateToCustomMenuItem(customMenuItem);
        }

        /// <summary>
        /// Sets the isEnabled property of the tab with the specified <see cref="Type"/> to the specified value.
        /// </summary>
        /// <param name="targetTabType">The <see cref="Type"/> of the desired tab.</param>
        /// <param name="isEnabled">A <see cref="bool"/> indicating whether or not the tab should be enabled.</param>
        public void SetTabIsEnabledProperty(Type targetTabType, bool isEnabled)
        {
            var tabMenuItem = FindCustomMenuItemByTabType(targetTabType);
            if (tabMenuItem == null)
            {
                return;
            }
            tabMenuItem.IsEnabled = isEnabled;

            if (!isEnabled && SelectedTabMenuItem == tabMenuItem)
            {
                ClearSelectedTab();
            }
        }

        /// <summary>
        /// Sets the isEnabled property of all the tabs to the specified value.
        /// </summary>
        /// <param name="isEnabled">A <see cref="bool"/> indicating whether or not the tabs should be enabled.</param>
        public void SetIsEnabledPropertyOfAllTabs(bool isEnabled)
        {
            CustomMenuItems?.ForEach(t => t.IsEnabled = isEnabled);

            if (!isEnabled)
            {
                ClearSelectedTab();
            }
        }

        /// <summary>
        /// Clears the selected tab.
        /// </summary>
        public void ClearSelectedTab()
        {
            CustomMenuItems?.ForEach(m => m.IsSelected = false);
            Content_View.Content = null;
        }
        #endregion

        #region Private
        private void RaiseSelectedTabChanged(View oldSelectedTabView, View newSelectedTabView)
        {
            SelectedTabChanged?.Invoke(this, new SelectedTabChangedEventArgs(oldSelectedTabView, newSelectedTabView));
        }

        private CustomMenuItem FindCustomMenuItemByTabType(Type targetTabType)
        {
            return CustomMenuItems?.FirstOrDefault(c => c.TargetType == targetTabType);
        }

        private static void TabAlignmentProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var tabView = (TabView)bindable;
            var alignment = (Alignment)newValue;

            Device.BeginInvokeOnMainThread(() =>
            {
                var menu = tabView.Menu;
                menu.IsVisible = true;

                switch (alignment)
                {
                    case Alignment.Top:
                        menu.MenuSettings.Orientation = Orientation.Horizontal;
                        Grid.SetRow(menu, 0);
                        Grid.SetRowSpan(menu, 1);
                        Grid.SetColumn(menu, 0);
                        Grid.SetColumnSpan(menu, 5);
                        break;
                    case Alignment.Bottom:
                        menu.MenuSettings.Orientation = Orientation.Horizontal;
                        Grid.SetRow(menu, 4);
                        Grid.SetRowSpan(menu, 1);
                        Grid.SetColumn(menu, 0);
                        Grid.SetColumnSpan(menu, 5);
                        break;
                    case Alignment.Left:
                        menu.MenuSettings.Orientation = Orientation.Vertical;
                        Grid.SetColumn(menu, 0);
                        Grid.SetColumnSpan(menu, 1);
                        Grid.SetRow(menu, 0);
                        Grid.SetRowSpan(menu, 5);
                        break;
                    case Alignment.Right:
                        menu.MenuSettings.Orientation = Orientation.Vertical;
                        Grid.SetColumn(menu, 4);
                        Grid.SetColumnSpan(menu, 1);
                        Grid.SetRow(menu, 0);
                        Grid.SetRowSpan(menu, 5);
                        break;
                    case Alignment.Hidden:
                        menu.IsVisible = false;
                        break;
                    default:
                        break;
                }
            });
        }

        private void Menu_SelectedMenuItemChanged(object sender, CustomMenuItem e)
        {
            SetContent(e);
        }

        private void SetContent(CustomMenuItem selectedCustomMenuItem)
        {
            var oldView = Content_View.Content;
            var newView = (View)CustomActivator.GetInstance(selectedCustomMenuItem.TargetType, ArePagesCached);
            Content_View.Content = newView;
            RaiseSelectedTabChanged(oldView, newView);
        }
        #endregion
        #endregion
    }
}
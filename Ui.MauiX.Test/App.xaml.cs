using Components.Models;
using Ui.Enums;
using Ui.MauiX.CustomControls;
using Ui.MauiX.CustomControls.ViewModels;
using Ui.MauiX.Helpers;
using Ui.MauiX.Models;
using Ui.MauiX.Resources;
using Ui.MauiX.Test.Models;
using Ui.MauiX.Test.ViewModels;
using Ui.MauiX.Test.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.Test
{
    public partial class App : Application
    {
        public static CustomMasterDetailPage CustomMasterDetailPage
        {
            get => (CustomMasterDetailPage)Current.MainPage;
        }

        public App()
        {
            InitializeComponent();

            SelectionSettings.Default.ButtonBorderThickness = 2;

            #region Custom master detail page
            var masterDetailPage = new CustomMasterDetailPage();
            masterDetailPage.SelectedMenuItemChanged += MasterDetailPage_SelectedMenuItemChanged;
            masterDetailPage.MenuMode = MenuMode.Popover;
            masterDetailPage.ArePagesCached = true;
            masterDetailPage.Header = new Header() { ViewModel = new HeaderViewModel(), };
            masterDetailPage.CustomMenuItems = new List<CustomMenuItem>()
            {
                new CustomMenuItem("Detail Page 1", "MenuTest.png", typeof(DetailPage1)),
                new CustomMenuItem("Detail Page 2", "DeviceInfo.png", typeof(DetailPage2)),
                new CustomMenuItem("Detail Page 3", "MenuTest.png", typeof(DetailPage3)),
                new CustomMenuItem("Detail Page 4", "DeviceInfo.png", typeof(DetailPage4)),
                new CustomMenuItem("Control Library", "MenuTest.png", typeof(ControlLibraryPage)),
            };
            masterDetailPage.HeaderMenuSettings.MenuItemWidth = 150;
            masterDetailPage.MenuSettings.MenuItemWidth = 300;
            MainPage = masterDetailPage;            

            RunCustomMasterDetailPageTests();

            //RunCustomMasterDetailPageDialogTests();
            #endregion

            BaseViewModel.CustomMasterDetailPageInstance.PropertyChanged += BaseViewModel_PropertyChanged;
        }

        private void BaseViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BaseViewModel.TextColor))
            {
                CustomMasterDetailPage.MenuSettings.MenuItemTextColor = BaseViewModel.CustomMasterDetailPageInstance.TextColor;
                CustomMasterDetailPage.HeaderMenuSettings.MenuItemTextColor = BaseViewModel.CustomMasterDetailPageInstance.TextColor;
            }
            else if (e.PropertyName == nameof(BaseViewModel.DisabledTextColor))
            {
                CustomMasterDetailPage.MenuSettings.DisabledMenuItemTextColor = BaseViewModel.CustomMasterDetailPageInstance.DisabledTextColor;
                CustomMasterDetailPage.HeaderMenuSettings.DisabledMenuItemTextColor = BaseViewModel.CustomMasterDetailPageInstance.DisabledTextColor;
            }
            else if (e.PropertyName == nameof(BaseViewModel.BackgroundColor))
            {
                CustomMasterDetailPage.MenuSettings.MenuItemBackgroundColor = BaseViewModel.CustomMasterDetailPageInstance.BackgroundColor;
                CustomMasterDetailPage.HeaderMenuSettings.MenuItemBackgroundColor = BaseViewModel.CustomMasterDetailPageInstance.BackgroundColor;
            }
            else if (e.PropertyName == nameof(BaseViewModel.DisabledBackgroundColor))
            {
                CustomMasterDetailPage.MenuSettings.DisabledMenuItemBackgroundColor = BaseViewModel.CustomMasterDetailPageInstance.DisabledBackgroundColor;
                CustomMasterDetailPage.HeaderMenuSettings.DisabledMenuItemBackgroundColor = BaseViewModel.CustomMasterDetailPageInstance.DisabledBackgroundColor;
            }
            else if (e.PropertyName == nameof(BaseViewModel.FontSize))
            {
                CustomMasterDetailPage.MenuSettings.MenuItemFontSize = BaseViewModel.CustomMasterDetailPageInstance.FontSize;
                CustomMasterDetailPage.HeaderMenuSettings.MenuItemFontSize = BaseViewModel.CustomMasterDetailPageInstance.FontSize;
            }
            else if (e.PropertyName == nameof(BaseViewModel.AreIconsVisible))
            {
                CustomMasterDetailPage.MenuSettings.AreMenuItemIconsVisible = BaseViewModel.CustomMasterDetailPageInstance.AreIconsVisible;
                CustomMasterDetailPage.HeaderMenuSettings.AreMenuItemIconsVisible = BaseViewModel.CustomMasterDetailPageInstance.AreIconsVisible;
            }
            else if (e.PropertyName == nameof(BaseViewModel.IconSize))
            {
                CustomMasterDetailPage.MenuSettings.MenuItemIconSize = (int)BaseViewModel.CustomMasterDetailPageInstance.IconSize;
                CustomMasterDetailPage.HeaderMenuSettings.MenuItemIconSize = (int)BaseViewModel.CustomMasterDetailPageInstance.IconSize;
            }
            else if (e.PropertyName == nameof(BaseViewModel.IsEnabled))
            {
                var selectedPageType = CustomMasterDetailPage.SelectedPage?.GetType();
                CustomMasterDetailPage.CustomMenuItems.ForEach(c =>
                {
                    if (c.TargetType == selectedPageType)
                    {
                        return;
                    }

                    CustomMasterDetailPage.SetPageIsEnabledProperty(c.TargetType, BaseViewModel.CustomMasterDetailPageInstance.IsEnabled);
                });
            }
        }

        private void MasterDetailPage_SelectedMenuItemChanged(object sender, CustomMenuItem e)
        {
            MessagingCenter.Send(this, MessagingCenterMessages.MasterDetailPageSelectedMenuItemChanged.ToString(), e);
        }

        private async void RunCustomMasterDetailPageTests()
        {
            await Task.Delay(200);

            //await App.Current.MainPage.ShowNumberPad();
            //await App.Current.MainPage.ShowKeyboard();
            //await App.Current.MainPage.ShowSelectionDialog(new List<string>() { "A", "B" });

            //await App.Current.MainPage.OpenModalPopup(new TestPanScrolling());
            //var list = new List<int>() { 1, 2, 3, 4, 5 };
            //var items = new List<BaseSelectableItem>()
            //{
            //    new BaseSelectableItem(){DisplayText = "1"},
            //    new BaseSelectableItem(){DisplayText = "2", IsEnabled = false},
            //    new BaseSelectableItem(){DisplayText = "3"},
            //    new BaseSelectableItem(){DisplayText = "4"},
            //    new BaseSelectableItem(){DisplayText = "5"},
            //};
            //var r = await App.Current.MainPage.ShowSelectionDialog(list, selectionMode: SelectionMode.Multiple);
            //await Current.MainPage.ShowNumberPad("DeviceInfo.png", "Stirrer Speed", 231.46, "RPM", 0, 250, 2, 2);
            //await Current.MainPage.ShowKeyboard("DeviceInfo.png", "Street Name", "St. James", "Road");
            //await CustomMasterDetailPage.NavigateToPage(typeof(View1));
            //CustomMasterDetailPage.ClearSelectedPage();
            //await Task.Delay(1000);
            //await CustomMasterDetailPage.ShowActivityIndicatorWhileTaskExecutes(Task.Delay(10000), "Running Custom Master Detail Page Tests...");
            //await Task.Delay(5020);
            return;

            #region Message declaration
            var longMessage = $"This is going to be a very long message. I must test the dialog to see how it handles extremely long messages. I need to know how it will perform under pressure from the user.{Environment.NewLine + Environment.NewLine}New line.";
            var shortMessage = "Testing, 1...2...3";
            #endregion

            #region CustomMasterDetailPage dialogs
            //await CustomMasterDetailPage.ShowMessageAsync("Title", longMessage, DialogButtonStyle.AffirmativeAndNegativeAndDoubleAuxiliary, "Affirmative", "Negative", "Aux 1", "Aux 2");
            //await CustomMasterDetailPage.ShowMessageAsync(null, longMessage, DialogButtonStyle.AffirmativeAndNegative, "Ok", "Cancel");
            #endregion

            //await CustomMasterDetailPage.ShowActivityIndicatorWhileTaskExecutes(Task.Delay(3000), "Delaying...");

            var page = (CustomMasterDetailPage)MainPage;
            await Task.Delay(3000);

            #region Dialog popups
            //await DialogPopup.ShowMessageAsync(CustomMasterDetailPage, "Title", longMessage, DialogButtonStyle.AffirmativeAndNegativeAndDoubleAuxiliary, "Affirmative", "Negative", "Aux 1", "Aux 2");
            //await DialogPopup.ShowMessageAsync(CustomMasterDetailPage, null, longMessage, DialogButtonStyle.AffirmativeAndNegative, "Ok", "Cancel");
            #endregion

            //page.IsActivityIndicatorVisible = true;
            await Task.Delay(5000);
            //page.ActivityIndicatorColor = Color.Orange;
            //page.ActivityIndicatorMargin = new Thickness(200);
            //page.MenuItemFontSize = 10;
            //page.HeaderMenuFontSize = 30;
            //page.MenuItemIconSize = 15;
            //((CustomMasterDetailPage)MainPage).MenuBackgroundColor = Color.Purple;
            //page.MenuItemTextColor = Color.Orange;
            //page.HeaderMenuItemTextColor = Color.Purple;            
            await Task.Delay(5000);
            //page.IsActivityIndicatorVisible = false;
            //page.MenuItemFontSize = 14;
            //page.HeaderMenuFontSize = 10;
            //page.MenuItemIconSize = 50;
            //((CustomMasterDetailPage)MainPage).MenuBackgroundColor = Color.White;
            //page.MenuItemTextColor = Color.Black;
            //page.HeaderMenuItemTextColor = Color.Black;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public async static void ShowErrorMessage(string message)
        {
            await Current.MainPage.ShowErrorMessageAsync(message);
        }
    }
}

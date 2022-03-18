using Ui.Helpers;
using Ui.MauiX.Helpers;
using Ui.MauiX.Resources;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageNavigator : CustomFrame
    {
        #region Fields
        private Page _page;
        #endregion

        #region Properties
        /// <summary>
        /// The resource id of the icon.
        /// </summary>
        public string IconResourceId
        {
            get => (string)GetValue(IconResourceIdProperty);
            set => SetValue(IconResourceIdProperty, value);
        }

        /// <summary>
        /// The icon resource id property.
        /// </summary>
        public static readonly BindableProperty IconResourceIdProperty = BindableProperty.Create(nameof(IconResourceId), typeof(string), typeof(PageNavigator), defaultValue: string.Empty);

        /// <summary>
        /// The size of the icon.
        /// </summary>
        public double IconSize
        {
            get => (double)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }

        /// <summary>
        /// The icon size property.
        /// </summary>
        public static readonly BindableProperty IconSizeProperty = BindableProperty.Create(nameof(IconSize), typeof(double), typeof(PageNavigator), defaultValue: 50.0);

        /// <summary>
        /// The label which describes where the navigation will take the user.
        /// </summary>
        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        /// <summary>
        /// The label property.
        /// </summary>
        public static readonly BindableProperty LabelProperty = BindableProperty.Create(nameof(Label), typeof(string), typeof(PageNavigator));

        /// <summary>
        /// The preview text shown.
        /// </summary>
        public string Preview
        {
            get => (string)GetValue(PreviewProperty);
            set => SetValue(PreviewProperty, value);
        }

        /// <summary>
        /// The preview property.
        /// </summary>
        public static readonly BindableProperty PreviewProperty = BindableProperty.Create(nameof(Preview), typeof(string), typeof(PageNavigator));

        /// <summary>
        /// The font size.
        /// </summary>
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        /// The font size property.
        /// </summary>
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(PageNavigator), defaultValue: 14.0);

        /// <summary>
        /// The <see cref="PageNavigator"/>'s height request.
        /// </summary>
        public new double HeightRequest
        {
            get => (double)GetValue(HeightRequestProperty);
            set => SetValue(HeightRequestProperty, value);
        }

        /// <summary>
        /// The height request property.
        /// </summary>
        public static readonly new BindableProperty HeightRequestProperty = BindableProperty.Create(nameof(HeightRequest), typeof(double), typeof(PageNavigator), defaultValue: 70.0);

        /// <summary>
        /// The text color.
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// The text color property.
        /// </summary>
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(PageNavigator), Color.Black);

        /// <summary>
        /// The disabled text color.
        /// </summary>
        public Color DisabledTextColor
        {
            get => (Color)GetValue(DisabledTextColorProperty);
            set => SetValue(DisabledTextColorProperty, value);
        }

        /// <summary>
        /// The disabled text color property.
        /// </summary>
        public static readonly BindableProperty DisabledTextColorProperty = BindableProperty.Create(nameof(DisabledTextColor), typeof(Color), typeof(PageNavigator), CustomColors.DisabledTextColor);

        /// <summary>
        /// The <see cref="Type"/> of the page that the user will be directed to.
        /// </summary>
        public Type PageType { get; set; }

        /// <summary>
        /// Determines whether or not the page is cached.
        /// </summary>
        public bool IsPageCached
        {
            get => (bool)GetValue(IsPageCachedProperty);
            set => SetValue(IsPageCachedProperty, value);
        }

        /// <summary>
        /// The is page cached property.
        /// </summary>
        public static readonly BindableProperty IsPageCachedProperty = BindableProperty.Create(nameof(IsPageCached), typeof(bool), typeof(PageNavigator), false, propertyChanged: (bindable, oldValue, newValue) =>
        {
            if (Device.RuntimePlatform == Device.GTK && (bool)newValue)
            {
                throw new Exception($"{nameof(PageNavigator)}.{nameof(IsPageCached)} cannot be set to true on GTK devices.");
            }
        });
        #endregion

        #region Events
        /// <summary>
        /// The event that is raised when the page is opened.
        /// </summary>
        public event EventHandler PageOpened;

        /// <summary>
        /// The event that is raised when the page is closed.
        /// <para>Note: This event is only raised if the page inherits from <see cref="BaseCustomNavigationPage"/>.</para>
        /// </summary>
        public event EventHandler PageClosed;
        #endregion

        #region Constructor
        public PageNavigator()
        {
            DisabledBackgroundColor = CustomColors.LightDisabledBackgroundColor;
            DisabledBorderColor = CustomColors.LightDisabledBorderColor;
            InitializeComponent();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Navigates to the page specified by the <see cref="PageType"/> property.
        /// </summary>
        /// <returns></returns>
        public async Task NavigateToPage()
        {
            if (!IsEnabled)
            {
                return;
            }

            UnsubscribeFromBaseCustomNavigationPageClosedEvent();
            DisposeOfBaseCustomNavigationPage();

            _page = (Page)CustomActivator.GetInstance(PageType, IsPageCached);

            if (_page is BaseCustomNavigationPage newBaseCustomNavigationPage)
            {
                newBaseCustomNavigationPage.Closed += BaseCustomNavigationPageClosed;
            }

            await Application.Current.MainPage.OpenModalPopup(_page);

            RaisePageOpened();
        }

        private async void PageNavigator_Tapped(object sender, EventArgs e)
        {
            await NavigateToPage();
        }

        private void BaseCustomNavigationPageClosed(object sender, EventArgs e)
        {
            RaisePageClosed();
            if (!IsPageCached)
            {
                UnsubscribeFromBaseCustomNavigationPageClosedEvent();
                DisposeOfBaseCustomNavigationPage();
                _page = null;
            }
        }

        private void RaisePageClosed()
        {
            PageClosed?.Invoke(this, new EventArgs());
        }

        private void RaisePageOpened()
        {
            PageOpened?.Invoke(this, new EventArgs());
        }

        private void UnsubscribeFromBaseCustomNavigationPageClosedEvent()
        {
            if (_page is BaseCustomNavigationPage oldBaseCustomNavigationPage)
            {
                oldBaseCustomNavigationPage.Closed -= BaseCustomNavigationPageClosed;
            }
        }

        private void DisposeOfBaseCustomNavigationPage()
        {
            if (_page is BaseCustomNavigationPage oldBaseCustomNavigationPage)
            {
                oldBaseCustomNavigationPage.Dispose();
            }
        }
        #endregion
    }
}
using Ui.MauiX.CustomControls.ViewModels;
using Ui.MauiX.Helpers;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    /// <summary>
    /// A popup for showing an activity indicator.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityIndicatorPopup : ContentPage
    {
        #region Properties
        internal ActivityIndicatorPopupViewModel ViewModel { get => (ActivityIndicatorPopupViewModel)BindingContext; }

        /// <summary>
        /// The activity indicator color.
        /// </summary>
        public Color ActivityIndicatorColor { get => ViewModel.ActivityIndicatorSettings.Color; set => ViewModel.ActivityIndicatorSettings.Color = value; }

        /// <summary>
        /// The activity indicator margin.
        /// </summary>
        public Thickness ActivityIndicatorMargin { get => ViewModel.ActivityIndicatorSettings.Margin; set => ViewModel.ActivityIndicatorSettings.Margin = value; }

        /// <summary>
        /// The font size of the activity indicator.
        /// </summary>
        public int ActivityIndicatorMessageFontSize { get => ViewModel.ActivityIndicatorSettings.MessageFontSize; set => ViewModel.ActivityIndicatorSettings.MessageFontSize = value; }

        /// <summary>
        /// The color used to fade the background of the application when the activity indicator is shown.
        /// </summary>
        public Color BackgroundFadeColor
        {
            get => ViewModel.ActivityIndicatorSettings.BackgroundFadeColor;
            set => ViewModel.ActivityIndicatorSettings.BackgroundFadeColor = value;
        }
        #endregion

        #region Constructor
        public ActivityIndicatorPopup()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Shows the activity indicator to the user while the provided <see cref="Task"/> executes.
        /// </summary>
        /// <typeparam name="T">The return type of the <see cref="Task"/>.</typeparam>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which this popup is to be displayed.</param>
        /// <param name="task">The <see cref="Task"/> with return type T.</param>
        /// <param name="message">The optional message to show under the activity indicator.</param>
        /// <returns></returns>
        public static async Task<T> ShowActivityIndicatorWhileTaskExecutes<T>(NavigableElement navigableElement, Task<T> task, string message = null)
        {
            return await Device.InvokeOnMainThreadAsync(async () =>
            {
                var popupPushed = false;
                ActivityIndicatorPopup popup = null;
                try
                {
                    popup = new ActivityIndicatorPopup();
                    await navigableElement.OpenModalPopup(popup);
                    popupPushed = true;
                    var result = await task.ShowActivityIndicatorWhileTaskExecutes(message, popup.ViewModel.ActivityIndicatorSettings);
                    return result;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (popupPushed)
                    {
                        await navigableElement.CloseModalPopup(popup);
                    }
                }
            });
        }

        /// <summary>
        /// Shows the activity indicator to the user while the provided <see cref="Task"/> executes.
        /// </summary>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which this popup is to be displayed.</param>
        /// <param name="task">The <see cref="Task"/>.</param>
        /// <param name="message">The optional message to show under the activity indicator.</param>
        /// <returns></returns>
        public static async Task ShowActivityIndicatorWhileTaskExecutes(NavigableElement navigableElement, Task task, string message = null)
        {
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                var popupPushed = false;
                ActivityIndicatorPopup popup = null;
                try
                {
                    popup = new ActivityIndicatorPopup();
                    await navigableElement.OpenModalPopup(popup);
                    popupPushed = true;
                    await task.ShowActivityIndicatorWhileTaskExecutes(message, popup.ViewModel.ActivityIndicatorSettings);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (popupPushed)
                    {
                        await navigableElement.CloseModalPopup(popup);
                    }
                }
            });
        }
        #endregion
    }
}
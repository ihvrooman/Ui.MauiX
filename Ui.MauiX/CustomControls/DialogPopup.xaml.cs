using Ui.MauiX.Enums;
using Ui.MauiX.Helpers;
using Ui.MauiX.Models;
using Ui.MauiX.Resources;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    /// <summary>
    /// A popup for showing a <see cref="Dialog"/>.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DialogPopup : ContentPage
    {
        #region Properties
        /// <summary>
        /// The default <see cref="Models.DialogSettings"/> used when showing a <see cref="CustomControls.Dialog"/>.
        /// <para>Note: Developers can edit the default <see cref="Models.DialogSettings"/> at the start of their application and the changes will apply to all <see cref="CustomControls.Dialog"/>s shown using the <see cref="DialogPopup"/>.</para>
        /// </summary>
        public static DialogSettings DefaultDialogSettings { get; set; } = new DialogSettings();

        /// <summary>
        /// The <see cref="Models.DialogSettings"/> used to display the <see cref="CustomControls.Dialog"/>.
        /// </summary>
        public DialogSettings DialogSettings
        {
            get => (DialogSettings)GetValue(DialogSettingsProperty);
            set => SetValue(DialogSettingsProperty, value);
        }

        /// <summary>
        /// The dialog settings property.
        /// </summary>
        public static readonly BindableProperty DialogSettingsProperty = BindableProperty.Create(nameof(DialogSettings), typeof(DialogSettings), typeof(DialogPopup), new DialogSettings());
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new <see cref="DialogPopup"/>.
        /// <para>Note: Developers should use one of the <see cref="DialogPopup"/>'s static SHOW methods rather than creating a new instance of the control.</para>
        /// </summary>
        internal DialogPopup()
        {
            InitializeComponent();
            if (DefaultDialogSettings != null)
            {
                DialogSettings = DefaultDialogSettings;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Shows a <see cref="Dialog"/> with the specified title, message, and buttons to the user asynchronously.
        /// </summary>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which this popup is to be displayed.</param>
        /// <param name="title">The dialog title.</param>
        /// <param name="message">The dialog message.</param>
        /// <param name="dialogButtonStyle">The <see cref="DialogButtonStyle"/>.</param>
        /// <param name="affirmativeButtonTextOverride">The affirmative button text override.</param>
        /// <param name="negativeButtonTextOverride">The negative button text override.</param>
        /// <param name="firstAuxiliaryButtonTextOverride">The first auxiliary button text override.</param>
        /// <param name="secondAuxiliaryButtonTextOverride">The second auxiliary button text override.</param>
        /// <param name="affirmativeButtonColorOverride">The affirmative button color override.</param>
        /// <param name="negativeButtonColorOverride">The negative button color override.</param>
        /// <param name="firstAuxiliaryButtonColorOverride">The first auxiliary button color override.</param>
        /// <param name="secondAuxiliaryButtonColorOverride">The second auxiliary button color override.</param>
        /// <param name="headerColorOverride">The header color override.</param>
        /// <param name="titleColorOverride">The title color override.</param>
        /// <param name="iconResourceIdOverride">The resource id override for the icon.</param>
        /// <param name="defaultButtonOverride">The default button override.</param>
        /// <returns></returns>        
        public static async Task<DialogResult> ShowMessageAsync(NavigableElement navigableElement, string title, string message, DialogButtonStyle dialogButtonStyle = DialogButtonStyle.Affirmative, string affirmativeButtonTextOverride = "Ok", string negativeButtonTextOverride = null, string firstAuxiliaryButtonTextOverride = null, string secondAuxiliaryButtonTextOverride = null, Color? affirmativeButtonColorOverride = null, Color? negativeButtonColorOverride = null, Color? firstAuxiliaryButtonColorOverride = null, Color? secondAuxiliaryButtonColorOverride = null, Color? headerColorOverride = null, Color? titleColorOverride = null, string iconResourceIdOverride = null, DialogResult? defaultButtonOverride = null)
        {
            return await Device.InvokeOnMainThreadAsync(async () =>
            {
                var popupPushed = false;
                DialogPopup popup = null;
                try
                {
                    popup = new DialogPopup();
                    await navigableElement.OpenModalPopup(popup);
                    popupPushed = true;
                    var result = await DialogController.ShowMessageAsync(title, message, dialogButtonStyle, popup.DialogSettings, affirmativeButtonTextOverride, negativeButtonTextOverride, firstAuxiliaryButtonTextOverride, secondAuxiliaryButtonTextOverride, affirmativeButtonColorOverride, negativeButtonColorOverride, firstAuxiliaryButtonColorOverride, secondAuxiliaryButtonColorOverride, headerColorOverride, titleColorOverride, iconResourceIdOverride, defaultButtonOverride);
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
        /// Shows a <see cref="Dialog"/> with an affirmative and negative button and the specified title and message to the user asynchronously.
        /// <para>Note: This method is designed for confirming user action. For instance, it could be used to confirm whether or not the user wants to close the application.</para>
        /// </summary>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which this popup is to be displayed.</param>
        /// <param name="title">The dialog title.</param>
        /// <param name="message">The dialog message.</param>
        /// <param name="affirmativeButtonTextOverride">The affirmative button text override.</param>
        /// <param name="negativeButtonTextOverride">The negative button text override.</param>
        /// <param name="defaultButtonOverride">The default button override.</param>
        public static async Task<DialogResult> ShowConfirmationDialogAsync(NavigableElement navigableElement, string title, string message, string affirmativeButtonTextOverride = "Yes", string negativeButtonTextOverride = "No", DialogResult? defaultButtonOverride = DialogResult.Affirmative)
        {
            return await ShowMessageAsync(navigableElement, title, message, DialogButtonStyle.AffirmativeAndNegative, affirmativeButtonTextOverride, negativeButtonTextOverride, defaultButtonOverride: defaultButtonOverride);
        }

        /// <summary>
        /// Shows a <see cref="Dialog"/> with a red header, an affirmative button, the specified title, icon, and message to the user asynchronously.
        /// <para>Note: This method is designed for showing an error message to the user.</para>
        /// </summary>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which this popup is to be displayed.</param>
        /// <param name="title">The dialog title.</param>
        /// <param name="message">The dialog message.</param>
        /// <param name="affirmativeButtonTextOverride">The affirmative button text override.</param>
        /// <param name="iconResourceIdOverride">The resource id override of the icon.</param>
        public static async Task<DialogResult> ShowErrorMessageAsync(NavigableElement navigableElement, string message, string title = "Error", string affirmativeButtonTextOverride = "Ok", string iconResourceIdOverride = "error-white-50.png")
        {
            return await ShowMessageAsync(navigableElement, title, message, DialogButtonStyle.Affirmative, affirmativeButtonTextOverride, headerColorOverride: CustomColors.Red, iconResourceIdOverride: iconResourceIdOverride);
        }
        #endregion
    }
}
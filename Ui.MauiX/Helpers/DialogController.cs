using Ui.MauiX.CustomControls;
using Ui.MauiX.Enums;
using Ui.MauiX.Models;
using Ui.MauiX.Resources;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ui.MauiX.Helpers
{
    /// <summary>
    /// Provides methods for controlling a <see cref="Dialog"/>.
    /// </summary>
    public static class DialogController
    {
        #region Methods
        /// <summary>
        /// Shows a <see cref="Dialog"/> with the specified title, message, and buttons to the user asynchronously.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="message">The dialog message.</param>
        /// <param name="dialogButtonStyle">The <see cref="DialogButtonStyle"/>.</param>
        /// <param name="dialogSettings">The <see cref="DialogSettings"/> used to control the <see cref="Dialog"/>.</param>
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
        public static async Task<DialogResult> ShowMessageAsync(string title, string message, DialogButtonStyle dialogButtonStyle, DialogSettings dialogSettings, string affirmativeButtonTextOverride = null, string negativeButtonTextOverride = null, string firstAuxiliaryButtonTextOverride = null, string secondAuxiliaryButtonTextOverride = null, Color? affirmativeButtonColorOverride = null, Color? negativeButtonColorOverride = null, Color? firstAuxiliaryButtonColorOverride = null, Color? secondAuxiliaryButtonColorOverride = null, Color? headerColorOverride = null, Color? titleColorOverride = null, string iconResourceIdOverride = null, DialogResult? defaultButtonOverride = null)
        {
            await dialogSettings.WaitForDialogToClose();

            #region Parameter validation
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Parameter cannot be null or whitespace.", nameof(title));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Parameter cannot be null or whitespace.", nameof(message));
            }

            if (dialogSettings == null)
            {
                throw new ArgumentException("Parameter cannot be null.", nameof(dialogSettings));
            }
            #endregion

            #region Get original settings
            var originalAffirmativeButtonColor = dialogSettings.AffirmativeButtonColor;
            var originalNegativeButtonColor = dialogSettings.NegativeButtonColor;
            var originalFirstAuxiliaryButtonColor = dialogSettings.FirstAuxiliaryButtonColor;
            var originalSecondAuxiliaryButtonColor = dialogSettings.SecondAuxiliaryButtonColor;
            var originalHeaderColor = dialogSettings.HeaderColor;
            var originalTitleColor = dialogSettings.TitleColor;
            var originalIconResourceId = dialogSettings.IconResourceId;
            var originalDefaultButton = dialogSettings.DefaultButton;
            var originalAffirmativeButtonText = dialogSettings.AffirmativeButtonText;
            var originalNegativeButtonText = dialogSettings.NegativeButtonText;
            var originalFirstAuxiliaryButtonText = dialogSettings.FirstAuxiliaryButtonText;
            var originalSecondAuxiliaryButtonText = dialogSettings.SecondAuxiliaryButtonText;
            #endregion

            dialogSettings.Title = title;
            dialogSettings.Message = message;
            dialogSettings.ButtonStyle = dialogButtonStyle;

            if (dialogButtonStyle == DialogButtonStyle.Affirmative)
            {
                /* If the button style is affirmative,
                 * default to having a blue button
                 */
                dialogSettings.DefaultAffirmativeButtonColor = dialogSettings.AffirmativeButtonColor;
                dialogSettings.AffirmativeButtonColor = CustomColors.Blue;
            }
            else
            {
                //Reset affirmative button color back to default
                dialogSettings.AffirmativeButtonColor = dialogSettings.DefaultAffirmativeButtonColor;
            }

            //Apply overrides
            if (affirmativeButtonColorOverride != null)
            {
                dialogSettings.AffirmativeButtonColor = (Color)affirmativeButtonColorOverride;
            }
            if (negativeButtonColorOverride != null)
            {
                dialogSettings.NegativeButtonColor = (Color)negativeButtonColorOverride;
            }
            if (firstAuxiliaryButtonColorOverride != null)
            {
                dialogSettings.FirstAuxiliaryButtonColor = (Color)firstAuxiliaryButtonColorOverride;
            }
            if (secondAuxiliaryButtonColorOverride != null)
            {
                dialogSettings.SecondAuxiliaryButtonColor = (Color)secondAuxiliaryButtonColorOverride;
            }
            if (headerColorOverride != null)
            {
                dialogSettings.HeaderColor = (Color)headerColorOverride;
            }
            if (titleColorOverride != null)
            {
                dialogSettings.TitleColor = (Color)titleColorOverride;
            }
            if (iconResourceIdOverride != null)
            {
                dialogSettings.IconResourceId = iconResourceIdOverride;
            }
            if (defaultButtonOverride != null)
            {
                dialogSettings.DefaultButton = (DialogResult)defaultButtonOverride;
            }
            if (affirmativeButtonTextOverride != null)
            {
                dialogSettings.AffirmativeButtonText = affirmativeButtonTextOverride;
            }
            if (negativeButtonTextOverride != null)
            {
                dialogSettings.NegativeButtonText = negativeButtonTextOverride;
            }
            if (firstAuxiliaryButtonTextOverride != null)
            {
                dialogSettings.FirstAuxiliaryButtonText = firstAuxiliaryButtonTextOverride;
            }
            if (secondAuxiliaryButtonTextOverride != null)
            {
                dialogSettings.SecondAuxiliaryButtonText = secondAuxiliaryButtonTextOverride;
            }

            #region Settings validation
            if (string.IsNullOrWhiteSpace(dialogSettings.AffirmativeButtonText))
            {
                throw new ArgumentException("Parameter cannot be null or whitespace.", "affirmativeButtonText");
            }

            var exceptionMessage = "Property in DialogSettings cannot be null or whitespace with the given DialogButtonStyle.";

            if (dialogButtonStyle != DialogButtonStyle.Affirmative && string.IsNullOrWhiteSpace(dialogSettings.NegativeButtonText))
            {
                throw new ArgumentException(exceptionMessage, nameof(dialogSettings.NegativeButtonText));
            }

            if ((dialogButtonStyle == DialogButtonStyle.AffirmativeAndNegativeAndSingleAuxiliary || dialogButtonStyle == DialogButtonStyle.AffirmativeAndNegativeAndDoubleAuxiliary) && string.IsNullOrWhiteSpace(dialogSettings.FirstAuxiliaryButtonText))
            {
                throw new ArgumentException(exceptionMessage, nameof(dialogSettings.FirstAuxiliaryButtonText));
            }

            if (dialogButtonStyle == DialogButtonStyle.AffirmativeAndNegativeAndDoubleAuxiliary && string.IsNullOrWhiteSpace(dialogSettings.SecondAuxiliaryButtonText))
            {
                throw new ArgumentException(exceptionMessage, nameof(dialogSettings.SecondAuxiliaryButtonText));
            }

            exceptionMessage = "Property in DialogSettings is not valid with the given DialogButtonStyle.";
            bool throwDefaultButtonArgumentException = false;
            switch (dialogSettings.DefaultButton)
            {
                case DialogResult.Negative:
                    throwDefaultButtonArgumentException = dialogButtonStyle == DialogButtonStyle.Affirmative;
                    break;
                case DialogResult.FirstAuxiliary:
                    throwDefaultButtonArgumentException = dialogButtonStyle != DialogButtonStyle.AffirmativeAndNegativeAndSingleAuxiliary && dialogButtonStyle != DialogButtonStyle.AffirmativeAndNegativeAndDoubleAuxiliary;
                    break;
                case DialogResult.SecondAuxiliary:
                    throwDefaultButtonArgumentException = dialogButtonStyle != DialogButtonStyle.AffirmativeAndNegativeAndDoubleAuxiliary;
                    break;
                default:
                    break;
            }
            if (throwDefaultButtonArgumentException)
            {
                throw new ArgumentException(exceptionMessage, nameof(dialogSettings.DefaultButton));
            }
            #endregion

            dialogSettings.IsVisible = true;

            while (dialogSettings.Result == DialogResult.Null)
            {
                await Task.Delay(50);
            }

            dialogSettings.IsVisible = false;
            var result = dialogSettings.Result;
            dialogSettings.Result = DialogResult.Null;

            #region Reset settings
            dialogSettings.AffirmativeButtonColor = originalAffirmativeButtonColor;
            dialogSettings.NegativeButtonColor = originalNegativeButtonColor;
            dialogSettings.FirstAuxiliaryButtonColor = originalFirstAuxiliaryButtonColor;
            dialogSettings.SecondAuxiliaryButtonColor = originalSecondAuxiliaryButtonColor;
            dialogSettings.HeaderColor = originalHeaderColor;
            dialogSettings.TitleColor = originalTitleColor;
            dialogSettings.IconResourceId = originalIconResourceId;
            dialogSettings.DefaultButton = originalDefaultButton;
            dialogSettings.AffirmativeButtonText = originalAffirmativeButtonText;
            dialogSettings.NegativeButtonText = originalNegativeButtonText;
            dialogSettings.FirstAuxiliaryButtonText = originalFirstAuxiliaryButtonText;
            dialogSettings.SecondAuxiliaryButtonText = originalSecondAuxiliaryButtonText;
            #endregion

            return result;
        }


        /// <summary>
        /// Waits for the dialog controlled by the specified dialog settings to close.
        /// </summary>
        /// <param name="dialogSettings"></param>
        /// <returns></returns>
        public static async Task WaitForDialogToClose(this DialogSettings dialogSettings)
        {
            while (dialogSettings.IsVisible || dialogSettings.Result != DialogResult.Null)
            {
                await Task.Delay(50);
            }
        }
        #endregion
    }
}

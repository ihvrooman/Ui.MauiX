using DataCollections.Models;
using Ui.Enums;
using Ui.MauiX.CustomControls;
using Ui.MauiX.Enums;
using Ui.MauiX.Models;
using Ui.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Ui.MauiX.CustomControls.LoginPopup;

namespace Ui.MauiX.Helpers
{
    /// <summary>
    /// Extension methods for <see cref="NavigableElement"/>s.
    /// </summary>
    public static class NavigableElementExtensions
    {
        #region Activity Indicator
        /// <summary>
        /// Shows the activity indicator to the user while the provided <see cref="Task"/> executes.
        /// </summary>
        /// <typeparam name="T">The return type of the <see cref="Task"/>.</typeparam>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which this popup is to be displayed.</param>
        /// <param name="task">The <see cref="Task"/> with return type T.</param>
        /// <param name="message">The optional message to show under the activity indicator.</param>
        /// <returns></returns>
        public static async Task<T> ShowActivityIndicatorWhileTaskExecutes<T>(this NavigableElement navigableElement, Task<T> task, string message = null)
        {
            return await ActivityIndicatorPopup.ShowActivityIndicatorWhileTaskExecutes(navigableElement, task, message);
        }

        /// <summary>
        /// Shows the activity indicator to the user while the provided <see cref="Task"/> executes.
        /// </summary>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which this popup is to be displayed.</param>
        /// <param name="task">The <see cref="Task"/>.</param>
        /// <param name="message">The optional message to show under the activity indicator.</param>
        /// <returns></returns>
        public static async Task ShowActivityIndicatorWhileTaskExecutes(this NavigableElement navigableElement, Task task, string message = null)
        {
            await ActivityIndicatorPopup.ShowActivityIndicatorWhileTaskExecutes(navigableElement, task, message);
        }
        #endregion

        #region Dialog
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
        public static async Task<DialogResult> ShowMessageAsync(this NavigableElement navigableElement, string title, string message, DialogButtonStyle dialogButtonStyle = DialogButtonStyle.Affirmative, string affirmativeButtonTextOverride = "Ok", string negativeButtonTextOverride = null, string firstAuxiliaryButtonTextOverride = null, string secondAuxiliaryButtonTextOverride = null, Color? affirmativeButtonColorOverride = null, Color? negativeButtonColorOverride = null, Color? firstAuxiliaryButtonColorOverride = null, Color? secondAuxiliaryButtonColorOverride = null, Color? headerColorOverride = null, Color? titleColorOverride = null, string iconResourceIdOverride = null, DialogResult? defaultButtonOverride = null)
        {
            return await DialogPopup.ShowMessageAsync(navigableElement, title, message, dialogButtonStyle, affirmativeButtonTextOverride, negativeButtonTextOverride, firstAuxiliaryButtonTextOverride, secondAuxiliaryButtonTextOverride, affirmativeButtonColorOverride, negativeButtonColorOverride, firstAuxiliaryButtonColorOverride, secondAuxiliaryButtonColorOverride, headerColorOverride, titleColorOverride, iconResourceIdOverride, defaultButtonOverride);
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
        public static async Task<DialogResult> ShowConfirmationDialogAsync(this NavigableElement navigableElement, string title, string message, string affirmativeButtonTextOverride = "Yes", string negativeButtonTextOverride = "No", DialogResult? defaultButtonOverride = DialogResult.Affirmative)
        {
            return await DialogPopup.ShowConfirmationDialogAsync(navigableElement, title, message, affirmativeButtonTextOverride, negativeButtonTextOverride, defaultButtonOverride);
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
        public static async Task<DialogResult> ShowErrorMessageAsync(this NavigableElement navigableElement, string message, string title = "Error", string affirmativeButtonTextOverride = "Ok", string iconResourceIdOverride = "error-white-50.png")
        {
            return await DialogPopup.ShowErrorMessageAsync(navigableElement, message, title, affirmativeButtonTextOverride, iconResourceIdOverride);
        }
        #endregion

        #region Input Controls
        /// <summary>
        /// Shows a numberpad to the user to collect numerical input.
        /// </summary>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which the popup is to be displayed.</param>
        /// <param name="iconResourceId">The icon resource id.</param>
        /// <param name="label">The label which describes the value that is being collected from the user.</param>
        /// <param name="initialValue">The initial value to show.</param>
        /// <param name="valueSuffix">The suffix of the value.
        /// <para>The unit of measure would be a common value for this property.</para></param>
        /// <param name="min">The minimum value constraint.</param>
        /// <param name="max">The maximum value constraint.</param>
        /// <param name="maxNumberOfDecimalPlaces">The maximum number of decimal places constraint.</param>
        /// <param name="minNumberOfDecimalPlaces">The minimum number of decimal places constraint.</param>
        /// <returns></returns>
        public static async Task<PopupResult> ShowNumberPad(this NavigableElement navigableElement, string iconResourceId = null, string label = null, double initialValue = 0, string valueSuffix = null, double? min = null, double? max = null, int? maxNumberOfDecimalPlaces = null, int? minNumberOfDecimalPlaces = null)
        {
            return await NumberPadPopup.Show(navigableElement, iconResourceId, label, initialValue, valueSuffix, min, max, maxNumberOfDecimalPlaces, minNumberOfDecimalPlaces);
        }

        /// <summary>
        /// Shows a numberpad to the user to collect numerical input.
        /// </summary>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which the popup is to be displayed.</param>
        /// <param name="doubleDataItem">The <see cref="DataCollections.Models.DoubleDataItem"/>.</param>
        /// <param name="iconResourceId">The icon resource id.</param>
        /// <param name="label">The label which describes the value that is being collected from the user.</param>
        /// <param name="initialValue">The initial value to show.</param>
        /// <param name="valueSuffix">The suffix of the value.
        /// <para>The unit of measure would be a common value for this property.</para></param>
        /// <returns></returns>
        public static async Task<PopupResult> ShowNumberPad(this NavigableElement navigableElement, DoubleDataItem doubleDataItem, string iconResourceId = null, string label = null, double initialValue = 0, string valueSuffix = null)
        {
            return await NumberPadPopup.Show(navigableElement, doubleDataItem, iconResourceId, label, initialValue, valueSuffix);
        }

        /// <summary>
        /// Shows a keyboard to the user to collect alpha-numeric input.
        /// </summary>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which the popup is to be displayed.</param>
        /// <param name="iconResourceId">The icon resource id.</param>
        /// <param name="label">The label which describes the value that is being collected from the user.</param>
        /// <param name="initialValue">The initial value to show.</param>
        /// <param name="valueSuffix">The suffix of the value.
        /// <para>The unit of measure would be a common value for this property.</para></param>
        /// <param name="reservedCharacters">The reserved characters constraint.</param>
        /// <param name="patternMask">The pattern mask constraint.</param>
        /// <param name="min">The minimum number of characters constraint.</param>
        /// <param name="max">The maximum number of characters constraint.</param>
        /// <returns></returns>
        public static async Task<PopupResult> ShowKeyboard(this NavigableElement navigableElement, string iconResourceId = null, string label = null, string initialValue = null, string valueSuffix = null, string reservedCharacters = null, string patternMask = null, double? min = null, double? max = null)
        {
            return await KeyboardPopup.Show(navigableElement, iconResourceId, label, initialValue, valueSuffix, reservedCharacters, patternMask, min, max);
        }

        /// <summary>
        /// Shows a keyboard to the user to collect alpha-numeric input.
        /// </summary>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which the popup is to be displayed.</param>
        /// <param name="stringDataItem">The <see cref="DataCollections.Models.StringDataItem"/>.</param>
        /// <param name="iconResourceId">The icon resource id.</param>
        /// <param name="label">The label which describes the value that is being collected from the user.</param>
        /// <param name="initialValue">The initial value to show.</param>
        /// <param name="valueSuffix">The suffix of the value.
        /// <para>The unit of measure would be a common value for this property.</para></param>
        /// <returns></returns>
        public static async Task<PopupResult> ShowKeyboard(this NavigableElement navigableElement, StringDataItem stringDataItem, string iconResourceId = null, string label = null, string initialValue = null, string valueSuffix = null)
        {
            return await KeyboardPopup.Show(navigableElement, stringDataItem, iconResourceId, label, initialValue, valueSuffix);
        }
        #endregion

        #region Login
        /// <summary>
        /// Shows the <see cref="LoginPopup"/>.
        /// </summary>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which to push the <see cref="LoginPopup"/>.</param>
        /// <param name="authenticateRFID">The <see cref="AuthenticateRFIDHandler"/> used to authenticate with an RFID.</param>
        /// <param name="authenticateUsernamePassword">The <see cref="AuthenticateUsernamePasswordHandler"/> used to authenticate with a username and password.</param>
        /// <param name="quickAuthenticate">The optional <see cref="QuickAuthenticateHandler"/> used to quickly login a user. Note: This method of authentication is activated when the user clicks on the RFID icon.</param>
        /// <param name="useKeyboardPopups">Determines whether or not a <see cref="GetInputPopup"/> will be used for getting the username and password.</param>
        /// <returns></returns>
        public static async Task ShowLoginDialog(this NavigableElement navigableElement, AuthenticateRFIDHandler authenticateRFID, AuthenticateUsernamePasswordHandler authenticateUsernamePassword, QuickAuthenticateHandler quickAuthenticate = null, bool useKeyboardPopups = true)
        {
            await LoginPopup.Show(navigableElement, authenticateRFID, authenticateUsernamePassword, quickAuthenticate, useKeyboardPopups);
        }
        #endregion

        #region Selection
        /// <summary>
        /// Shows an instance of the <see cref="SelectionPopup"/>.
        /// </summary>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which the <see cref="SelectionPopup"/> is to be shown.</param>
        /// <param name="itemsSource">The source of the items to template and display.</param>
        /// <param name="iconResourceId">The icon resource id.</param>
        /// <param name="label">The label which describes the value that is being collected from the user.</param>
        /// <param name="valueSuffix">The suffix of the value.
        /// <para>The unit of measure would be a common value for this property.</para></param>
        /// <param name="itemTemplate">The <see cref="DataTemplate"/> used to render items.</param>
        /// <param name="settingsOverride">The <see cref="SelectionSettings"/> override.</param>
        /// <param name="autoCloseOnItemSelected">Indicates whether or not the popup will automatically close as soon as the user selects an item.
        /// <para>Note: This only applies if the <see cref="Xamarin.Forms.SelectionMode"/> is set to <see cref="Xamarin.Forms.SelectionMode.Single"/>.</para></param>
        /// <returns></returns>
        public static async Task<PopupResult> ShowSelectionDialog(this NavigableElement navigableElement, IEnumerable itemsSource, string iconResourceId = null, string label = null, string valueSuffix = null, DataTemplate itemTemplate = null, SelectionSettings settingsOverride = null, bool autoCloseOnItemSelected = false)
        {
            return await SelectionPopup.Show(navigableElement, itemsSource, iconResourceId, label, valueSuffix, itemTemplate, settingsOverride, autoCloseOnItemSelected);
        }
        #endregion
    }
}

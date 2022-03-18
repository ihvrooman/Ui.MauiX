using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ui.MauiX.Helpers
{
    /// <summary>
    /// A class that contains helper methods for working with popups.
    /// </summary>
    public static class PopupHelper
    {
        #region Methods
        #region Public
        /// <summary>
        /// Opens the provided <see cref="Page"/> as a modal popup over the provided <see cref="NavigableElement"/>.
        /// </summary>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which to open the modal popup.</param>
        /// <param name="popup">The popup to open.</param>
        /// <returns></returns>
        public static async Task OpenModalPopup(this NavigableElement navigableElement, Page popup)
        {
            await navigableElement.Navigation.PushModalAsync(popup);
        }

        /// <summary>
        /// Closes the provided popup.
        /// </summary>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which the modal popup was opened.</param>
        /// <param name="popup">The popup to close.</param>
        /// <returns></returns>
        public static async Task CloseModalPopup(this NavigableElement navigableElement, Page popup)
        {
            if (popup == null)
            {
                throw new ArgumentException("Parameter cannot be null.", nameof(popup));
            }

            if (!GetModalStack(navigableElement).Contains(popup))
            {
                throw new Exception("The provided navigableElement's modal stack does not contain the provided page.");
            }

            while (GetLastModalPage(navigableElement) != popup)
            {
                await Task.Delay(50);
            }
            await PopModalAsync(navigableElement);
        }

        /// <summary>
        /// Closes the popup of the provided <see cref="Type"/>.
        /// </summary>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which the modal popup was opened.</param>
        /// <param name="popupType">The <see cref="Type"/> of the popup to close.</param>
        /// <returns></returns>
        public static async Task CloseModalPopup(this NavigableElement navigableElement, Type popupType)
        {
            if (GetModalStack(navigableElement).FirstOrDefault(p => p.GetType() == popupType) == null)
            {
                throw new Exception("The provided navigableElement's modal stack does not contain a page with the provided type.");
            }

            while (GetLastModalPage(navigableElement).GetType() != popupType)
            {
                await Task.Delay(50);
            }
            await PopModalAsync(navigableElement);
        }
        #endregion

        #region Private
        private static async Task PopModalAsync(NavigableElement navigableElement)
        {
            await navigableElement.Navigation.PopModalAsync();
        }

        private static IReadOnlyCollection<Page> GetModalStack(NavigableElement navigableElement)
        {
            return navigableElement.Navigation.ModalStack;
        }

        private static Page GetLastModalPage(NavigableElement navigableElement)
        {
            return GetModalStack(navigableElement).Last();
        }
        #endregion
        #endregion
    }
}

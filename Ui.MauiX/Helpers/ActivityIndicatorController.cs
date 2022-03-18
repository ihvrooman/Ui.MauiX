using Ui.MauiX.Models;
using System.Threading.Tasks;

namespace Ui.MauiX.Helpers
{
    public static class ActivityIndicatorController
    {
        #region Methods
        #region Public
        /// <summary>
        /// Shows an activity indicator to the user with the provided message while the provided <see cref="Task"/> executes.
        /// </summary>
        /// <typeparam name="T">The return type of the <see cref="Task"/>.</typeparam>
        /// <param name="task">The <see cref="Task"/> with return type T.</param>
        /// <param name="message">The message to show under the activity indicator.</param>
        /// <param name="activityIndicatorSettings">The <see cref="ActivityIndicatorSettings"/>.</param>
        /// <returns></returns>
        public static async Task<T> ShowActivityIndicatorWhileTaskExecutes<T>(this Task<T> task, string message, ActivityIndicatorSettings activityIndicatorSettings)
        {
            try
            {
                await activityIndicatorSettings.WaitForActivityIndicatorToClose();
                ShowActivityIndicator(message, activityIndicatorSettings);
                return await task;
            }
            catch
            {
                throw;
            }
            finally
            {
                HideActivityIndicator(activityIndicatorSettings);
            }
        }

        /// <summary>
        /// Shows an activity indicator to the user with the provided message while the provided <see cref="Task"/> executes.
        /// </summary>
        /// <param name="task">The <see cref="Task"/>.</param>
        /// <param name="message">The message to show under the activity indicator.</param>
        /// <param name="activityIndicatorSettings">The <see cref="ActivityIndicatorSettings"/>.</param>
        /// <returns></returns>
        public static async Task ShowActivityIndicatorWhileTaskExecutes(this Task task, string message, ActivityIndicatorSettings activityIndicatorSettings)
        {
            try
            {
                await activityIndicatorSettings.WaitForActivityIndicatorToClose();
                ShowActivityIndicator(message, activityIndicatorSettings);
                await task;
            }
            catch
            {
                throw;
            }
            finally
            {
                HideActivityIndicator(activityIndicatorSettings);
            }
        }

        /// <summary>
        /// Waits for the activity indicator bound to the provided settings to close.
        /// </summary>
        /// <param name="activityIndicatorSettings">The <see cref="ActivityIndicatorSettings"/> of the activity indicator of interest.</param>
        /// <returns></returns>
        public static async Task WaitForActivityIndicatorToClose(this ActivityIndicatorSettings activityIndicatorSettings)
        {
            while (activityIndicatorSettings.IsVisible)
            {
                await Task.Delay(50);
            }
        }
        #endregion

        #region Private
        private static void ShowActivityIndicator(string message, ActivityIndicatorSettings activityIndicatorSettings)
        {
            activityIndicatorSettings.IsVisible = true;
            activityIndicatorSettings.Message = message;
        }

        private static void HideActivityIndicator(ActivityIndicatorSettings activityIndicatorSettings)
        {
            activityIndicatorSettings.Message = null;
            activityIndicatorSettings.IsVisible = false;
        }
        #endregion
        #endregion
    }
}

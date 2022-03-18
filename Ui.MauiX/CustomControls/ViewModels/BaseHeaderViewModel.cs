using Components.Helpers;
using System;
using System.Globalization;
using System.Timers;
using TimeZoneNames;

namespace Ui.MauiX.CustomControls.ViewModels
{
    /// <summary>
    /// The base implementation of a viewModel for a <see cref="Header"/>.
    /// </summary>
    public class BaseHeaderViewModel : NotifyPropertyChanged
    {
        #region Fields
        private string _pageTitle;
        private string _username;
        private string _dateAndTime;
        private Timer _dateTimeTimer = new Timer(100) { AutoReset = true };
        #endregion

        #region Properties
        /// <summary>
        /// The title of the current page.
        /// </summary>
        public string PageTitle { get => _pageTitle; set { SetProperty(ref _pageTitle, value); } }
        /// <summary>
        /// The current user's name.
        /// </summary>
        public string Username { get => _username; set { SetProperty(ref _username, value); } }
        /// <summary>
        /// The current date and time.
        /// </summary>
        public string DateAndTime { get => _dateAndTime; set { SetProperty(ref _dateAndTime, value); } }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new <see cref="BaseHeaderViewModel"/>.
        /// </summary>
        public BaseHeaderViewModel()
        {
            _dateTimeTimer.Elapsed += DateTimeTimer_Elapsed;
            _dateTimeTimer.Start();
        }
        #endregion

        #region Methods
        private void DateTimeTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DateAndTime = GetCurrentDateTime();
        }

        private string GetCurrentDateTime()
        {
            var timeZone = TimeZoneInfo.Local;
            string timeZoneID = timeZone.Id;
            string lang = CultureInfo.CurrentCulture.Name;
            var timeZoneAbbreviation = TZNames.GetAbbreviationsForTimeZone(timeZoneID, lang);

            return timeZoneAbbreviation.Standard + " " + DateTime.Now.ToLocalTime().ToString("hh:mm tt, dd MMM yyyy");
        }
        #endregion

        #region Finalizer
        /// <summary>
        /// Finalizer.
        /// </summary>
        ~BaseHeaderViewModel()
        {
            _dateTimeTimer.Stop();
        }
        #endregion
    }
}

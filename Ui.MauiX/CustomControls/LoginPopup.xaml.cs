using Ui.Enums;
using Ui.MauiX.Helpers;
using Ui.MauiX.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    /// <summary>
    /// A popup used for authenticating a user.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPopup : ContentPageWithSetPropertyMethod
    {
        #region Fields
        private Command _usernamePasswordLoginCommand;
        private Command _rfidLoginCommand;
        private Command _quickLoginCommand;
        private bool _isRFIDLogin = true;
        private string _username;
        private string _starPassword;
        private string _password;
        private string _rfid;
        private AuthenticateUsernamePasswordHandler _authenticateUsernamePassword;
        private AuthenticateRFIDHandler _authenticateRFID;
        private QuickAuthenticateHandler _quickAuthenticate;
        #endregion

        #region Properties
        public Command UsernamePasswordLoginCommand
        {
            get
            {
                if (_usernamePasswordLoginCommand == null)
                {
                    _usernamePasswordLoginCommand = new Command(UsernamePasswordLogin, CanLoginWithUsernamePassword);
                }
                return _usernamePasswordLoginCommand;
            }
        }

        public Command RFIDLoginCommand
        {
            get
            {
                if (_rfidLoginCommand == null)
                {
                    _rfidLoginCommand = new Command(RFIDLogin);
                }
                return _rfidLoginCommand;
            }
        }

        public Command QuickLoginCommand
        {
            get
            {
                if (_quickLoginCommand == null)
                {
                    _quickLoginCommand = new Command(QuickLogin);
                }
                return _quickLoginCommand;
            }
        }

        /// <summary>
        /// The default <see cref="Models.LoginPopupSettings"/> used when showing the <see cref="LoginPopup"/>.
        /// <para>Note: Developers can edit the default <see cref="Models.LoginPopupSettings"/> at the start of their application and the changes will apply to all <see cref="CustomControls.LoginPopup"/>s shown.</para>
        /// </summary>
        public static LoginPopupSettings DefaultLoginPopupSettings { get; set; } = new LoginPopupSettings();

        /// <summary>
        /// The <see cref="Models.LoginPopupSettings"/> used to display the <see cref="CustomControls.LoginPopup"/>.
        /// </summary>
        public LoginPopupSettings LoginPopupSettings
        {
            get => (LoginPopupSettings)GetValue(LoginPopupSettingsProperty);
            set => SetValue(LoginPopupSettingsProperty, value);
        }

        /// <summary>
        /// The login popup settings property.
        /// </summary>
        public static readonly BindableProperty LoginPopupSettingsProperty = BindableProperty.Create(nameof(LoginPopupSettings), typeof(LoginPopupSettings), typeof(LoginPopup), new LoginPopupSettings());

        /// <summary>
        /// Determines whether or not a <see cref="GetInputPopup"/> will be used for getting the username and password.
        /// </summary>
        public bool UseKeyboardPopups
        {
            get => (bool)GetValue(UseKeyboardPopupsProperty);
            set => SetValue(UseKeyboardPopupsProperty, value);
        }

        /// <summary>
        /// The use keyboard popups property.
        /// </summary>
        public static readonly BindableProperty UseKeyboardPopupsProperty = BindableProperty.Create(nameof(UseKeyboardPopups), typeof(bool), typeof(LoginPopup), true);

        public bool IsRFIDLogin
        {
            get => _isRFIDLogin;
            set
            {
                if (!SetProperty(ref _isRFIDLogin, value))
                {
                    return;
                }
                Username = string.Empty;
                StarPassword = string.Empty;
                RFID = string.Empty;
                SetFocusOnRFIDEntry(false);
            }
        }
        public string Username
        {
            get => _username;
            set
            {
                if (!SetProperty(ref _username, value))
                {
                    return;
                }
                UsernamePasswordLoginCommand.ChangeCanExecute();
            }
        }
        public string StarPassword
        {
            get => _starPassword;
            set
            {
                if (value == StarPassword)
                {
                    return;
                }
                _password = value;
                if (UseKeyboardPopups)
                {
                    var passwordString = string.Empty;
                    var minLength = 12;
                    var length = value.Length >= minLength || string.IsNullOrEmpty(value) ? value.Length : minLength;
                    for (int i = 0; i < length; i++)
                    {
                        passwordString += "*";
                    }
                    _starPassword = passwordString;
                }
                else
                {
                    _starPassword = _password;
                }
                OnPropertyChanged();
            }
        }
        public string RFID { get => _rfid; set => SetProperty(ref _rfid, value); }
        internal bool AuthenticationPassed { get; set; }
        #endregion

        #region Delegates
        public delegate Task<bool> AuthenticateUsernamePasswordHandler(string username, string password);
        public delegate Task<bool> AuthenticateRFIDHandler(int RFID);
        public delegate Task QuickAuthenticateHandler();
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new <see cref="LoginPopup"/>.
        /// <para>Note: Developers should use one of the <see cref="LoginPopup"/>'s static SHOW methods rather than creating a new instance of the control.</para>
        /// </summary>
        internal LoginPopup(AuthenticateRFIDHandler authenticateRFID, AuthenticateUsernamePasswordHandler authenticateUsernamePassword, QuickAuthenticateHandler quickAuthenticate)
        {
            _authenticateRFID = authenticateRFID ?? throw new ArgumentException("Parameter cannot be null", nameof(authenticateRFID));
            _authenticateUsernamePassword = authenticateUsernamePassword ?? throw new ArgumentException("Parameter cannot be null", nameof(authenticateUsernamePassword));
            _quickAuthenticate = quickAuthenticate;

            InitializeComponent();

            if (DefaultLoginPopupSettings != null)
            {
                LoginPopupSettings = DefaultLoginPopupSettings;
            }
            MonitorRFIDEntryFocus();
        }
        #endregion

        #region Methods
        #region Public
        /// <summary>
        /// Shows the <see cref="LoginPopup"/>.
        /// </summary>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which to push the <see cref="LoginPopup"/>.</param>
        /// <param name="authenticateRFID">The <see cref="AuthenticateRFIDHandler"/> used to authenticate with an RFID.</param>
        /// <param name="authenticateUsernamePassword">The <see cref="AuthenticateUsernamePasswordHandler"/> used to authenticate with a username and password.</param>
        /// <param name="quickAuthenticate">The optional <see cref="QuickAuthenticateHandler"/> used to quickly login a user. Note: This method of authentication is activated when the user clicks on the RFID icon.</param>
        /// <param name="useKeyboardPopups">Determines whether or not a <see cref="GetInputPopup"/> will be used for getting the username and password.</param>
        /// <returns></returns>
        public static async Task Show(NavigableElement navigableElement, AuthenticateRFIDHandler authenticateRFID, AuthenticateUsernamePasswordHandler authenticateUsernamePassword, QuickAuthenticateHandler quickAuthenticate = null, bool useKeyboardPopups = true)
        {
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                var popupPushed = false;
                LoginPopup popup = null;
                try
                {
                    popup = new LoginPopup(authenticateRFID, authenticateUsernamePassword, quickAuthenticate)
                    {
                        UseKeyboardPopups = useKeyboardPopups,
                    };
                    await navigableElement.OpenModalPopup(popup);
                    popupPushed = true;
                    while (!popup.AuthenticationPassed)
                    {
                        await Task.Delay(50);
                    }
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

        #region Private
        private async void EnterUsername(object sender, EventArgs e)
        {
            if (!UseKeyboardPopups)
            {
                return;
            }

            var result = await Application.Current.MainPage.ShowKeyboard("user-32.png", "Username", Username).ShowActivityIndicatorWhileTaskExecutes("Loading...", DefaultLoginPopupSettings.ActivityIndicatorSettings);
            if (result.UserCancelled)
            {
                return;
            }
            Username = result.Value.ToString();
        }

        private async void EnterPassword(object sender, EventArgs e)
        {
            if (!UseKeyboardPopups)
            {
                return;
            }

            //TODO: Add password icon
            var result = await Application.Current.MainPage.ShowKeyboard(label: "Password").ShowActivityIndicatorWhileTaskExecutes("Loading...", DefaultLoginPopupSettings.ActivityIndicatorSettings);
            if (result.UserCancelled)
            {
                return;
            }
            StarPassword = result.Value.ToString();
        }

        private void MonitorRFIDEntryFocus()
        {
            Task.Run(async () =>
            {
                await Device.InvokeOnMainThreadAsync(async () =>
                {
                    await SetFocusOnRFIDEntry(false);
                    while (Application.Current.MainPage.Navigation.ModalStack.Contains(this))
                    {
                        /* This loop monitors the navigation stack of the app's
                         * CustomMasterDetailPage. When another modal page is pushed
                         * on top of the login screen and then removed, focus is
                         * returned to the RFID entry.
                         */
                        if (Application.Current.MainPage.Navigation.ModalStack.Last() != this)
                        {
                            while (Application.Current.MainPage.Navigation.ModalStack.Last() != this)
                            {
                                await Task.Delay(50);
                            }

                            await SetFocusOnRFIDEntry(false);
                        }
                        await Task.Delay(50);
                    }
                });
            });
        }

        private async Task SetFocusOnRFIDEntry(bool invokeFocusOnMainThread = true)
        {
            if (!IsRFIDLogin)
            {
                return;
            }

            var wasFocused = false;
            while (!wasFocused)
            {
                await Task.Delay(50);
                if (invokeFocusOnMainThread)
                {
                    await Device.InvokeOnMainThreadAsync(() =>
                    {
                        wasFocused = RFIDEntry.Focus();
                    });
                }
                else
                {
                    wasFocused = RFIDEntry.Focus();
                }
            }
        }

        private async void RFIDLogin(object obj)
        {
            if (int.TryParse(RFID, out int UID))
            {
                AuthenticationPassed = await _authenticateRFID.Invoke(UID).ShowActivityIndicatorWhileTaskExecutes("Authenticating...", DefaultLoginPopupSettings.ActivityIndicatorSettings);
            }
            RFID = string.Empty;
            await SetFocusOnRFIDEntry(false);
        }

        private bool CanLoginWithUsernamePassword(object arg)
        {
            return !string.IsNullOrEmpty(Username);
        }

        private async void UsernamePasswordLogin(object obj)
        {
            AuthenticationPassed = await _authenticateUsernamePassword.Invoke(Username, _password).ShowActivityIndicatorWhileTaskExecutes("Authenticating...", DefaultLoginPopupSettings.ActivityIndicatorSettings);
            await SetFocusOnRFIDEntry(false);
        }

        private async void CoverGrid_Tapped(object sender, EventArgs e)
        {
            await SetFocusOnRFIDEntry(false);
        }

        private async void QuickLogin(object obj)
        {
            if (_quickAuthenticate == null)
            {
                return;
            }

            await _quickAuthenticate.Invoke().ShowActivityIndicatorWhileTaskExecutes("Authenticating...", DefaultLoginPopupSettings.ActivityIndicatorSettings);
            AuthenticationPassed = true;
        }
        #endregion
        #endregion
    }
}
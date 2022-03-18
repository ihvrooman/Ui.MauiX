using Components.Helpers;
using Ui.Enums;
using Ui.MauiX.Helpers;
using Ui.MauiX.Resources;
using Ui.MauiX.Test.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ui.MauiX.Test.ViewModels
{
    public class BaseViewModel : NotifyPropertyChanged
    {
        #region Fields
        private Xamarin.Forms.Command _showActivityIndicatorCommand;
        private bool _isEnabled;
        private bool _areIconsVisible = true;
        private Color _backgroundColor = CustomColors.BackgroundColor;
        private Color _disabledBackgroundColor = CustomColors.DisabledBackgroundColor;
        private Color _textColor = CustomColors.TextColor;
        private Color _disabledTextColor = CustomColors.DisabledTextColor;
        private Color _borderColor = CustomColors.BorderColor;
        private Color _disabledBorderColor = CustomColors.DisabledBorderColor;
        private double _borderThickness = 1;
        private int _fontSize = 14;
        private ObservableCollection<Type> _supplementalPageControlTypes = new ObservableCollection<Type>();
        private double _iconSize = 50;
        private CornerRadius _cornerRadius = new CornerRadius(0);
        private double _topLeftCornerRadius = 0;
        private double _topRightCornerRadius = 0;
        private double _bottomLeftCornerRadius = 0;
        private double _bottomRightCornerRadius = 0;
        private string _iconResourceId = "DeviceInfo.png";
        #endregion

        #region Properties
        public List<Color> AvailableColors { get; } = new List<Color>()
        {
            CustomColors.Red,
            CustomColors.Blue,
            CustomColors.DarkBlue,
            CustomColors.Green,
            CustomColors.Orange,
            CustomColors.TextColor,
            CustomColors.BackgroundColor,
            Color.Blue,
            Color.Red,
            Color.Green,
            Color.Purple,
            Color.Brown,
            Color.White,
            Color.Yellow,
            Color.Black,
            CustomColors.DisabledTextColor,
            CustomColors.DisabledBackgroundColor,
            CustomColors.LightDisabledBackgroundColor,
            CustomColors.BorderColor,
            CustomColors.DisabledBorderColor,
            CustomColors.LightDisabledBorderColor,
            CustomColors.SeparatorColor,
            CustomColors.BackgroundFadeColor,
        };

        public List<string> AvailableIconResourceIds { get; } = new List<string>()
        {
            "DeviceInfo.png",
            "MenuTest.png",
            "cancel-80.png",
            "circle-50-red.png",
            "database-50.png",
            "go-back-64.png",
        };

        public List<SelectionMode> AvailableSelectionModes { get; } = new List<SelectionMode>()
        {
            SelectionMode.Single,
            SelectionMode.Multiple,
            SelectionMode.None,
        };

        public ObservableCollection<string> Items { get; private set; } = new ObservableCollection<string>();

        public static BaseViewModel CustomMasterDetailPageInstance { get; } = new BaseViewModel()
        {
            BackgroundColor = Color.White,
            DisabledBackgroundColor = CustomColors.LightDisabledBackgroundColor,
        };

        public Xamarin.Forms.Command ShowActivityIndicatorCommand
        {
            get
            {
                if (_showActivityIndicatorCommand == null)
                {
                    _showActivityIndicatorCommand = new Xamarin.Forms.Command(ShowActivityIndicator, CanShowActivityIndicator);
                }
                return _showActivityIndicatorCommand;
            }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                SetProperty(ref _isEnabled, value);
                ShowActivityIndicatorCommand.ChangeCanExecute();
            }
        }

        public bool AreIconsVisible { get => _areIconsVisible; set => SetProperty(ref _areIconsVisible, value); }

        public Color BackgroundColor { get => _backgroundColor; set => SetProperty(ref _backgroundColor, value); }

        public Color DisabledBackgroundColor { get => _disabledBackgroundColor; set => SetProperty(ref _disabledBackgroundColor, value); }

        public Color TextColor { get => _textColor; set => SetProperty(ref _textColor, value); }

        public Color DisabledTextColor { get => _disabledTextColor; set => SetProperty(ref _disabledTextColor, value); }

        public Color BorderColor { get => _borderColor; set => SetProperty(ref _borderColor, value); }

        public Color DisabledBorderColor { get => _disabledBorderColor; set => SetProperty(ref _disabledBorderColor, value); }

        public double BorderThickness { get => _borderThickness; set => SetProperty(ref _borderThickness, value); }

        public int FontSize { get => _fontSize; set => SetProperty(ref _fontSize, value); }

        public ObservableCollection<Type> SupplementalPageControlTypes { get => _supplementalPageControlTypes; set => SetProperty(ref _supplementalPageControlTypes, value); }

        public double IconSize { get => _iconSize; set => SetProperty(ref _iconSize, value); }

        public CornerRadius CornerRadius { get => _cornerRadius; set => SetProperty(ref _cornerRadius, value); }

        public double TopLeftCornerRadius { get => _topLeftCornerRadius; set => SetProperty(ref _topLeftCornerRadius, value); }

        public double TopRightCornerRadius { get => _topRightCornerRadius; set => SetProperty(ref _topRightCornerRadius, value); }

        public double BottomLeftCornerRadius { get => _bottomLeftCornerRadius; set => SetProperty(ref _bottomLeftCornerRadius, value); }

        public double BottomRightCornerRadius{get => _bottomRightCornerRadius;set => SetProperty(ref _bottomRightCornerRadius, value); }

        public string IconResourceId { get => _iconResourceId; set => SetProperty(ref _iconResourceId, value); }
        #endregion

        #region Constructor
        public BaseViewModel()
        {
            SetCornerRadius();
            Task.Run(async () =>
            {
                await Task.Delay(250);
                IsEnabled = true;
            });

            Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    Items.Add(i.ToString());
                }
            });
        }
        #endregion

        #region Methods
        #region Internal
        internal void SetIndividualCornerRadiusProperties()
        {
            TopRightCornerRadius = CornerRadius.TopRight;
            TopLeftCornerRadius = CornerRadius.TopLeft;
            BottomRightCornerRadius = CornerRadius.BottomRight;
            BottomLeftCornerRadius = CornerRadius.BottomLeft;
        }

        internal void SetCornerRadius()
        {
            CornerRadius = new CornerRadius(TopLeftCornerRadius, TopRightCornerRadius, BottomLeftCornerRadius, BottomRightCornerRadius);
        }
        #endregion

        #region Private
        private bool CanShowActivityIndicator(object arg)
        {
            return IsEnabled;
        }

        private async void ShowActivityIndicator(object obj)
        {
            await Application.Current.MainPage.ShowActivityIndicatorWhileTaskExecutes(Task.Delay(2000), "Showing activity indicator popup...");
        }
        #endregion
        #endregion
    }
}

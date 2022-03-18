using Ui.Enums;
using Ui.MauiX.Helpers;
using Ui.MauiX.Resources;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToggleSwitch : ContentView
    {
        #region Fields
        private Color _darkDisabledColor = Color.FromRgb(163, 163, 163);
        #endregion

        #region Properties
        /// <summary>
        /// Indicates the state of the <see cref="ToggleSwitch"/>.
        /// </summary>
        public ToggleState State
        {
            get => (ToggleState)GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }

        /// <summary>
        /// The state property.
        /// </summary>
        public static readonly BindableProperty StateProperty = BindableProperty.Create(nameof(State), typeof(ToggleState), typeof(ToggleSwitch), ToggleState.Untoggled, propertyChanged: StateProperty_Changed, defaultBindingMode: BindingMode.TwoWay);

        /// <summary>
        /// The font size of the label.
        /// </summary>
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        /// The font size property.
        /// </summary>
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(ToggleSwitch), 14.0);

        /// <summary>
        /// The text to show when the control is toggled to <see cref="ToggleState.Toggled"/>.
        /// </summary>
        public string ToggledText
        {
            get => (string)GetValue(ToggledTextProperty);
            set => SetValue(ToggledTextProperty, value);
        }

        /// <summary>
        /// The toggled text property.
        /// </summary>
        public static readonly BindableProperty ToggledTextProperty = BindableProperty.Create(nameof(ToggledText), typeof(string), typeof(ToggleSwitch), "On");

        /// <summary>
        /// The text to show when the control is toggled to <see cref="ToggleState.Untoggled"/>.
        /// </summary>
        public string UntoggledText
        {
            get => (string)GetValue(UntoggledTextProperty);
            set => SetValue(UntoggledTextProperty, value);
        }

        /// <summary>
        /// The untoggled text property.
        /// </summary>
        public static readonly BindableProperty UntoggledTextProperty = BindableProperty.Create(nameof(UntoggledText), typeof(string), typeof(ToggleSwitch), "Off");

        /// <summary>
        /// The text to show when the control is toggled to <see cref="ToggleState.Indeterminate"/>.
        /// </summary>
        public string IndeterminateText
        {
            get => (string)GetValue(IndeterminateTextProperty);
            set => SetValue(IndeterminateTextProperty, value);
        }

        /// <summary>
        /// The indeterminate text property.
        /// </summary>
        public static readonly BindableProperty IndeterminateTextProperty = BindableProperty.Create(nameof(IndeterminateText), typeof(string), typeof(ToggleSwitch), "Unknown");

        /// <summary>
        /// The color used when toggled to <see cref="ToggleState.Untoggled"/> or <see cref="ToggleState.Indeterminate"/>.
        /// </summary>
        public Color UntoggledColor
        {
            get => (Color)GetValue(UntoggledColorProperty);
            set => SetValue(UntoggledColorProperty, value);
        }

        /// <summary>
        /// The untoggled color property.
        /// </summary>
        public static readonly BindableProperty UntoggledColorProperty = BindableProperty.Create(nameof(UntoggledColor), typeof(Color), typeof(ToggleSwitch), Color.White);

        /// <summary>
        /// The color used when toggled to <see cref="ToggleState.Toggled"/>.
        /// </summary>
        public Color ToggledColor
        {
            get => (Color)GetValue(ToggledColorProperty);
            set => SetValue(ToggledColorProperty, value);
        }

        /// <summary>
        /// The toggled color property.
        /// </summary>
        public static readonly BindableProperty ToggledColorProperty = BindableProperty.Create(nameof(ToggledColor), typeof(Color), typeof(ToggleSwitch), CustomColors.Blue);

        /// <summary>
        /// Determines whether or not the label is visible.
        /// </summary>
        public bool IsLabelVisible
        {
            get => (bool)GetValue(IsLabelVisibleProperty);
            set => SetValue(IsLabelVisibleProperty, value);
        }

        /// <summary>
        /// The is label visible property.
        /// </summary>
        public static readonly BindableProperty IsLabelVisibleProperty = BindableProperty.Create(nameof(IsLabelVisible), typeof(bool), typeof(ToggleSwitch), true);

        /// <summary>
        /// The text color of the label.
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// The text color property.
        /// </summary>
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(ToggleSwitch), Color.Black);

        /// <summary>
        /// The disabled text color of the label.
        /// </summary>
        public Color DisabledTextColor
        {
            get => (Color)GetValue(DisabledTextColorProperty);
            set => SetValue(DisabledTextColorProperty, value);
        }

        /// <summary>
        /// The disabled text color property.
        /// </summary>
        public static readonly BindableProperty DisabledTextColorProperty = BindableProperty.Create(nameof(DisabledTextColor), typeof(Color), typeof(ToggleSwitch), Color.FromRgb(163, 163, 163));

        /// <summary>
        /// Indicates whether or not the <see cref="ToggleSwitch"/> is enabled.
        /// </summary>
        public new bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        /// <summary>
        /// The is enabled property.
        /// </summary>
        public static readonly new BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(ToggleSwitch), true);

        /// <summary>
        /// Indicates whether or not the <see cref="ToggleSwitch"/> is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        /// <summary>
        /// The read only property.
        /// </summary>
        public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(ToggleSwitch), defaultValue: false);
        #endregion

        #region Events
        /// <summary>
        /// The toggled event raised when the <see cref="ToggleState"/> is changed by the user.
        /// </summary>
        public event EventHandler Toggled;
        #endregion

        #region Constructor
        public ToggleSwitch()
        {
            PropertyChanged += ToggleSwitch_PropertyChanged;
            InitializeComponent();
            UpdateLabelTextBinding();
            UpdateToggleBoxHorizantalOptions();
            UpdateToggleBoxColor();
        }
        #endregion

        #region Methods
        private static void StateProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var toggleSwitch = (ToggleSwitch)bindable;
            toggleSwitch.UpdateLabelTextBinding();
            toggleSwitch.UpdateToggleBoxHorizantalOptions();
            toggleSwitch.UpdateToggleBoxColor();
        }

        private void ToggleSwitch_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsEnabled))
            {
                UpdateToggleBoxColor();
            }
        }

        private void UpdateToggleBoxHorizantalOptions()
        {
            switch (State)
            {
                case ToggleState.Indeterminate:
                    ToggleBox.HorizontalOptions = LayoutOptions.Center;
                    break;
                case ToggleState.Toggled:
                    ToggleBox.HorizontalOptions = LayoutOptions.End;
                    break;
                case ToggleState.Untoggled:
                    ToggleBox.HorizontalOptions = LayoutOptions.Start;
                    break;
                default:
                    break;
            }
        }

        private void UpdateToggleBoxColor()
        {
            ToggleBox.RemoveBinding(BoxView.ColorProperty);
            if (IsEnabled)
            {
                if (State == ToggleState.Toggled)
                {
                    ToggleBox.SetBinding(BoxView.ColorProperty, nameof(UntoggledColor));
                }
                else
                {
                    ToggleBox.SetBinding(BoxView.ColorProperty, nameof(ToggledColor));
                }
            }
            else
            {
                ToggleBox.Color = _darkDisabledColor;
            }
        }

        private void UpdateLabelTextBinding()
        {
            Label.RemoveBinding(CustomLabel.TextProperty);
            switch (State)
            {
                case ToggleState.Indeterminate:
                    Label.SetBinding(CustomLabel.TextProperty, nameof(IndeterminateText));
                    break;
                case ToggleState.Toggled:
                    Label.SetBinding(CustomLabel.TextProperty, nameof(ToggledText));
                    break;
                case ToggleState.Untoggled:
                    Label.SetBinding(CustomLabel.TextProperty, nameof(UntoggledText));
                    break;
                default:
                    break;
            }
        }

        private void ToggleSwitch_Tapped(object sender, EventArgs e)
        {
            if (!IsEnabled || IsReadOnly)
            {
                return;
            }

            switch (State)
            {
                case ToggleState.Indeterminate:
                    State = ToggleState.Toggled;
                    RaiseToggledEvent();
                    break;
                case ToggleState.Toggled:
                    State = ToggleState.Untoggled;
                    RaiseToggledEvent();
                    break;
                case ToggleState.Untoggled:
                    State = ToggleState.Toggled;
                    RaiseToggledEvent();
                    break;
                default:
                    break;
            }
        }

        private void RaiseToggledEvent()
        {
            Toggled?.Invoke(this, new EventArgs());
        }
        #endregion
    }
}
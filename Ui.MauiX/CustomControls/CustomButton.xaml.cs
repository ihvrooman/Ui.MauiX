using Ui.MauiX.Resources;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    /// <summary>
    /// A custom button made for touch-based applications with a simple appearance.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomButton : CustomFrame
    {
        #region Properties
        /// <summary>
        /// The text to display in the button.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// The text property.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomButton));

        /// <summary>
        /// The button's text color.
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// The text color property.
        /// </summary>
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CustomButton), Color.White);

        /// <summary>
        /// The button's disabled text color.
        /// </summary>
        public Color DisabledTextColor
        {
            get => (Color)GetValue(DisabledTextColorProperty);
            set => SetValue(DisabledTextColorProperty, value);
        }

        /// <summary>
        /// The disabled text color property.
        /// </summary>
        public static readonly BindableProperty DisabledTextColorProperty = BindableProperty.Create(nameof(DisabledTextColor), typeof(Color), typeof(CustomButton), CustomColors.DisabledTextColor);

        /// <summary>
        /// The button's font size.
        /// </summary>
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        /// The font size property.
        /// </summary>
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CustomButton), 16.0);

        /// <summary>
        /// The button's <see cref="Command"/>.
        /// <para>Executed when the button is tapped or clicked.</para>
        /// </summary>
        public Command Command
        {
            get => (Command)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// The command property.
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(Command), typeof(CustomButton), propertyChanged: CommandProperty_Changed);

        /// <summary>
        /// The button's command parameter.
        /// </summary>
        public object CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// The command parameter property.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CustomButton));
        #endregion

        #region Events
        /// <summary>
        /// The event that is fired when the <see cref="CustomButton"/> is clicked.
        /// </summary>
        public event EventHandler Clicked;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new <see cref="CustomButton"/>.
        /// </summary>
        public CustomButton()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private static void CommandProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var customButton = (CustomButton)bindable;
            var oldCommand = (Command)oldValue;
            var newCommand = (Command)newValue;

            if (oldCommand != null)
            {
                oldCommand.CanExecuteChanged -= customButton.Command_CanExecuteChanged;
            }

            if (newCommand != null)
            {
                newCommand.CanExecuteChanged += customButton.Command_CanExecuteChanged;
                customButton.Command_CanExecuteChanged(newCommand, null);
            }
        }

        private void Command_CanExecuteChanged(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>IsEnabled = ((Command)sender).CanExecute(CommandParameter));
        }

        private void CustomButton_Tapped(object sender, EventArgs e)
        {
            if (!IsEnabled)
            {
                return;
            }

            Command?.Execute(CommandParameter);
            Clicked?.Invoke(this, new EventArgs());
        }
        #endregion
    }
}
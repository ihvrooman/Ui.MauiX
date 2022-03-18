
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomLabel : Label
    {
        #region Fields
        private string _textColorPath = "TextColor";
        private string _disabledTextColorPath = "DisabledTextColor";
        #endregion

        #region Properties
        /// <summary>
        /// Indicates whether or not the <see cref="CustomLabel"/> is enabled.
        /// </summary>
        public new bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        /// <summary>
        /// The is enabled property.
        /// </summary>
        public static readonly new BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(CustomLabel), true, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (CustomLabel)bindable;
            control.SetTextColorBinding();
        });

        /// <summary>
        /// The path used to bind to the text color.
        /// </summary>
        public string TextColorPath 
        { 
            get => _textColorPath;
            set
            {
                _textColorPath = value;
                SetTextColorBinding();
            }
        }

        /// <summary>
        /// The path used to bind to the disabled text color.
        /// </summary>
        public string DisabledTextColorPath
        {
            get => _disabledTextColorPath;
            set
            {
                _disabledTextColorPath = value;
                SetTextColorBinding();
            }
        }
        #endregion

        #region Constructor
        public CustomLabel()
        {
            InitializeComponent();
            SetTextColorBinding();
        }
        #endregion

        #region Methods
        private void SetTextColorBinding()
        {
            RemoveBinding(TextColorProperty);
            if (IsEnabled)
            {
                this.SetBinding(TextColorProperty, TextColorPath);
            }
            else
            {
                this.SetBinding(TextColorProperty, DisabledTextColorPath);
            }
        }
        #endregion
    }
}
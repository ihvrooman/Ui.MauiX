
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomExpander : CustomFrame
    {
        #region Fields
        private Command _toggleIsExpandedCommand;
        #endregion

        #region Properties
        /// <summary>
        /// Determines whether or not the <see cref="CustomExpander"/> is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }

        /// <summary>
        /// The is expanded property.
        /// </summary>
        public static readonly BindableProperty IsExpandedProperty = BindableProperty.Create(nameof(IsExpanded), typeof(bool), typeof(CustomExpander), false, defaultBindingMode: BindingMode.TwoWay);

        /// <summary>
        /// The <see cref="CustomExpander"/>'s header content.
        /// </summary>
        /// 
        public View Header
        {
            get => (View)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        /// <summary>
        /// The header property.
        /// </summary>
        public static readonly BindableProperty HeaderProperty = BindableProperty.Create(nameof(Header), typeof(View), typeof(CustomExpander));

        /// <summary>
        /// The background <see cref="Color"/> of the <see cref="CustomExpander"/>'s header.
        /// </summary>
        /// 
        public Color HeaderBackgroundColor
        {
            get => (Color)GetValue(HeaderBackgroundColorProperty);
            set => SetValue(HeaderBackgroundColorProperty, value);
        }

        /// <summary>
        /// The header background color property.
        /// </summary>
        public static readonly BindableProperty HeaderBackgroundColorProperty = BindableProperty.Create(nameof(HeaderBackgroundColor), typeof(Color), typeof(CustomExpander), defaultValue: Color.White);

        /// <summary>
        /// The background <see cref="Color"/> of the <see cref="CustomExpander"/>'s header when the control is disabled.
        /// </summary>
        /// 
        public Color DisabledHeaderBackgroundColor
        {
            get => (Color)GetValue(DisabledHeaderBackgroundColorProperty);
            set => SetValue(DisabledHeaderBackgroundColorProperty, value);
        }

        /// <summary>
        /// The disabled header background color property.
        /// </summary>
        public static readonly BindableProperty DisabledHeaderBackgroundColorProperty = BindableProperty.Create(nameof(DisabledHeaderBackgroundColor), typeof(Color), typeof(CustomExpander), defaultValue: Color.FromRgb(220, 220, 220));

        /// <summary>
        /// The <see cref="Command"/> for expanding and collapsing the <see cref="CustomExpander"/>.
        /// </summary>
        public Command ToggleIsExpandedCommand
        {
            get
            {
                if (_toggleIsExpandedCommand == null)
                {
                    _toggleIsExpandedCommand = new Command(ToggleIsExpanded);
                }
                return _toggleIsExpandedCommand;
            }
        }

        /// <summary>
        /// The size of the arrow icon.
        /// </summary>
        public double ArrowIconSize
        {
            get => (double)GetValue(ArrowIconSizeProperty);
            set => SetValue(ArrowIconSizeProperty, value);
        }

        /// <summary>
        /// The arrow icon size property.
        /// </summary>
        public static readonly BindableProperty ArrowIconSizeProperty = BindableProperty.Create(nameof(ArrowIconSize), typeof(double), typeof(CustomExpander), defaultValue: 25.0);
        #endregion

        #region Constructor
        public CustomExpander()
        {
            PropertyChanged += CustomExpander_PropertyChanged;
            InitializeComponent();
            SetColorBindings();
        }
        #endregion

        #region Methods
        private void CustomExpander_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsEnabled))
            {
                SetColorBindings();
            }
        }

        private void ToggleIsExpanded(object obj)
        {
            if (!IsEnabled)
            {
                return;
            }

            IsExpanded = !IsExpanded;
        }

        private void SetColorBindings()
        {
            var header = (Grid)GetTemplateChild("Header");
            header?.RemoveBinding(BackgroundColorProperty);
            if (header != null)
            {
                header.BindingContext = this;
            }

            if (IsEnabled)
            {
                header?.SetBinding(BackgroundColorProperty, "HeaderBackgroundColor");
            }
            else
            {
                header?.SetBinding(BackgroundColorProperty, "DisabledHeaderBackgroundColor");
            }
        }
        #endregion
    }
}
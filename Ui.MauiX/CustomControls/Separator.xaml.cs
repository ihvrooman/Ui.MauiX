
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ui.Enums;

namespace Ui.MauiX.CustomControls
{
    /// <summary>
    /// A separator.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Separator : BoxView
    {
        #region Properties
        /// <summary>
        /// The <see cref="Ui.Enums.Orientation"/> of the <see cref="Separator"/>.
        /// </summary>
        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        /// <summary>
        /// The orientation property.
        /// </summary>
        public static readonly BindableProperty OrientationProperty = BindableProperty.Create(nameof(Orientation), typeof(Orientation), typeof(Separator), Orientation.Horizontal, propertyChanged: OrientationProperty_Changed);

        /// <summary>
        /// The thickness of the <see cref="Separator"/>.
        /// </summary>
        public int Thickness
        {
            get { return (int)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        public static readonly BindableProperty ThicknessProperty = BindableProperty.Create(nameof(Thickness), typeof(int), typeof(Separator), 1, propertyChanged: ThicknessProperty_Changed);
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new <see cref="Separator"/>.
        /// </summary>
        public Separator()
        {
            InitializeComponent();
            AdjustLayout();
        }
        #endregion

        #region Methods
        private void AdjustLayout()
        {
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    HeightRequest = Thickness;
                    VerticalOptions = LayoutOptions.CenterAndExpand;
                    HorizontalOptions = LayoutOptions.FillAndExpand;
                    break;
                case Orientation.Vertical:
                    WidthRequest = Thickness;
                    VerticalOptions = LayoutOptions.FillAndExpand;
                    HorizontalOptions = LayoutOptions.CenterAndExpand;
                    break;
                default:
                    break;
            }
        }

        private static void ThicknessProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var separator = (Separator)bindable;
            separator.AdjustLayout();
        }

        private static void OrientationProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var separator = (Separator)bindable;
            separator.AdjustLayout();
        }
        #endregion
    }
}
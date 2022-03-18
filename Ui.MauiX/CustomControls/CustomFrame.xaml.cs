using Ui.MauiX.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomFrame : ContentViewWithSetPropertyMethod
    {
        #region Fields
        private CornerRadius _internalCornerRadius = new CornerRadius(0);
        #endregion

        #region Properties
        /// <summary>
        /// The <see cref="CustomFrame"/>'s background color.
        /// </summary>
        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        /// <summary>
        /// The background color property.
        /// </summary>
        public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(CustomFrame), Color.White);

        /// <summary>
        /// The <see cref="CustomFrame"/>'s disabled background color.
        /// </summary>
        public Color DisabledBackgroundColor
        {
            get => (Color)GetValue(DisabledBackgroundColorProperty);
            set => SetValue(DisabledBackgroundColorProperty, value);
        }

        /// <summary>
        /// The disabled background color property.
        /// </summary>
        public static readonly BindableProperty DisabledBackgroundColorProperty = BindableProperty.Create(nameof(DisabledBackgroundColor), typeof(Color), typeof(CustomFrame), CustomColors.DisabledBackgroundColor);

        /// <summary>
        /// The <see cref="CustomFrame"/>'s border color.
        /// </summary>
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        /// <summary>
        /// The border color property.
        /// </summary>
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CustomFrame), CustomColors.BorderColor);

        /// <summary>
        /// The <see cref="CustomFrame"/>'s disabled border color.
        /// </summary>
        public Color DisabledBorderColor
        {
            get => (Color)GetValue(DisabledBorderColorProperty);
            set => SetValue(DisabledBorderColorProperty, value);
        }

        /// <summary>
        /// The disabled border color property.
        /// </summary>
        public static readonly BindableProperty DisabledBorderColorProperty = BindableProperty.Create(nameof(DisabledBorderColor), typeof(Color), typeof(CustomFrame), CustomColors.DisabledBorderColor);

        /// <summary>
        /// The thickness of the <see cref="CustomFrame"/>'s border.
        /// </summary>
        public double BorderThickness
        {
            get => (double)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        /// <summary>
        /// The border thickness property.
        /// </summary>
        public static readonly BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(double), typeof(CustomFrame), 1.0, propertyChanged: BorderThicknessProperty_Changed);

        /// <summary>
        /// The <see cref="CustomFrame"/>'s padding.
        /// </summary>
        public new Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        /// <summary>
        /// The padding property.
        /// </summary>
        public static readonly new BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(CustomFrame), new Thickness(0));

        /// <summary>
        /// The <see cref="CustomFrame"/>'s corner radius.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// The corner radius property.
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(CustomFrame), new CornerRadius(0), propertyChanged: CornerRadiusProperty_Changed);

        /// <summary>
        /// The corner radius used internally by the <see cref="CustomFrame"/>.
        /// </summary>
        public CornerRadius InternalCornerRadius { get => _internalCornerRadius; private set => SetProperty(ref _internalCornerRadius, value); }

        /// <summary>
        /// Indicates whether or not the <see cref="CustomFrame"/> is enabled.
        /// </summary>
        public new bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        /// <summary>
        /// The is enabled property.
        /// </summary>
        public static readonly new BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(CustomFrame), true, propertyChanged: IsEnabledProperty_Changed);
        #endregion

        #region Constructor
        public CustomFrame()
        {
            InitializeComponent();
            SetColorBindings();
        }
        #endregion

        #region Methods
        private static void CornerRadiusProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var customFrame = (CustomFrame)bindable;
            customFrame.SetInternalCornerRadius();
        }

        private static void BorderThicknessProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var customFrame = (CustomFrame)bindable;
            customFrame.SetInternalCornerRadius();
        }

        private static void IsEnabledProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var customFrame = (CustomFrame)bindable;
            customFrame.SetColorBindings();
        }

        private void SetColorBindings()
        {
            var border = (BoxView)GetTemplateChild("Border");
            var background = (BoxView)GetTemplateChild("Background");
            border?.RemoveBinding(BoxView.ColorProperty);
            background?.RemoveBinding(BoxView.ColorProperty);
            if (border != null)
            {
                border.BindingContext = this;
            }
            if (background != null)
            {
                background.BindingContext = this;
            }

            if (IsEnabled)
            {
                border?.SetBinding(BoxView.ColorProperty, nameof(BorderColor));
                background?.SetBinding(BoxView.ColorProperty, nameof(BackgroundColor));
            }
            else
            {
                border?.SetBinding(BoxView.ColorProperty, nameof(DisabledBorderColor));
                background?.SetBinding(BoxView.ColorProperty, nameof(DisabledBackgroundColor));
            }
        }

        private void SetInternalCornerRadius()
        {
            var topLeft = CornerRadius.TopLeft - BorderThickness;
            var topRight = CornerRadius.TopRight - BorderThickness;
            var bottomLeft = CornerRadius.BottomLeft - BorderThickness;
            var bottomRight = CornerRadius.BottomRight - BorderThickness;

            if (topLeft < 0)
            {
                topLeft = 0;
            }
            if (topRight < 0)
            {
                topRight = 0;
            }
            if (bottomLeft < 0)
            {
                bottomLeft = 0;
            }
            if (bottomRight < 0)
            {
                bottomRight = 0;
            }
            InternalCornerRadius = new CornerRadius(topLeft, topRight, bottomLeft, bottomRight);
        }
        #endregion
    }
}
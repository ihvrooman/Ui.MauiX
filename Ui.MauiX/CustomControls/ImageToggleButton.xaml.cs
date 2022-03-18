using Ui.MauiX.Converters;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ui.Enums;
using Ui.MauiX.Resources;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageToggleButton : BaseToggleControl
    {
        #region Properties
        /// <summary>
        /// The resource id of the image shown when the <see cref="State"/> is <see cref="ToggleState.Toggled"/>.
        /// </summary>
        public string ToggledImageResourceId
        {
            get => (string)GetValue(ToggledImageResourceIdProperty);
            set => SetValue(ToggledImageResourceIdProperty, value);
        }

        /// <summary>
        /// The toggled image resource id property.
        /// </summary>
        public static readonly BindableProperty ToggledImageResourceIdProperty = BindableProperty.Create(nameof(ToggledImageResourceId), typeof(string), typeof(ImageToggleButton), defaultValue: string.Empty);

        /// <summary>
        /// The resource id of the image shown when the <see cref="State"/> is <see cref="ToggleState.Untoggled"/>.
        /// </summary>
        public string UntoggledImageResourceId
        {
            get => (string)GetValue(UntoggledImageResourceIdProperty);
            set => SetValue(UntoggledImageResourceIdProperty, value);
        }

        /// <summary>
        /// The untoggled image resource id property.
        /// </summary>
        public static readonly BindableProperty UntoggledImageResourceIdProperty = BindableProperty.Create(nameof(UntoggledImageResourceId), typeof(string), typeof(ImageToggleButton), defaultValue: string.Empty);

        /// <summary>
        /// The resource id of the image shown when the <see cref="State"/> is <see cref="ToggleState.Indeterminate"/>.
        /// </summary>
        public string IndeterminateImageResourceId
        {
            get => (string)GetValue(IndeterminateImageResourceIdProperty);
            set => SetValue(IndeterminateImageResourceIdProperty, value);
        }

        /// <summary>
        /// The indeterminate image resource id property.
        /// </summary>
        public static readonly BindableProperty IndeterminateImageResourceIdProperty = BindableProperty.Create(nameof(IndeterminateImageResourceId), typeof(string), typeof(ImageToggleButton), defaultValue: string.Empty);

        /// <summary>
        /// The size of the image.
        /// </summary>
        public double ImageSize
        {
            get => (double)GetValue(ImageSizeProperty);
            set => SetValue(ImageSizeProperty, value);
        }

        /// <summary>
        /// The image size property.
        /// </summary>
        public static readonly BindableProperty ImageSizeProperty = BindableProperty.Create(nameof(ImageSize), typeof(double), typeof(ImageToggleButton), defaultValue: 30.0);

        /// <summary>
        /// The image margin.
        /// </summary>
        public Thickness ImageMargin
        {
            get => (Thickness)GetValue(ImageMarginProperty);
            set => SetValue(ImageMarginProperty, value);
        }

        /// <summary>
        /// The image margin property.
        /// </summary>
        public static readonly BindableProperty ImageMarginProperty = BindableProperty.Create(nameof(ImageMargin), typeof(Thickness), typeof(ImageToggleButton), defaultValue: new Thickness(10));
        #endregion

        #region Events
        /// <summary>
        /// The toggled event raised when the <see cref="ToggleState"/> is changed by the user.
        /// </summary>
        public event EventHandler Toggled;
        #endregion

        #region Constructor
        public ImageToggleButton()
        {
            BorderThickness = 0;
            InitializeComponent();
            SetImageBindings();
            PropertyChanged += ImageToggleButton_PropertyChanged;            
        }
        #endregion

        #region Methods
        private void ImageToggleButton_Tapped(object sender, EventArgs e)
        {
            if (!IsEnabled)
            {
                return;
            }

            switch (State)
            {
                case ToggleState.Untoggled:
                    State = ToggleState.Toggled;
                    RaiseToggledEvent();
                    break;
                case ToggleState.Toggled:
                    State = ToggleState.Untoggled;
                    RaiseToggledEvent();
                    break;
                case ToggleState.Indeterminate:
                    State = ToggleState.Toggled;
                    RaiseToggledEvent();
                    break;
                default:
                    break;
            }
        }

        private void SetImageBindings()
        {
            Image.RemoveBinding(Image.SourceProperty);
            var bindingPath = string.Empty;
            switch (State)
            {
                case ToggleState.Untoggled:
                    bindingPath = nameof(UntoggledImageResourceId);
                    break;
                case ToggleState.Toggled:
                    bindingPath = nameof(ToggledImageResourceId);
                    break;
                case ToggleState.Indeterminate:
                    bindingPath = nameof(IndeterminateImageResourceId);
                    break;
                default:
                    break;
            }
            var binding = new Binding(bindingPath, converter: new ImageSourceFromStringConverter());
            Image.SetBinding(Image.SourceProperty, binding);
        }

        private void RaiseToggledEvent()
        {
            Toggled?.Invoke(this, new EventArgs());
        }

        private void ImageToggleButton_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(State))
            {
                SetImageBindings();
            }
        }
        #endregion
    }
}
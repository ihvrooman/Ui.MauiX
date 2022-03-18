using Ui.Enums;
using Ui.MauiX.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Ui.MauiX.CustomControls
{
    public class BaseToggleControl : CustomFrame
    {
        #region Properties
        /// <summary>
        /// Indicates the state of the <see cref="BaseToggleControl"/>.
        /// </summary>
        public ToggleState State
        {
            get => (ToggleState)GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }

        /// <summary>
        /// The state property.
        /// </summary>
        public static readonly BindableProperty StateProperty = BindableProperty.Create(nameof(State), typeof(ToggleState), typeof(BaseToggleControl), ToggleState.Untoggled, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var button = (BaseToggleControl)bindable;
            button.SetColorBindings();
        });

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
        public static readonly BindableProperty UntoggledColorProperty = BindableProperty.Create(nameof(UntoggledColor), typeof(Color), typeof(BaseToggleControl), Color.Transparent);

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
        public static readonly BindableProperty ToggledColorProperty = BindableProperty.Create(nameof(ToggledColor), typeof(Color), typeof(BaseToggleControl), Color.Transparent);
        #endregion

        #region Constructor
        public BaseToggleControl()
        {
            SetColorBindings();
        }
        #endregion

        #region Methods
        private void SetColorBindings()
        {
            var path = nameof(UntoggledColor);
            RemoveBinding(BackgroundColorProperty);
            if (State == ToggleState.Toggled)
            {
                path = nameof(ToggledColor);
            }
            SetBinding(BackgroundColorProperty, new Binding(path) { Source = this });
        }
        #endregion
    }
}
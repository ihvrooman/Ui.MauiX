using Ui.Enums;
using Ui.MauiX.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Ui.MauiX.CustomControls
{
    /// <summary>
    /// A custom label used in a toggle control. This class provides properties and bindings for changing the appearance of the label when the <see cref="ToggleState"/> changes.
    /// </summary>
    public class ToggleLabel : CustomLabel
    {
        #region Properties
        /// <summary>
        /// Indicates the state of the <see cref="ToggleLabel"/>.
        /// </summary>
        public ToggleState State
        {
            get => (ToggleState)GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }

        /// <summary>
        /// The state property.
        /// </summary>
        public static readonly BindableProperty StateProperty = BindableProperty.Create(nameof(State), typeof(ToggleState), typeof(ToggleLabel), ToggleState.Untoggled, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var label = (ToggleLabel)bindable;
            label.SetColorBindings();
        });

        /// <summary>
        /// The text color used when toggled to <see cref="ToggleState.Untoggled"/> or <see cref="ToggleState.Indeterminate"/>.
        /// </summary>
        public Color UntoggledTextColor
        {
            get => (Color)GetValue(UntoggledTextColorProperty);
            set => SetValue(UntoggledTextColorProperty, value);
        }

        /// <summary>
        /// The untoggled text color property.
        /// </summary>
        public static readonly BindableProperty UntoggledTextColorProperty = BindableProperty.Create(nameof(UntoggledTextColor), typeof(Color), typeof(ToggleLabel), Color.Black);

        /// <summary>
        /// The text color used when disabled.
        /// </summary>
        public Color DisabledTextColor
        {
            get => (Color)GetValue(DisabledTextColorProperty);
            set => SetValue(DisabledTextColorProperty, value);
        }

        /// <summary>
        /// The disabled text color property.
        /// </summary>
        public static readonly BindableProperty DisabledTextColorProperty = BindableProperty.Create(nameof(DisabledTextColor), typeof(Color), typeof(ToggleLabel), CustomColors.DisabledTextColor);

        /// <summary>
        /// The text color used when toggled to <see cref="ToggleState.Toggled"/>.
        /// </summary>
        public Color ToggledTextColor
        {
            get => (Color)GetValue(ToggledTextColorProperty);
            set => SetValue(ToggledTextColorProperty, value);
        }

        /// <summary>
        /// The toggled text color property.
        /// </summary>
        public static readonly BindableProperty ToggledTextColorProperty = BindableProperty.Create(nameof(ToggledTextColor), typeof(Color), typeof(ToggleLabel), Color.White);

        /// <summary>
        /// Indicates whether or not the <see cref="ToggleLabel"/> is enabled.
        /// </summary>
        public new bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        /// <summary>
        /// The is enabled property.
        /// </summary>
        public static readonly new BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(ToggleLabel), true, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (ToggleLabel)bindable;
            control.SetColorBindings();
        });
        #endregion

        #region Constructor
        public ToggleLabel()
        {
            SetColorBindings();
            this.SetBinding(FontSizeProperty, nameof(FontSize));
        }
        #endregion

        #region Methods
        private void SetColorBindings()
        {
            var path = nameof(UntoggledTextColor);
            RemoveBinding(TextColorProperty);
            if (!IsEnabled)
            {
                path = nameof(DisabledTextColor);
            }
            else if (State == ToggleState.Toggled)
            {
                path = nameof(ToggledTextColor);
            }
            SetBinding(TextColorProperty, new Binding(path) { Source = this });
        }
        #endregion
    }
}
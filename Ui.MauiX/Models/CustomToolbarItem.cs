using System;
using System.Collections.Generic;
using System.Text;
using Ui.MauiX.CustomControls;
using Ui.Helpers;
using Xamarin.Forms;

namespace Ui.MauiX.Models
{
    /// <summary>
    /// A custom implementation of the <see cref="ToolbarItem"/> for use in a <see cref="CustomContentView"/> within a <see cref="CustomMasterDetailPage"/>.
    /// </summary>
    public class CustomToolbarItem : BindableObject
    {
        #region Fields
        private ToolbarItemOrder _order = ToolbarItemOrder.Default;
        private int _priority;
        #endregion

        #region Properties
        /// <summary>
        /// The command associated with the item.
        /// </summary>
        public Command Command
        {
            get => (Command)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// The command property.
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(Command), typeof(CustomToolbarItem), propertyChanging: (bindable, oldValue, newValue) =>
        {
            var item = (CustomToolbarItem)bindable;
            if (oldValue is Command oldCommand)
            {
                oldCommand.CanExecuteChanged -= item.CommandCanExecute_Changed;
            }
            if (newValue is Command newCommand)
            {
                newCommand.CanExecuteChanged += item.CommandCanExecute_Changed;
                if (newCommand == null)
                {
                    return;
                }
                item.IsEnabled = newCommand.CanExecute(item.CommandParameter);
            }
        });

        /// <summary>
        /// The command parameter.
        /// </summary>
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// The command parameter property.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CustomToolbarItem));

        /// <summary>
        /// Determines whether or not the <see cref="CustomToolbarItem"/> is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        /// <summary>
        /// The is enabled property.
        /// </summary>
        public static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(CustomToolbarItem), true);

        /// <summary>
        /// The toolbar item order.
        /// </summary>
        public ToolbarItemOrder Order 
        { 
            get => _order;
            set
            {
                _order = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The toolbar item priority.
        /// </summary>
        public int Priority
        {
            get => _priority;
            set
            {
                _priority = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The icon image resource id.
        /// </summary>
        public string IconImageResourceId
        {
            get => (string)GetValue(IconImageResourceIdProperty);
            set => SetValue(IconImageResourceIdProperty, value);
        }

        /// <summary>
        /// The icon image resource id property.
        /// </summary>
        public static readonly BindableProperty IconImageResourceIdProperty = BindableProperty.Create(nameof(IconImageResourceId), typeof(string), typeof(CustomToolbarItem));

        /// <summary>
        /// The item text.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// The text property.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomToolbarItem));
        #endregion

        #region Methods
        private void CommandCanExecute_Changed(object sender, EventArgs e)
        {
            if (Command == null)
            {
                return;
            }

            IsEnabled = Command.CanExecute(CommandParameter);
        }
        #endregion
    }
}

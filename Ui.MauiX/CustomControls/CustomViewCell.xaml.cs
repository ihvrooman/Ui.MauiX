using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomViewCell : ContentView
    {
        #region Properties
        /// <summary>
        /// Indicates whether or not the item which this view represents is selected.
        /// </summary>
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        /// <summary>
        /// The is selected property.
        /// </summary>
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(CustomViewCell), defaultBindingMode: BindingMode.TwoWay);

        /// <summary>
        /// Indicates whether or not the item which this view represents is enabled.
        /// </summary>
        public new bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        /// <summary>
        /// The is enabled property.
        /// </summary>
        public static readonly new BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(CustomViewCell), defaultBindingMode: BindingMode.TwoWay, defaultValue: true);
        #endregion

        #region Constructor
        public CustomViewCell()
        {
            var t = new TapGestureRecognizer()
            {
                CommandParameter = this
            };
            t.SetBinding(TapGestureRecognizer.CommandProperty, new Binding(nameof(SelectionControl.SelectItemCommand)) { Source = new RelativeBindingSource(RelativeBindingSourceMode.FindAncestor, typeof(SelectionControl)) });
            GestureRecognizers.Add(t);
        }
        #endregion
    }
}
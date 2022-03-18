using Ui.MauiX.Models;
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
    public partial class MenuItem : CustomFrame
    {
        #region Properties
        /// <summary>
        /// The <see cref="MauiX.Models.MenuSettings"/>.
        /// </summary>
        public MenuSettings MenuSettings
        {
            get => (MenuSettings)GetValue(MenuSettingsProperty);
            set => SetValue(MenuSettingsProperty, value);
        }

        /// <summary>
        /// The menu settings property.
        /// </summary>
        public static readonly BindableProperty MenuSettingsProperty = BindableProperty.Create(nameof(MenuSettings), typeof(MenuSettings), typeof(MenuItem), new MenuSettings());

        /// <summary>
        /// Indicates whether or not the <see cref="MenuItem"/> is selected.
        /// </summary>
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        /// <summary>
        /// The is selected property.
        /// </summary>
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(MenuItem), false);

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
        public static readonly BindableProperty IconImageResourceIdProperty = BindableProperty.Create(nameof(IconImageResourceId), typeof(string), typeof(MenuItem));

        /// <summary>
        /// The menu item text.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// The text property.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MenuItem));
        #endregion

        #region Constructor
        public MenuItem()
        {
            InitializeComponent();
        }
        #endregion
    }
}
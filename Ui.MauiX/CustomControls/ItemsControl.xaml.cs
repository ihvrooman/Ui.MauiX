using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    /// <summary>
    /// A control used to create a dynamic collection of visual elements from an itemsSource based on an itemTemplate.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsControl : ContentView
    {
        #region Properties
        /// <summary>
        /// The source of the items to template and display.
        /// </summary>
        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// The items source property.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(ItemsControl));


        /// <summary>
        /// The <see cref="DataTemplate"/> used to render items.
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        /// <summary>
        /// The item template property.
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(ItemsControl));

        /// <summary>
        /// The <see cref="DataTemplateSelector"/> used to render items.
        /// <para>Note: This property is only used when the <see cref="ItemTemplate"/> property is not set.</para>
        /// </summary>
        public DataTemplateSelector ItemTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty);
            set => SetValue(ItemTemplateSelectorProperty, value);
        }

        /// <summary>
        /// The item template selector property.
        /// </summary>
        public static readonly BindableProperty ItemTemplateSelectorProperty = BindableProperty.Create(nameof(ItemTemplateSelector), typeof(DataTemplateSelector), typeof(ItemsControl), propertyChanging: ItemTemplateSelectorProperty_Changed);

        /// <summary>
        /// The <see cref="StackOrientation"/> used when displaying the items.
        /// </summary>
        public StackOrientation Orientation
        {
            get => (StackOrientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        /// <summary>
        /// The orientation property.
        /// </summary>
        public static readonly BindableProperty OrientationProperty = BindableProperty.Create(nameof(Orientation), typeof(StackOrientation), typeof(ItemsControl), StackOrientation.Vertical);

        /// <summary>
        /// The spacing between items.
        /// </summary>
        public double Spacing
        {
            get => (double)GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }

        /// <summary>
        /// The spacing property.
        /// </summary>
        public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing), typeof(double), typeof(ItemsControl), 0.0);

        /// <summary>
        /// The visual items shown in the <see cref="ItemsControl"/>.
        /// </summary>
        public IList<View> VisualItems { get => Stack_Layout.Children; }
        #endregion

        #region Constructor
        public ItemsControl()
        {
            InitializeComponent();
            SetDefaultItemTemplate();
        }
        #endregion

        #region Methods
        #region Property changed
        private static void ItemTemplateSelectorProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsControl = (ItemsControl)bindable;
            if (newValue != null)
            {
                /* If the user is setting an itemTemplateSelector, then remove
                 * the item template so that the user's itemTemplateSelector 
                 * will be used.
                 */
                itemsControl.ItemTemplate = null;
            }
            else if (newValue == null && itemsControl.ItemTemplate == null)
            {
                /* If the user is removing an itemTemplateSelector and the
                 * item template is null, set the itemTemplate to the default
                 * data template.
                 */
                itemsControl.SetDefaultItemTemplate();
            }
        }
        #endregion

        private void SetDefaultItemTemplate()
        {
            ItemTemplate = (DataTemplate)Resources["ItemsControlDefaultItemTemplate"];
        }
        #endregion
    }
}
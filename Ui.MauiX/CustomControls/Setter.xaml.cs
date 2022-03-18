using System;
using System.Collections;
using Ui.Models;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ui.Enums;
using Ui.MauiX.Resources;
using Ui.MauiX.Helpers;
using System.Linq;
using System.Threading.Tasks;
using Ui.MauiX.Models;
using DataCollections.Models;
using Ui.MauiX.Converters;

namespace Ui.MauiX.CustomControls
{
    /// <summary>
    /// A control for setting a value.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Setter : CustomFrame
    {
        #region Fields
        private bool _isRequiredErrorMessageVisible;
        #endregion

        #region Properties
        /// <summary>
        /// The resource id of the icon.
        /// </summary>
        public string IconResourceId
        {
            get => (string)GetValue(IconResourceIdProperty);
            set => SetValue(IconResourceIdProperty, value);
        }

        /// <summary>
        /// The icon resource id property.
        /// </summary>
        public static readonly BindableProperty IconResourceIdProperty = BindableProperty.Create(nameof(IconResourceId), typeof(string), typeof(Setter), defaultValue: string.Empty);

        /// <summary>
        /// The size of the icon.
        /// </summary>
        public double IconSize
        {
            get => (double)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }

        /// <summary>
        /// The icon size property.
        /// </summary>
        public static readonly BindableProperty IconSizeProperty = BindableProperty.Create(nameof(IconSize), typeof(double), typeof(Setter), defaultValue: 50.0);

        /// <summary>
        /// The <see cref="Setter"/>'s <see cref="Ui.Enums.InputMode"/>.
        /// </summary>
        public InputMode InputMode
        {
            get => (InputMode)GetValue(InputModeProperty);
            set => SetValue(InputModeProperty, value);
        }

        /// <summary>
        /// The <see cref="Ui.Enums.InputMode"/> property.
        /// </summary>
        public static readonly BindableProperty InputModeProperty = BindableProperty.Create(nameof(InputMode), typeof(InputMode), typeof(Setter), defaultValue: InputMode.AlphaNumberic, propertyChanged: InputModeProperty_Changed);

        /// <summary>
        /// The label which describes what value the <see cref="Setter"/> is setting.
        /// </summary>
        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        /// <summary>
        /// The label property.
        /// </summary>
        public static readonly BindableProperty LabelProperty = BindableProperty.Create(nameof(Label), typeof(string), typeof(Setter));

        /// <summary>
        /// The value which is modified by the <see cref="Setter"/>.
        /// </summary>
        public object Value
        {
            get => (object)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        /// <summary>
        /// The value property.
        /// </summary>
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(object), typeof(Setter), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
        {
            ((Setter)bindable).UpdateIsRequiredErrorMessageVisible();
        });

        /// <summary>
        /// The <see cref="Setter"/>'s font size.
        /// </summary>
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        /// The font size property.
        /// </summary>
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(Setter), defaultValue: 14.0);

        /// <summary>
        /// The <see cref="Setter"/>'s height request.
        /// </summary>
        public new double HeightRequest
        {
            get => (double)GetValue(HeightRequestProperty);
            set => SetValue(HeightRequestProperty, value);
        }

        /// <summary>
        /// The height request property.
        /// </summary>
        public static readonly new BindableProperty HeightRequestProperty = BindableProperty.Create(nameof(HeightRequest), typeof(double), typeof(Setter), defaultValue: 70.0);

        /// <summary>
        /// The suffix of the value.
        /// <para>The unit of measure would be a common value for this property.</para>
        /// </summary>
        public string ValueSuffix
        {
            get => (string)GetValue(ValueSuffixProperty);
            set => SetValue(ValueSuffixProperty, value);
        }

        /// <summary>
        /// The value suffix property.
        /// </summary>
        public static readonly BindableProperty ValueSuffixProperty = BindableProperty.Create(nameof(ValueSuffix), typeof(string), typeof(Setter));

        /// <summary>
        /// The value shown when the <see cref="Setter"/>'s <see cref="Ui.Enums.ToggleState"/> is <see cref="Ui.Enums.ToggleState.Toggled"/>.
        /// <para>Note: This value is only used when the <see cref="InputMode"/> is set to <see cref="Ui.Enums.InputMode.Toggle"/>.</para>
        /// </summary>
        public string ToggledValueText
        {
            get => (string)GetValue(ToggledValueTextProperty);
            set => SetValue(ToggledValueTextProperty, value);
        }

        /// <summary>
        /// The toggled value text property.
        /// </summary>
        public static readonly BindableProperty ToggledValueTextProperty = BindableProperty.Create(nameof(ToggledValueText), typeof(string), typeof(Setter), defaultValue: "On");

        /// <summary>
        /// The value shown when the <see cref="Setter"/>'s <see cref="Ui.Enums.ToggleState"/> is <see cref="Ui.Enums.ToggleState.Untoggled"/>.
        /// <para>Note: This value is only used when the <see cref="InputMode"/> is set to <see cref="Ui.Enums.InputMode.Toggle"/>.</para>
        /// </summary>
        public string UntoggledValueText
        {
            get => (string)GetValue(UntoggledValueTextProperty);
            set => SetValue(UntoggledValueTextProperty, value);
        }

        /// <summary>
        /// The untoggled value text property.
        /// </summary>
        public static readonly BindableProperty UntoggledValueTextProperty = BindableProperty.Create(nameof(UntoggledValueText), typeof(string), typeof(Setter), defaultValue: "Off");

        /// <summary>
        /// The value shown when the <see cref="Setter"/>'s <see cref="Ui.Enums.ToggleState"/> is <see cref="Ui.Enums.ToggleState.Indeterminate"/>.
        /// <para>Note: This value is only used when the <see cref="InputMode"/> is set to <see cref="Ui.Enums.InputMode.Toggle"/>.</para>
        /// </summary>
        public string IndeterminateValueText
        {
            get => (string)GetValue(IndeterminateValueTextProperty);
            set => SetValue(IndeterminateValueTextProperty, value);
        }

        /// <summary>
        /// The indeterminate value text property.
        /// </summary>
        public static readonly BindableProperty IndeterminateValueTextProperty = BindableProperty.Create(nameof(IndeterminateValueText), typeof(string), typeof(Setter), defaultValue: "Unknown");

        /// <summary>
        /// Indicates the <see cref="Ui.Enums.ToggleState"/> of the <see cref="Setter"/>.
        /// <para>Note: This property is only used when the <see cref="InputMode"/> is set to <see cref="Ui.Enums.InputMode.Toggle"/>.</para>
        /// </summary>
        public ToggleState ToggleState
        {
            get => (ToggleState)GetValue(ToggleStateProperty);
            set => SetValue(ToggleStateProperty, value);
        }

        /// <summary>
        /// The <see cref="Ui.Enums.ToggleState"/> property.
        /// </summary>
        public static readonly BindableProperty ToggleStateProperty = BindableProperty.Create(nameof(ToggleState), typeof(ToggleState), typeof(Setter), defaultBindingMode: BindingMode.TwoWay, propertyChanged: ToggleStateProperty_Changed, defaultValue: ToggleState.Indeterminate);

        /// <summary>
        /// Indicates whether or not the <see cref="Setter"/> should show the initial value on the <see cref="GetInputPopup"/>.
        /// <para>Note: This does not affect <see cref="Setter"/>s whose <see cref="Ui.Enums.InputMode"/> is set to <see cref="Ui.Enums.InputMode.Toggle"/> since they do not use a <see cref="GetInputPopup"/>.</para>
        /// </summary>
        public bool ShowInitialValueOnPopup
        {
            get => (bool)GetValue(ShowInitialValueOnPopupProperty);
            set => SetValue(ShowInitialValueOnPopupProperty, value);
        }

        /// <summary>
        /// The show initial value on popup property.
        /// </summary>
        public static readonly BindableProperty ShowInitialValueOnPopupProperty = BindableProperty.Create(nameof(ShowInitialValueOnPopup), typeof(bool), typeof(Setter), defaultValue: false);

        /// <summary>
        /// The <see cref="Setter"/>'s text color.
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// The text color property.
        /// </summary>
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(Setter), CustomColors.TextColor);

        /// <summary>
        /// The <see cref="Setter"/>'s disabled text color.
        /// </summary>
        public Color DisabledTextColor
        {
            get => (Color)GetValue(DisabledTextColorProperty);
            set => SetValue(DisabledTextColorProperty, value);
        }

        /// <summary>
        /// The disabled text color property.
        /// </summary>
        public static readonly BindableProperty DisabledTextColorProperty = BindableProperty.Create(nameof(DisabledTextColor), typeof(Color), typeof(Setter), CustomColors.DisabledTextColor);

        /// <summary>
        /// The source of the items to template and display.
        /// <para>Note: This property is only used when the <see cref="InputMode"/> is set to <see cref="Ui.Enums.InputMode.Selection"/>.</para>
        /// </summary>
        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// The items source property.
        /// <para>Note: This property is only used when the <see cref="InputMode"/> is set to <see cref="Ui.Enums.InputMode.Selection"/>.</para>
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(Setter));

        /// <summary>
        /// The <see cref="DataTemplate"/> used to render items.
        /// <para>Note: This property is only used when the <see cref="InputMode"/> is set to <see cref="Ui.Enums.InputMode.Selection"/>.</para>
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        /// <summary>
        /// The item template property.
        /// <para>Note: This property is only used when the <see cref="InputMode"/> is set to <see cref="Ui.Enums.InputMode.Selection"/>.</para>
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(Setter));

        /// <summary>
        /// Indicates whether or not the value is visible.
        /// <para>Note: This should only be set to false if the <see cref="InputMode"/> is set to <see cref="Ui.Enums.InputMode.Toggle"/> and you wish to show the toggle control without a value next to it.</para>
        /// </summary>
        public bool IsValueVisible
        {
            get => (bool)GetValue(IsValueVisibleProperty);
            set => SetValue(IsValueVisibleProperty, value);
        }

        /// <summary>
        /// The is value visible property.
        /// </summary>
        public static readonly BindableProperty IsValueVisibleProperty = BindableProperty.Create(nameof(IsValueVisible), typeof(bool), typeof(Setter), defaultValue: true);

        /// <summary>
        /// Determines whether or not the <see cref="ItemsSource"/> will be filtered to remove the currently selected item before being displayed in the <see cref="SelectionPopup"/>.
        /// <para>Note: This property is only used when the <see cref="Ui.Enums.InputMode"/> is set to <see cref="Ui.Enums.InputMode.Selection"/>.</para>
        /// </summary>
        public bool RemoveCurrentItemFromItemsSource
        {
            get => (bool)GetValue(RemoveCurrentItemFromItemsSourceProperty);
            set => SetValue(RemoveCurrentItemFromItemsSourceProperty, value);
        }

        /// <summary>
        /// The remove current item from items source property.
        /// </summary>
        public static readonly BindableProperty RemoveCurrentItemFromItemsSourceProperty = BindableProperty.Create(nameof(RemoveCurrentItemFromItemsSource), typeof(bool), typeof(Setter), defaultValue: true);

        /// <summary>
        /// The minimum value constraint.
        /// <para>Note: This property is only used when the <see cref="Ui.Enums.InputMode"/> is set to <see cref="Ui.Enums.InputMode.Numeric"/> or <see cref="Ui.Enums.InputMode.AlphaNumberic"/>.</para>
        /// </summary>
        public double? Min
        {
            get => (double?)GetValue(MinProperty);
            set => SetValue(MinProperty, value);
        }

        /// <summary>
        /// The min property.
        /// </summary>
        public static readonly BindableProperty MinProperty = BindableProperty.Create(nameof(Min), typeof(double?), typeof(Setter), defaultValue: null);

        /// <summary>
        /// The maximum value constraint.
        /// <para>Note: This property is only used when the <see cref="Ui.Enums.InputMode"/> is set to <see cref="Ui.Enums.InputMode.Numeric"/> or <see cref="Ui.Enums.InputMode.AlphaNumberic"/>.</para>
        /// </summary>
        public double? Max
        {
            get => (double?)GetValue(MaxProperty);
            set => SetValue(MaxProperty, value);
        }

        /// <summary>
        /// The max property.
        /// </summary>
        public static readonly BindableProperty MaxProperty = BindableProperty.Create(nameof(Max), typeof(double?), typeof(Setter), defaultValue: null);

        /// <summary>
        /// The maximum number of decimal places constraint.
        /// <para>Note: This property is only used when the <see cref="Ui.Enums.InputMode"/> is set to <see cref="Ui.Enums.InputMode.Numeric"/>.</para>
        /// </summary>
        public int? MaxNumberOfDecimalPlaces
        {
            get => (int?)GetValue(MaxNumberOfDecimalPlacesProperty);
            set => SetValue(MaxNumberOfDecimalPlacesProperty, value);
        }

        /// <summary>
        /// The max number of decimal places property.
        /// </summary>
        public static readonly BindableProperty MaxNumberOfDecimalPlacesProperty = BindableProperty.Create(nameof(MaxNumberOfDecimalPlaces), typeof(int?), typeof(Setter), defaultValue: null);

        /// <summary>
        /// The minimum number of decimal places constraint.
        /// <para>Note: This property is only used when the <see cref="Ui.Enums.InputMode"/> is set to <see cref="Ui.Enums.InputMode.Numeric"/>.</para>
        /// </summary>
        public int? MinNumberOfDecimalPlaces
        {
            get => (int?)GetValue(MinNumberOfDecimalPlacesProperty);
            set => SetValue(MinNumberOfDecimalPlacesProperty, value);
        }

        /// <summary>
        /// The min number of decimal places property.
        /// </summary>
        public static readonly BindableProperty MinNumberOfDecimalPlacesProperty = BindableProperty.Create(nameof(MinNumberOfDecimalPlaces), typeof(int?), typeof(Setter), defaultValue: null);

        /// <summary>
        /// The size of the buttons on the <see cref="SelectionPopup"/>.
        /// <para>Note: This property is only used if the <see cref="InputMode"/> is set to <see cref="Ui.Enums.InputMode.Selection"/>.</para>
        /// </summary>
        public double ButtonSize { get => (double)GetValue(ButtonSizeProperty); set => SetValue(ButtonSizeProperty, value); }

        /// <summary>
        /// The button size property.
        /// </summary>
        public static readonly BindableProperty ButtonSizeProperty = BindableProperty.Create(nameof(ButtonSize), typeof(double), typeof(Setter), 30.0);

        /// <summary>
        /// The <see cref="Xamarin.Forms.SelectionMode"/>.
        /// <para>Note: This property is only used if the <see cref="InputMode"/> is set to <see cref="Ui.Enums.InputMode.Selection"/>.</para>
        /// </summary>
        public SelectionMode SelectionMode { get => (SelectionMode)GetValue(SelectionModeProperty); set => SetValue(SelectionModeProperty, value); }

        /// <summary>
        /// The selection mode property.
        /// </summary>
        public static readonly BindableProperty SelectionModeProperty = BindableProperty.Create(nameof(SelectionMode), typeof(SelectionMode), typeof(Setter), SelectionMode.Single);

        /// <summary>
        /// Indicates whether or not the <see cref="SelectionPopup"/> will automatically close as soon as the user selects an item.
        /// <para>Note: This property is only used if the <see cref="InputMode"/> is set to <see cref="Ui.Enums.InputMode.Selection"/> and if the <see cref="SelectionMode"/> is set to <see cref="Xamarin.Forms.SelectionMode.Single"/>.</para>
        /// </summary>
        public bool AutoCloseSelectionPopupOnItemSelected { get => (bool)GetValue(AutoCloseSelectionPopupOnItemSelectedProperty); set => SetValue(AutoCloseSelectionPopupOnItemSelectedProperty, value); }

        /// <summary>
        /// The auto close selection popup on item selected property property.
        /// </summary>
        public static readonly BindableProperty AutoCloseSelectionPopupOnItemSelectedProperty = BindableProperty.Create(nameof(AutoCloseSelectionPopupOnItemSelected), typeof(bool), typeof(Setter), false);

        /// <summary>
        /// Indicates whether or not the value is required.
        /// </summary>
        public bool IsRequired
        {
            get => (bool)GetValue(IsRequiredProperty);
            set => SetValue(IsRequiredProperty, value);
        }

        /// <summary>
        /// The is required property.
        /// </summary>
        public static readonly BindableProperty IsRequiredProperty = BindableProperty.Create(nameof(IsRequired), typeof(bool), typeof(Setter), defaultValue: false, propertyChanged: (bindable, oldValue, newValue) =>
        {
            ((Setter)bindable).UpdateIsRequiredErrorMessageVisible();
        });

        public bool IsRequiredErrorMessageVisible { get => _isRequiredErrorMessageVisible; private set => SetProperty(ref _isRequiredErrorMessageVisible, value); }

        /// <summary>
        /// The <see cref="DataCollections.Models.DataItem"/> to edit.
        /// <para>When this property is used, the <see cref="Setter"/> will automatically bind the following properties:
        /// <para><see cref="IconResourceIdProperty"/> --> <see cref="DataItem.DisplayIconString"/></para>
        /// <para><see cref="LabelProperty"/> --> <see cref="DataItem.Label"/></para>
        /// <para><see cref="IsRequiredProperty"/> --> <see cref="DataItem.IsRequired"/></para>
        /// <para><see cref="ValueSuffixProperty"/> --> <see cref="DataItem.Units"/></para>
        /// <para><see cref="ValueProperty"/> --> DataItem.Value</para>
        /// </para>
        /// <para><see cref="ItemsSourceProperty"/> --> <see cref="EnumDataItem.PickList"/></para>
        /// <para>In addition, the constraints of the <see cref="DataItem"/> will be used without the need to enter the constraints on the <see cref="Setter"/> and the <see cref="InputMode"/> will be set based on the type of the <see cref="DataItem"/>.</para>
        /// </summary>
        public DataItem DataItem
        {
            get => (DataItem)GetValue(DataItemProperty);
            set => SetValue(DataItemProperty, value);
        }

        /// <summary>
        /// The data item property.
        /// </summary>
        public static readonly BindableProperty DataItemProperty = BindableProperty.Create(nameof(DataItem), typeof(DataItem), typeof(Setter), propertyChanged: (bindable, oldValue, newValue) =>
        {
            ((Setter)bindable).SetDataItemBindings();
        });

        /// <summary>
        /// The reserved characters constraint.
        /// <para>Note: This property is only used when the <see cref="Ui.Enums.InputMode"/> is set to <see cref="Ui.Enums.InputMode.AlphaNumberic"/> and must be in a regex format.</para>
        /// </summary>
        public string ReservedCharacters
        {
            get => (string)GetValue(ReservedCharactersProperty);
            set => SetValue(ReservedCharactersProperty, value);
        }

        /// <summary>
        /// The reserved characters property.
        /// </summary>
        public static readonly BindableProperty ReservedCharactersProperty = BindableProperty.Create(nameof(ReservedCharacters), typeof(string), typeof(Setter));

        /// <summary>
        /// The pattern mask constraint.
        /// <para>Note: This property is only used when the <see cref="Ui.Enums.InputMode"/> is set to <see cref="Ui.Enums.InputMode.AlphaNumberic"/>.</para>
        /// </summary>
        public string PatternMask
        {
            get => (string)GetValue(PatternMaskProperty);
            set => SetValue(PatternMaskProperty, value);
        }

        /// <summary>
        /// The pattern mask property.
        /// </summary>
        public static readonly BindableProperty PatternMaskProperty = BindableProperty.Create(nameof(PatternMask), typeof(string), typeof(Setter));

        /// <summary>
        /// Indicates whether or not the <see cref="Setter"/> is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        /// <summary>
        /// The read only property.
        /// </summary>
        public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(Setter), defaultValue: false);
        #endregion

        #region Events
        /// <summary>
        /// Indicates that the user has changed the value.
        /// </summary>
        public event EventHandler ValueChanged;
        #endregion

        #region Constructor
        public Setter()
        {
            InitializeComponent();

            UpdateIsRequiredErrorMessageVisible();

            Task.Run(async () =>
            {
                await Task.Delay(500);
                SetDefaultItemTemplate();
            });
        }
        #endregion

        #region Methods
        #region Internal
        internal void SetValueFromToggleState()
        {
            if (InputMode != InputMode.Toggle)
            {
                return;
            }

            switch (ToggleState)
            {
                case ToggleState.Toggled:
                    Value = ToggledValueText;
                    break;
                case ToggleState.Untoggled:
                    Value = UntoggledValueText;
                    break;
                default:
                    Value = IndeterminateValueText;
                    break;
            }
        }
        #endregion

        #region Private
        private async void Setter_Tapped(object sender, EventArgs e)
        {
            if (!IsEnabled || InputMode == InputMode.Toggle || IsReadOnly)
            {
                return;
            }

            PopupResult result;
            if (InputMode == InputMode.AlphaNumberic)
            {
                var initialValue = ShowInitialValueOnPopup ? Value?.ToString() : null;

                if (DataItem is StringDataItem stringDataItem)
                {
                    result = await Application.Current.MainPage.ShowKeyboard(stringDataItem, IconResourceId, Label, initialValue, ValueSuffix);
                }
                else
                {
                    result = await Application.Current.MainPage.ShowKeyboard(IconResourceId, Label, initialValue, ValueSuffix, ReservedCharacters, PatternMask, Min, Max);
                }
            }
            else if (InputMode == InputMode.Numeric)
            {
                var initialValue = (ShowInitialValueOnPopup && double.TryParse(Value?.ToString(), out double initialValueDouble)) ? initialValueDouble : 0;

                if (DataItem is DoubleDataItem doubleDataItem)
                {
                    result = await Application.Current.MainPage.ShowNumberPad(doubleDataItem, IconResourceId, Label, initialValue, ValueSuffix);
                }
                else
                {
                    result = await Application.Current.MainPage.ShowNumberPad(IconResourceId, Label, initialValue, ValueSuffix, Min, Max, MaxNumberOfDecimalPlaces, MinNumberOfDecimalPlaces);
                }
            }
            else
            {
                var IEnumerable = ItemsSource;
                if (RemoveCurrentItemFromItemsSource && ItemsSource != null)
                {
                    List<object> itemsSource = new List<object>();
                    foreach (var item in ItemsSource)
                    {
                        /* If the item is a SelectionObject (which would have
                         * come from an EnumDataItem.PickList) and the DataItem
                         * is an EnumDataItem and the selectionObject's id is the
                         * same as the EnumDataItem's Value (which means that the
                         * selection object is currently selected), don't add it
                         * to the list.
                         */
                        if (item is SelectionObject selectionObject && DataItem is EnumDataItem enumDataItem)
                        {
                            if (selectionObject.Id != enumDataItem.Value)
                            {
                                itemsSource.Add(item);
                            }
                        }
                        else if (!item.Equals(Value))
                        {
                            itemsSource.Add(item);
                        }
                    }
                    IEnumerable = itemsSource;
                }
                var itemTemplate = ItemTemplate.CreateContent() is Grid g && g.Children.Count > 0 && g.Children[0] is CustomLabel ? null : ItemTemplate;
                var settings = new SelectionSettings()
                {
                    SelectionMode = SelectionMode,
                    ButtonSize = ButtonSize,
                    BackgroundColor = CustomColors.BackgroundColor,
                };
                result = await Application.Current.MainPage.ShowSelectionDialog(IEnumerable, IconResourceId, Label, ValueSuffix, itemTemplate, settings, AutoCloseSelectionPopupOnItemSelected);
            }

            if (!IsEnabled || result.UserCancelled)
            {
                return;
            }

            if (result.Value is SelectionObject s)
            {
                Value = s.Id;
            }
            else
            {
                Value = result.Value;
            }

            RaiseValueChanged();
        }

        private static void ToggleStateProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var setter = (Setter)bindable;
            setter.SetValueFromToggleState();
        }

        private void RaiseValueChanged()
        {
            ValueChanged?.Invoke(this, new EventArgs());
        }

        private static void InputModeProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var setter = (Setter)bindable;
            setter.SetValueFromToggleState();
        }

        private void ToggleSwitch_Toggled(object sender, EventArgs e)
        {
            RaiseValueChanged();
        }

        private void SetDefaultItemTemplate()
        {
            if (ItemTemplate != null)
            {
                return;
            }

            ItemTemplate = (DataTemplate)Resources["DefaultSetterItemTemplate"];
        }

        private void UpdateIsRequiredErrorMessageVisible()
        {
            IsRequiredErrorMessageVisible = IsRequired && (Value == null || (Value is string s && string.IsNullOrEmpty(s)));
        }

        private void SetDataItemBindings()
        {
            RemoveBinding(IconResourceIdProperty);
            SetBinding(IconResourceIdProperty, new Binding(nameof(DataItem.DisplayIconString), source: DataItem));

            RemoveBinding(LabelProperty);
            SetBinding(LabelProperty, new Binding(nameof(DataItem.Label), source: DataItem));

            RemoveBinding(IsRequiredProperty);
            SetBinding(IsRequiredProperty, new Binding(nameof(DataItem.IsRequired), source: DataItem));

            RemoveBinding(ValueSuffixProperty);
            SetBinding(ValueSuffixProperty, new Binding(nameof(DataItem.Units), source: DataItem));

            /* Since the numberpad and keyboard popups both will set the value
             * of the data item directly, we want a oneway binding from the
             * DataItem.Value property to the Setter.Value property to show the
             * value to the user on the Setter.
             */
            RemoveBinding(ValueProperty);
            SetBinding(ValueProperty, new Binding("Value", source: DataItem, mode: BindingMode.OneWay));

            if (DataItem is StringDataItem)
            {
                InputMode = InputMode.AlphaNumberic;
            }
            else if (DataItem is DoubleDataItem)
            {
                InputMode = InputMode.Numeric;
            }
            else if (DataItem is EnumDataItem)
            {
                InputMode = InputMode.Selection;

                RemoveBinding(ItemsSourceProperty);
                SetBinding(ItemsSourceProperty, new Binding(nameof(EnumDataItem.PickList), source: DataItem));

                /* We need to remove the OneWay binding that was created above and
                 * create the default TwoWay binding when in InputMode.Selection
                 * since the selection popup doesn't directly change the value
                 * of the DataItem.
                 */
                RemoveBinding(ValueProperty);
                SetBinding(ValueProperty, new Binding(nameof(EnumDataItem.Value), source: DataItem));

                /* We need a special data template to convert the
                 * EnumDataItem.Value (which is an int) into a display
                 * string.
                 */
                ItemTemplate = (DataTemplate)Resources["EnumDataItemSetterItemTemplate"];

            }
            else if (DataItem is BooleanDataItem b)
            {
                InputMode = InputMode.Toggle;

                /* We need to remove the binding on the Setter.ValueProperty because when we're in InputMode.Toggle, we use the Setter.ToggleStateProperty instead.
                 */
                RemoveBinding(ValueProperty);

                RemoveBinding(ToggleStateProperty);
                SetBinding(ToggleStateProperty, new Binding(nameof(BooleanDataItem.Value), source: DataItem, converter: new BoolToToggleStateConverter()));
            }
        }
        #endregion
        #endregion
    }
}
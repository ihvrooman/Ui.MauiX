using DataCollections.Models;
using Ui.MauiX.CustomControls.ViewModels;
using Ui.MauiX.Helpers;
using Ui.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    /// <summary>
    /// An on-screen numberpad.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NumberPadPopup : BaseInputControl
    {
        #region Properties
        public override object Value
        {
            get => DoubleDataItem.StringValue;
            set
            {
                if (Value == value)
                {
                    return;
                }
                DoubleDataItem.StringValue = (string)value;
                OnPropertyChanged();
            }
        }

        internal override string ClearedValue => "0";

        public static Thickness DefaultPadding { get; set; } = new Thickness(225, 30);

        public new Thickness Padding => DefaultPadding;

        private DoubleDataItem DoubleDataItem => (DoubleDataItem)DataItem;

        public override CornerRadius CornerRadius => new CornerRadius(5);
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new <see cref="NumberPadPopup"/> with the specified constraints.
        /// </summary>
        /// <param name="min">The minimum value constraint.</param>
        /// <param name="max">The maximum value constraint.</param>
        /// <param name="maxNumberOfDecimalPlaces">The maximum number of decimal places constraint.</param>
        /// <param name="minNumberOfDecimalPlaces">The minimum number of decimal places constraint.</param>
        internal NumberPadPopup(double? min, double? max, int? maxNumberOfDecimalPlaces, int? minNumberOfDecimalPlaces)
        {
            DataItem = new DoubleDataItem();
            InitializeComponent();

            var tip = $"Min: {min} | Max: {max}";

            if (min != null)
            {
                DoubleDataItem.AddConstraint(ConstraintTypes.Min, min.ToString(), tip, 0);
            }
            if (max != null)
            {
                DoubleDataItem.AddConstraint(ConstraintTypes.Max, max.ToString(), tip, 1);
            }
            if (maxNumberOfDecimalPlaces != null)
            {
                DoubleDataItem.AddConstraint(ConstraintTypes.MaxDecimalPlaces, maxNumberOfDecimalPlaces.ToString());
            }
            if (minNumberOfDecimalPlaces != null)
            {
                DoubleDataItem.AddConstraint(ConstraintTypes.MinDecimalPlaces, minNumberOfDecimalPlaces.ToString(), "Min decimal places: {0}", 2);
            }
        }

        internal NumberPadPopup(DoubleDataItem doubleDataItem)
        {
            DataItem = doubleDataItem;
            InitializeComponent();
        }
        #endregion

        #region Methods
        #region Public
        /// <summary>
        /// Shows a numberpad to the user to collect numerical input.
        /// </summary>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which the popup is to be displayed.</param>
        /// <param name="iconResourceId">The icon resource id.</param>
        /// <param name="label">The label which describes the value that is being collected from the user.</param>
        /// <param name="initialValue">The initial value to show.</param>
        /// <param name="valueSuffix">The suffix of the value.
        /// <para>The unit of measure would be a common value for this property.</para></param>
        /// <param name="min">The minimum value constraint.</param>
        /// <param name="max">The maximum value constraint.</param>
        /// <param name="maxNumberOfDecimalPlaces">The maximum number of decimal places constraint.</param>
        /// <param name="minNumberOfDecimalPlaces">The minimum number of decimal places constraint.</param>
        /// <returns></returns>
        public static async Task<PopupResult> Show(NavigableElement navigableElement, string iconResourceId = null, string label = null, double initialValue = 0, string valueSuffix = null, double? min = null, double? max = null, int? maxNumberOfDecimalPlaces = null, int? minNumberOfDecimalPlaces = null)
        {
            return await Device.InvokeOnMainThreadAsync(async () =>
            {
                var popupPushed = false;
                NumberPadPopup popup = null;
                try
                {
                    popup = new NumberPadPopup(min, max, maxNumberOfDecimalPlaces, minNumberOfDecimalPlaces)
                    {
                        Label = label,
                        IconResourceId = iconResourceId,
                        ValueSuffix = valueSuffix,
                        Value = initialValue.ToString(),
                    };
                    await navigableElement.OpenModalPopup(popup);
                    popupPushed = true;

                    await popup.WaitForResultToBeSet();

                    return popup.Result;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (popupPushed)
                    {
                        await navigableElement.CloseModalPopup(popup);
                    }
                }
            });
        }

        /// <summary>
        /// Shows a numberpad to the user to collect numerical input.
        /// </summary>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which the popup is to be displayed.</param>
        /// <param name="doubleDataItem">The <see cref="DataCollections.Models.DoubleDataItem"/>.</param>
        /// <param name="iconResourceId">The icon resource id.</param>
        /// <param name="label">The label which describes the value that is being collected from the user.</param>
        /// <param name="initialValue">The initial value to show.</param>
        /// <param name="valueSuffix">The suffix of the value.
        /// <para>The unit of measure would be a common value for this property.</para></param>
        /// <returns></returns>
        public static async Task<PopupResult> Show(NavigableElement navigableElement, DoubleDataItem doubleDataItem, string iconResourceId = null, string label = null, double initialValue = 0, string valueSuffix = null)
        {
            return await Device.InvokeOnMainThreadAsync(async () =>
            {
                var popupPushed = false;
                NumberPadPopup popup = null;
                try
                {
                    var iValue = doubleDataItem.Value;
                    popup = new NumberPadPopup(doubleDataItem)
                    {
                        Label = label,
                        IconResourceId = iconResourceId,
                        ValueSuffix = valueSuffix,
                        Value = initialValue.ToString(),
                    };
                    await navigableElement.OpenModalPopup(popup);
                    popupPushed = true;

                    await popup.WaitForResultToBeSet();

                    if (popup.Result.UserCancelled)
                    {
                        //If the user cancels their operation, revert the value to it's original value
                        doubleDataItem.Value = iValue;
                    }

                    popup.DataItem = null;

                    return popup.Result;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (popupPushed)
                    {
                        await navigableElement.CloseModalPopup(popup);
                    }
                }
            });
        }
        #endregion

        #region Private
        private void PlusMinusButton_Tapped(object sender, EventArgs e)
        {
            if (StringValue.StartsWith("-"))
            {
                Value = StringValue.Remove(0, 1);
            }
            else
            {
                Value = "-" + Value;
            }
        }
        #endregion
        #endregion
    }
}
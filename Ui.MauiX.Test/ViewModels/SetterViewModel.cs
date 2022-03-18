using DataCollections.Models;
using Ui.MauiX.Test.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ui.MauiX.Test.ViewModels
{
    public class SetterViewModel : ToggleViewModel
    {
        #region Fields
        private bool _areSuffixesVisible = true;
        private bool _showInitialValueOnPopup = false;
        private bool _removeCurrentItemFromItemsSource = true;
        private bool _autoCloseSelectionPopupOnItemSelection = false;
        private bool _isRequired = false;
        private SelectionMode _selectionMode = SelectionMode.Single;
        private bool _isReadOnly;
        #endregion

        #region Properties
        public bool AreSuffixesVisible { get => _areSuffixesVisible; set => SetProperty(ref _areSuffixesVisible, value); }

        public bool ShowInitialValueOnPopup { get => _showInitialValueOnPopup; set => SetProperty(ref _showInitialValueOnPopup, value); }

        public bool IsReadOnly { get => _isReadOnly; set => SetProperty(ref _isReadOnly, value); }

        public bool IsRequired { get => _isRequired; set => SetProperty(ref _isRequired, value); }

        public bool RemoveCurrentItemFromItemsSource { get => _removeCurrentItemFromItemsSource; set => SetProperty(ref _removeCurrentItemFromItemsSource, value); }

        public bool AutoCloseSelectionPopupOnItemSelection { get => _autoCloseSelectionPopupOnItemSelection; set => SetProperty(ref _autoCloseSelectionPopupOnItemSelection, value); }

        public SelectionMode SelectionMode { get => _selectionMode; set => SetProperty(ref _selectionMode, value); }

        public DoubleDataItem DoubleDataItem { get; } = new DoubleDataItem();

        public StringDataItem StringDataItem { get; } = new StringDataItem();

        public BooleanDataItem BooleanDataItem { get; } = new BooleanDataItem();

        public EnumDataItem EnumDataItem { get; } = new EnumDataItem();
        #endregion

        #region Constructor
        public SetterViewModel()
        {
            SupplementalPageControlTypes.Add(typeof(SetterSupplementalPageControls));

            DoubleDataItem.AddConstraint(ConstraintTypes.Min, "-50", "Value must be greater than or equal to -50", 0);
            DoubleDataItem.AddConstraint(ConstraintTypes.Max, "50", "Value must be less than or equal to 50", 1);
            DoubleDataItem.AddConstraint(ConstraintTypes.MinDecimalPlaces, "2", "Must have 2 decimal places.", 2);
            DoubleDataItem.AddConstraint(ConstraintTypes.MaxDecimalPlaces, "5", "Must have less than 6 decimal places.", 3);
            DoubleDataItem.Label = "Numeric Bound to DoubleDataItem";
            DoubleDataItem.DisplayIconString = "Info.png";
            DoubleDataItem.Units = "$";
            DoubleDataItem.PropertyChanged += DataItem_PropertyChanged;

            StringDataItem.AddConstraint(ConstraintTypes.ReservedCharacters, "[f-gF-G]+", "Text can only have letters 'f' and 'g'", 0);
            StringDataItem.AddConstraint(ConstraintTypes.Min, "3", "Value must be at least 3 characters", 1);
            StringDataItem.AddConstraint(ConstraintTypes.Max, "7", "Value must be less than or equal to 7 characters", 2);
            StringDataItem.DisplayIconString = "Info.png";
            StringDataItem.Label = "AlphaNumeric Bound to StringDataItem";
            StringDataItem.Units = "Ave";
            StringDataItem.PropertyChanged += DataItem_PropertyChanged;

            BooleanDataItem.Label = "Toggle Bound to BooleanDataItem";
            BooleanDataItem.DisplayIconString = "Info.png";
            BooleanDataItem.PropertyChanged += DataItem_PropertyChanged;

            EnumDataItem.Label = "Selection Bound to EnumDataItem";
            EnumDataItem.DisplayIconString = "Info.png";
            EnumDataItem.PropertyChanged += DataItem_PropertyChanged;
            EnumDataItem.PickList = new List<SelectionObject>()
            {
                new SelectionObject(){ DisplayString = "Distek", Id = 1 },
                new SelectionObject(){ DisplayString = "AT7Smart" , Id = 2},
                new SelectionObject(){ DisplayString = "Other", Id = 3 },
            };
        }
        #endregion

        #region Methods
        private void DataItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                var value = string.Empty;
                if (sender is DoubleDataItem d)
                {
                    value = d.Value.ToString();
                }
                else if (sender is StringDataItem s)
                {
                    value = s.Value;
                }
                else if (sender is EnumDataItem en)
                {
                    value = en.Value.ToString();
                }
                else if (sender is BooleanDataItem b)
                {
                    value = b.Value.ToString();
                }

                Debug.WriteLine($"@@ Data item value set to: {value}");
            }
        }
        #endregion
    }
}

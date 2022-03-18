using System;
using System.Collections.Generic;
using System.Text;
using Components.Enums;

namespace Components
{
    public class NumericWorkflowInfo : AlphaNumericWorkflowInfo
    {
        #region Fields
        private double? _maxNumberOfDecimalPlaces = null;
        private double? _minNumberOfDecimalPlaces = null;
        #endregion

        #region Properties
        public double? MaxNumberOfDecimalPlaces { get => _maxNumberOfDecimalPlaces; set => SetProperty(ref _maxNumberOfDecimalPlaces, value); }

        public double? MinNumberOfDecimalPlaces { get => _minNumberOfDecimalPlaces; set => SetProperty(ref _minNumberOfDecimalPlaces, value); }
        #endregion

        #region Constructor
        public NumericWorkflowInfo()
        {
            InputMode = InputMode.Numeric;
        }
        #endregion
    }
}

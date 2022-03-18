using Components.Helpers;
using Components.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Components
{
    public class AlphaNumericWorkflowInfo : NotifyPropertyChanged
    {
        #region Fields
        private string _iconResourceId = string.Empty;
        private string _label;
        private object _value;
        private string _valueSuffix;
        private double? _min = null;
        private double? _max = null;
        private string _reservedCharacters;
        private string _patternMask;
        private bool _isRequired;
        #endregion

        #region Properties
        public string IconResourceId { get => _iconResourceId; set => SetProperty(ref _iconResourceId, value); }

        public string Label { get => _label; set => SetProperty(ref _label, value); }

        public object Value { get => _value; set => SetProperty(ref _value, value); }

        public string ValueSuffix { get => _valueSuffix; set => SetProperty(ref _valueSuffix, value); }

        public InputMode InputMode { get; internal set; } = InputMode.AlphaNumberic;

        public double? Min { get => _min; set => SetProperty(ref _min, value); }

        public double? Max { get => _max; set => SetProperty(ref _max, value); }

        public string ReservedCharacters { get => _reservedCharacters; set => SetProperty(ref _reservedCharacters, value); }

        public string PatternMatch { get => _patternMask; set => SetProperty(ref _patternMask, value); }

        public bool IsRequired { get => _isRequired; set => SetProperty(ref _isRequired, value); }
        #endregion
    }
}

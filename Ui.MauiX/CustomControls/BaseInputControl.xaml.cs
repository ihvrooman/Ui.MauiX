using Ui.Models;
using DataCollections.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    /// <summary>
    /// The base implementation for an input control.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public abstract partial  class BaseInputControl : ContentPageWithSetPropertyMethod
    {
        #region Fields
        private Command _appendToValueCommand;
        private Command _deleteCharacterCommand;
        private Command _clearValueCommand;
        private Command _doneCommand;
        private Command _cancelCommand;
        private string _iconResourceId;
        private string _label;
        private string _valueSuffix;
        private DataTemplate _itemTemplate;
        private readonly string _defaultItemTemplateResourcesId;
        private Thickness _contentMargin = new Thickness(10,0,10,10);
        private bool _isInvalidTipVisible = true;
        private DataItem _dataItem;
        #endregion

        #region Properties
        public Command AppendToValueCommand
        {
            get
            {
                if (_appendToValueCommand == null)
                {
                    _appendToValueCommand = new Command<string>(AppendToValue);
                }
                return _appendToValueCommand;
            }
        }

        public Command DeleteCharacterCommand
        {
            get
            {
                if (_deleteCharacterCommand == null)
                {
                    _deleteCharacterCommand = new Command(DeleteCharacter);
                }
                return _deleteCharacterCommand;
            }
        }

        public Command ClearValueCommand
        {
            get
            {
                if (_clearValueCommand == null)
                {
                    _clearValueCommand = new Command(ClearValue);
                }
                return _clearValueCommand;
            }
        }

        public Command DoneCommand
        {
            get
            {
                if (_doneCommand == null)
                {
                    _doneCommand = new Command((obj) => SetResult(false), CanExecuteDoneCommand);
                }
                return _doneCommand;
            }
        }

        public Command CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new Command((obj) => SetResult(true));
                }
                return _cancelCommand;
            }
        }

        /// <summary>
        /// The value entered by the user.
        /// </summary>
        public abstract object Value { get; set; }

        /// <summary>
        /// The value entered by the user displayed as a string.
        /// </summary>
        public string StringValue => Value.ToString();

        /// <summary>
        /// The value used when the input is cleared.
        /// </summary>
        internal abstract string ClearedValue { get; }

        /// <summary>
        /// The resource id of the icon.
        /// </summary>
        public string IconResourceId { get => _iconResourceId; set { SetProperty(ref _iconResourceId, value); } }

        /// <summary>
        /// The label which describes what value the <see cref="BaseInputControl"/> is setting.
        /// </summary>
        public string Label { get => _label; set { SetProperty(ref _label, value); } }

        /// <summary>
        /// The suffix of the value.
        /// <para>The unit of measure would be a common value for this property.</para>
        /// </summary>
        public string ValueSuffix { get => _valueSuffix; set { SetProperty(ref _valueSuffix, value); } }

        /// <summary>
        /// The <see cref="PopupResult"/> from showing the <see cref="BaseInputControl"/>.
        /// </summary>
        internal PopupResult Result;

        public DataItem DataItem 
        { 
            get => _dataItem;
            set
            {
                if (DataItem != null)
                {
                    DataItem.PropertyChanged -= DataItem_PropertyChanged;
                }

                SetProperty(ref _dataItem, value);

                if (DataItem != null)
                {
                    DataItem.PropertyChanged += DataItem_PropertyChanged;
                }
            }
        }

        public abstract CornerRadius CornerRadius { get; }

        public DataTemplate ItemTemplate { get => _itemTemplate; set => SetProperty(ref _itemTemplate, value); }

        /// <summary>
        /// The margin around the content of the input control.
        /// </summary>
        public Thickness ContentMargin { get => _contentMargin; set => SetProperty(ref _contentMargin, value); }

        /// <summary>
        /// Indicates whether or not the invalid tip is visible.
        /// <para>Note: When this property is set to true, the tip will not actually display until the <see cref="DataItem.IsValid"/> property is false.</para>
        /// </summary>
        public bool IsInvalidTipVisible { get => _isInvalidTipVisible; set => SetProperty(ref _isInvalidTipVisible, value); }
        #endregion

        #region Constructor
        public BaseInputControl(string defaultItemTemplateResourceId = "DefaultInputControlItemTemplate")
        {
            _defaultItemTemplateResourcesId = defaultItemTemplateResourceId;
            BackgroundColor = Color.Transparent;
            InitializeComponent();
            SetDefaultItemTemplate();
        }
        #endregion

        #region Methods
        #region Protected
        protected async Task WaitForResultToBeSet()
        {
            while (Result == null)
            {
                await Task.Delay(50);
            }
        }

        protected virtual void SetResult(bool userCancelled)
        {
            Result = new PopupResult(userCancelled, Value);
        }

        protected virtual bool CanExecuteDoneCommand(object arg)
        {
            return DataItem != null && DataItem.IsValid;
        }
        #endregion

        #region Private
        private void AppendToValue(string obj)
        {
            Value += obj;
        }

        private void DeleteCharacter(object obj)
        {
            if (string.IsNullOrEmpty(StringValue))
            {
                return;
            }

            if (StringValue.Length <= 1)
            {
                Value = ClearedValue;
                return;
            }

            Value = StringValue.Substring(0, StringValue.Length - 1);
        }

        private void ClearValue(object obj)
        {
            Value = ClearedValue;
        }

        private void DataItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(DataItem.IsValid))
            {
                return;
            }

            DoneCommand.ChangeCanExecute();
        }

        private void SetDefaultItemTemplate()
        {
            if (ItemTemplate != null)
            {
                return;
            }

            ItemTemplate = (DataTemplate)Resources[_defaultItemTemplateResourcesId];
        }
        #endregion
        #endregion
    }
}
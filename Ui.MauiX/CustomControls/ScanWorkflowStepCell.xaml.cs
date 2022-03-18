using Components.Models;
using Ui.MauiX.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanWorkflowStepCell : ContentView
    {
        #region Fields
        private string _scannedValue;
        private Color _frameColor = Color.LightGray;
        private bool _isValidated;
        #endregion

        #region Properties
        public string ExpectedValueString { get => "Expected Value: "; }

        public string ScannedValue
        {
            get => _scannedValue;
            set
            {
                _scannedValue = value;
                OnPropertyChanged();
            }
        }

        public Color FrameColor
        {
            get => _frameColor;
            private set
            {
                _frameColor = value;
                OnPropertyChanged();
            }
        }

        private ValidateWorkflowStep WorkflowStep => (ValidateWorkflowStep)BindingContext;
        #endregion

        #region Constructor
        public ScanWorkflowStepCell()
        {
            InitializeComponent();
            SetFocusToEntryField();
        }
        #endregion

        #region Methods
        private async void SetFocusToEntryField()
        {
            await Task.Delay(200);
            idEntry.Focus();
        }

        private void Entry_TextChanged(object sender, EventArgs e)
        {
            //We need a short delay here to allow the card reader to input the full value
            Task.Run(() =>
            {
                Task.Delay(200).Wait();
                Validate();
            });
        }

        private void Validate()
        {
            if (ScannedValue.Equals(WorkflowStep.ValidationValue))
            {
                if (_isValidated)
                    return;

                _isValidated = true;

                FrameColor = Color.LightGreen;

                //Delay so user sees confirmation
                Task.Run(() =>
                {
                    Task.Delay(2000).Wait();
                    PostValidation();
                });
            }
            else
            {
                _isValidated = false;
                FrameColor = Color.Red;
                Device.BeginInvokeOnMainThread(() =>
                {
                    idEntry.CursorPosition = 0;
                    idEntry.SelectionLength = idEntry.Text.Length;
                });
            }
        }

        private void PostValidation()
        {
            WorkflowStep.DateTimeStamp = DateTime.Now.ToString();
            WorkflowStep.IsCompleted = true;
        }
        #endregion
    }
}
using Components.Models;
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
    public partial class ManualEntryWorkflowStepCell : ContentView
    {
        #region Fields

        private Color _frameColor = Color.LightGray;

        #endregion

        #region Properties

        public Color FrameColor
        {
            get => _frameColor;
            private set
            {
                _frameColor = value;
                OnPropertyChanged();
            }
        }

        private ManualEntryWorkflowStep WorkflowStep => (ManualEntryWorkflowStep)BindingContext;

        #endregion

        public ManualEntryWorkflowStepCell()
        {
            InitializeComponent();
            SetFocusToEntryField();
        }

        private async void SetFocusToEntryField()
        {
            await Task.Delay(200);
            idEntry.Focus();
        }

        private void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            CompleteStep();
        }

        private void CompleteStep()
        {
            WorkflowStep.IsCompleted = true;
            WorkflowStep.DateTimeStamp = DateTime.Now.ToString();
        }
    }
}
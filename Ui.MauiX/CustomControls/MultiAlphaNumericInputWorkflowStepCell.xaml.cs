using Components.Models;
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
    public partial class MultiAlphaNumericInputWorkflowStepCell : ContentView
    {
        #region Properties
        private MultiAlphaNumericInputWorkflowStep WorkflowStep => (MultiAlphaNumericInputWorkflowStep)BindingContext;
        #endregion

        #region Constructor

        public MultiAlphaNumericInputWorkflowStepCell()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            CompleteStep();
        }

        private void CompleteStep()
        {
            WorkflowStep.IsCompleted = true;
            WorkflowStep.DateTimeStamp = DateTime.Now.ToString();
        }

        #endregion
    }
}
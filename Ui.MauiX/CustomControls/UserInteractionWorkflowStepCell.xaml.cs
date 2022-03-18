using Components.Models;
using Ui.MauiX.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserInteractionWorkflowStepCell : ContentView
    {
        #region Fields
        private Command _confirmCommand;
        #endregion

        #region Properties
        public Command ConfirmCommand
        {
            get
            {
                if (_confirmCommand == null)
                {
                    _confirmCommand = new Command(Confirm);
                }
                return _confirmCommand;
            }
        }

        private WorkflowStep WorkflowStep => (WorkflowStep)BindingContext;
        #endregion

        #region Constructor
        public UserInteractionWorkflowStepCell()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void Confirm(object obj)
        {
            WorkflowStep.IsCompleted = true;
            WorkflowStep.DateTimeStamp = DateTime.Now.ToString();
        }
        #endregion
    }
}
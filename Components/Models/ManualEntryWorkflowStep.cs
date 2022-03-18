using System;
using System.Collections.Generic;
using System.Text;

namespace Components.Models
{
    public class ManualEntryWorkflowStep : WorkflowStep
    {
        private string _enteredValue;
        public string EnteredValue
        {
            get => _enteredValue;
            set => SetProperty(ref _enteredValue, value);
        }

        #region Methods
        protected override void CopyFromWorkflowStep(WorkflowStep workflowStep)
        {
            base.CopyFromWorkflowStep(workflowStep);

            if (workflowStep is ManualEntryWorkflowStep baseSelectionWorkflowStep)
            {
                EnteredValue = baseSelectionWorkflowStep.EnteredValue;
            }
        }
        #endregion
    }
}

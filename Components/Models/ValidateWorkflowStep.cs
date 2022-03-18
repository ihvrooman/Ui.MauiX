using System;
using System.Collections.Generic;
using System.Text;

namespace Components.Models
{
    public class ValidateWorkflowStep : WorkflowStep
    {
        #region Fields
        private string _validationValue;
        #endregion

        #region Properties
        /// <summary>
        /// The validation value.
        /// </summary>
        public string ValidationValue { get => _validationValue; set { SetProperty(ref _validationValue, value); } }
        #endregion

        #region Methods
        protected override void CopyFromWorkflowStep(WorkflowStep workflowStep)
        {
            base.CopyFromWorkflowStep(workflowStep);

            if (workflowStep is ValidateWorkflowStep validateWorkflowStep)
            {
                ValidationValue = validateWorkflowStep.ValidationValue;
            }
        }
        #endregion
    }
}

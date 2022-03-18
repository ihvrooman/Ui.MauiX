using Components.Models;
using Components.Enums;
using System;
using System.Collections.ObjectModel;

namespace Components.Models
{
    public class MultiAlphaNumericInputWorkflowStep : WorkflowStep
    {
        #region Fields
        private ObservableCollection<AlphaNumericWorkflowInfo> _inputInfo = new ObservableCollection<AlphaNumericWorkflowInfo>();
        #endregion

        #region Properties
        public ObservableCollection<AlphaNumericWorkflowInfo> InputInfo { get => _inputInfo; set => SetProperty(ref _inputInfo, value); }
        #endregion

        #region Constructors
        public MultiAlphaNumericInputWorkflowStep()
        {
            Type = WorkflowStepType.MultiAlphaNumericInput;
        }

        public MultiAlphaNumericInputWorkflowStep(WorkflowStep workflowStep)
        {
            if (workflowStep.Type != WorkflowStepType.MultiAlphaNumericInput)
            {
                throw new ArgumentException("Invalid WorkflowStepType", nameof(workflowStep.Type));
            }

            CopyFromWorkflowStep(workflowStep);
        }
        #endregion
    }
}

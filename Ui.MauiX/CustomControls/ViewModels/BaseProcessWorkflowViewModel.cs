using Components.Enums;
using Components.Models;
using Ui.MauiX.Enums;
using Ui.MauiX.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ui.MauiX.CustomControls.ViewModels
{
    public abstract class BaseProcessWorkflowViewModel : BaseWorkflowViewModel
    {
        #region Fields
        private WorkflowStepType _summaryWorkflowStepType;
        #endregion

        #region Constructor
        public BaseProcessWorkflowViewModel(WorkflowStepType summaryWorkflowStepType = WorkflowStepType.ExpanderSummary)
        {
            AutoScrollLastItemIntoView = true;
            _summaryWorkflowStepType = summaryWorkflowStepType;
        }
        #endregion

        #region Methods
        protected override Task OnAddStep(WorkflowStep step)
        {
            step.Type = _summaryWorkflowStepType;
            return base.OnAddStep(step);
        }

        protected async Task AddSubStepAndWaitForItToBeCompleted(WorkflowStep step)
        {
            await base.OnAddSubStep(step);
            await WaitForStepToBeCompleted(step);

            //This implementation was for when the current sub step was displayed at the bottom.
            //CurrentSubStep = step;
            //await WaitForStepToBeCompleted(step);
            //await base.OnAddSubStep(step);
        }
        #endregion
    }
}

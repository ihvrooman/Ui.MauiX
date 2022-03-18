using Components.Enums;
using Components.Models;
using Ui.MauiX.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Ui.MauiX.Models
{
    public class SelectionWorkflowStep : BaseSelectionWorkflowStep
    {
        #region Fields
        private SelectionWorkflowStyle _selectionWorkflowStyle = SelectionWorkflowStyle.RadioButton;
        private SelectionSettings _selectionSettings = new SelectionSettings();
        #endregion

        #region Properties
        public SelectionWorkflowStyle SelectionWorkflowStyle { get => _selectionWorkflowStyle; set => SetProperty(ref _selectionWorkflowStyle, value); }

        public SelectionSettings Settings { get => _selectionSettings; set => SetProperty(ref _selectionSettings, value); }

        #endregion

        #region Constructors
        public SelectionWorkflowStep()
        {
            Type = WorkflowStepType.Selection;
        }

        public SelectionWorkflowStep(BaseSelectionWorkflowStep baseSelectionWorkflowStep)
        {
            if (baseSelectionWorkflowStep.Type != WorkflowStepType.Selection)
            {
                throw new ArgumentException("Invalid WorkflowStepType", nameof(baseSelectionWorkflowStep.Type));
            }

            CopyFromWorkflowStep(baseSelectionWorkflowStep);
        }
        #endregion
    }
}

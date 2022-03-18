using System;
using System.Collections.Generic;
using System.Text;

namespace Components.Models
{
    public class BaseSelectionWorkflowStep : WorkflowStep
    {
        #region Fields
        private IEnumerable<BaseSelectableItem> _selections;
        private BaseSelectableItem _selectedItem;
        private bool _requireSelectionConfirmation = true;
        #endregion

        #region Properties
        /// <summary>
        /// The selections available to choose from.
        /// </summary>
        public IEnumerable<BaseSelectableItem> Selections
        {
            get => _selections;
            set => SetProperty(ref _selections, value);
        }

        /// <summary>
        /// The selected item.
        /// </summary>
        public BaseSelectableItem SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        /// <summary>
        /// Indicates whether or not the selection must be confirmed.
        /// </summary>
        public bool RequireSelectionConfirmation { get => _requireSelectionConfirmation; set => SetProperty(ref _requireSelectionConfirmation, value); }
        #endregion

        #region Methods
        protected override void CopyFromWorkflowStep(WorkflowStep workflowStep)
        {
            base.CopyFromWorkflowStep(workflowStep);

            if (workflowStep is BaseSelectionWorkflowStep baseSelectionWorkflowStep)
            {
                Selections = baseSelectionWorkflowStep.Selections;
                SelectedItem = baseSelectionWorkflowStep.SelectedItem;
                RequireSelectionConfirmation = baseSelectionWorkflowStep.RequireSelectionConfirmation;
            }
        }
        #endregion
    }
}

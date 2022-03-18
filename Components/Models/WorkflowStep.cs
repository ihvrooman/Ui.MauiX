using Components.Enums;
using Components.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Components.Models
{
    public class WorkflowStep : NotifyPropertyChanged
    {
        #region Fields
        private WorkflowStepType _type = WorkflowStepType.Info;
        private string _name;
        private string _description;
        private string _dateTimeStamp;
        private string _userName;
        private bool _isCompleted;
        private string _iconResourceId;
        private bool _autoAdvanceSubSteps;
        private bool _isEnabled = true;
        private string _completedText = "";
        private ObservableCollection<WorkflowStep> _subSteps = new ObservableCollection<WorkflowStep>();
        #endregion

        #region Properties
        /// <summary>
        /// The <see cref="WorkflowStepType"/>.
        /// </summary>
        public WorkflowStepType Type { get => _type; set { SetProperty(ref _type, value); } }

        /// <summary>
        /// The name of the step.
        /// </summary>
        public string Name { get => _name; set { SetProperty(ref _name, value); } }

        /// <summary>
        /// The display text of the completed step.
        /// </summary>
        public string CompletedText { get => _completedText; set { SetProperty(ref _completedText, value); } }

        /// <summary>
        /// The description of the step.
        /// </summary>
        public string Description { get => _description; set { SetProperty(ref _description, value); } }

        /// <summary>
        /// The step timestamp.
        /// </summary>
        public string DateTimeStamp { get => _dateTimeStamp; set { SetProperty(ref _dateTimeStamp, value); } }

        /// <summary>
        /// The username of the user who completed the step.
        /// </summary>
        public string UserName { get => _userName; set { SetProperty(ref _userName, value); } }

        /// <summary>
        /// Indicates whether or not the step has been completed.
        /// </summary>
        public bool IsCompleted { get => _isCompleted; set { SetProperty(ref _isCompleted, value); } }

        /// <summary>
        /// The resource id used to render the step's icon.
        /// </summary>
        public string IconResourceId { get => _iconResourceId; set => SetProperty(ref _iconResourceId, value); }

        /// <summary>
        /// Indicates whether or not the step is enabled.
        /// </summary>
        public bool IsEnabled { get => _isEnabled; set => SetProperty(ref _isEnabled, value); }

        /// <summary>
        /// Determines whether or not the application will automatically advance to the next sub step after the current sub step is completed by the user.
        /// </summary>
        public bool AutoAdvanceSubSteps { get => _autoAdvanceSubSteps; set => SetProperty(ref _autoAdvanceSubSteps, value); }

        /// <summary>
        /// The collection of <see cref="WorkflowStep"/>s that are sub steps of the current <see cref="WorkflowStep"/>.
        /// </summary>
        public ObservableCollection<WorkflowStep> SubSteps { get => _subSteps; set { SetProperty(ref _subSteps, value); } }
        #endregion

        #region Methods
        protected virtual void CopyFromWorkflowStep(WorkflowStep workflowStep)
        {
            Type = workflowStep.Type;
            Name = workflowStep.Name;
            Description = workflowStep.Description;
            DateTimeStamp = workflowStep.DateTimeStamp;
            UserName = workflowStep.UserName;
            IsCompleted = workflowStep.IsCompleted;
            IconResourceId = workflowStep.IconResourceId;
            SubSteps = workflowStep.SubSteps;
        }
        #endregion
    }
}

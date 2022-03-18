using Components.Helpers;
using Components.Models;
using Ui.MauiX.Enums;
using Ui.MauiX.Helpers;
using Ui.MauiX.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ui.MauiX.CustomControls.ViewModels
{
    public abstract class BaseWorkflowViewModel : NotifyPropertyChanged
    {
        #region Fields
        private ObservableCollection<WorkflowStep> _steps = new ObservableCollection<WorkflowStep>();
        private WorkflowStep _currentStep;
        private WorkflowStep _currentSubStep;
        private WorkflowType _workflowType = WorkflowType.Process;
        #endregion

        #region Properties
        public ObservableCollection<WorkflowStep> Steps { get => _steps; private set { SetProperty(ref _steps, value); } }

        public WorkflowStep CurrentStep { get => _currentStep; set => SetProperty(ref _currentStep, value); }

        public WorkflowStep CurrentSubStep { get => _currentSubStep; set => SetProperty(ref _currentSubStep, value); }

        public WorkflowType WorkflowType { get => _workflowType; set => SetProperty(ref _workflowType, value); }

        public double CurrentStepViewHeight { get; set; } = 300;

        public bool AutoScrollLastItemIntoView { get; set; }
        #endregion

        #region Methods
        protected virtual async Task OnAddStep(WorkflowStep step)
        {
            await Device.InvokeOnMainThreadAsync(() =>
            {
                Steps.Add(step);
            });

            CurrentStep = step;
        }

        protected virtual async Task OnAddSubStep(WorkflowStep subStep)
        {
            CurrentSubStep = subStep;
            await Device.InvokeOnMainThreadAsync(() =>
            {
                CurrentStep.SubSteps.Add(subStep);
            });
        }

        protected virtual async Task OnClearSteps()
        {
            await Device.InvokeOnMainThreadAsync(() =>
            {
                Steps.Clear();
            });
        }

        /// <summary>
        /// Waits for the given step to be completed.
        /// <para>Note: If the step is null, this method will immediately return.</para>
        /// </summary>
        /// <param name="step">The step of interest.</param>
        /// <returns></returns>
        protected async Task WaitForStepToBeCompleted(WorkflowStep step)
        {
            if (step == null)
            {
                return;
            }

            while (!step.IsCompleted)
            {
                await Task.Delay(50);
            }
        }

        /// <summary>
        /// Waits for the given step to be completed.
        /// <para>Note: If the step is null, this method will immediately return.</para>
        /// </summary>
        /// <param name="step">The step of interest.</param>
        /// <param name="timeoutInMilliseconds">The timeout period.</param>
        /// <returns></returns>
        protected async Task WaitForStepToBeCompleted(WorkflowStep step, long timeoutInMilliseconds = 5000)
        {
            if (step == null)
            {
                return;
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (!step.IsCompleted && stopwatch.ElapsedMilliseconds < timeoutInMilliseconds)
            {
                await Task.Delay(50);
            }

            stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds >= timeoutInMilliseconds)
            {
                throw new TimeoutException();
            }
        }

        protected virtual Task RunWorkflow()
        {
            throw new NotImplementedException();
        }

        protected void MarkStepAsCompleted(WorkflowStep step)
        {
            if (step == null)
            {
                return;
            }

            step.DateTimeStamp = DateTime.Now.ToString();
            step.IsCompleted = true;
        }

        protected void MarkCurrentStepAsCompleted()
        {
            MarkStepAsCompleted(CurrentStep);
        }

        protected void MarkCurrentSubStepAsCompleted()
        {
            MarkStepAsCompleted(CurrentSubStep);
        }
        #endregion
    }
}

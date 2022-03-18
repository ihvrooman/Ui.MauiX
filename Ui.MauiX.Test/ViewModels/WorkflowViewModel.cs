using Components.Enums;
using Components.Models;
using Ui.MauiX.CustomControls.ViewModels;
using Ui.MauiX.Enums;
using Ui.MauiX.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Ui.MauiX.Test.ViewModels
{
    public class WorkflowViewModel : BaseWorkflowViewModel
    {
        #region Constructor
        public WorkflowViewModel()
        {
            WorkflowType = WorkflowType.Menu;
            RunWorkflow();
        }
        #endregion

        #region Methods
        protected override async Task RunWorkflow()
        {
            await Task.Run(async () =>
            {
                var summaryStep = new WorkflowStep()
                {
                    Type = WorkflowStepType.MenuSummary,
                    AutoAdvanceSubSteps = false,
                    SubSteps = new ObservableCollection<WorkflowStep>()
                    {
                        new WorkflowStep()
                        {
                            Type = WorkflowStepType.MenuSummary,
                            Name = "Database Connection",
                            AutoAdvanceSubSteps = false,
                            SubSteps = new ObservableCollection<WorkflowStep>()
                            {
                                new WorkflowStep()
                                {
                                    Name = "Connect"
                                },
                                new WorkflowStep
                                {
                                    Name = "Connect 2",
                                },
                            }
                        },
                        //new WorkflowStep()
                        //{
                        //    Type = WorkflowStepType.MenuSummary,
                        //    Name = "Device Setup",
                        //    AutoAdvanceSubSteps = false,
                        //    SubSteps = new ObservableCollection<WorkflowStep>()
                        //    {
                        //        new WorkflowStep
                        //        {
                        //            Name = "Step 1",
                        //        },
                        //        new WorkflowStep
                        //        {
                        //            Name = "Step 2",
                        //        },
                        //    }
                        //},
                          new WorkflowStep()
                          {
                              Name = "Connect"
                          },
                        //new WorkflowStep
                        //{
                        //    Name = "Connect 2",
                        //},
                    }
                };

                await OnAddStep(summaryStep);
            });
        }
        #endregion
    }
}

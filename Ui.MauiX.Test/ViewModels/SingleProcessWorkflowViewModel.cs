using Components.Enums;
using Components.Models;
using Ui.MauiX.CustomControls.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ui.MauiX.Test.ViewModels
{
    public class SingleProcessWorkflowViewModel : BaseProcessWorkflowViewModel
    {
        #region Contructor
        public SingleProcessWorkflowViewModel() : base(WorkflowStepType.HeaderSummary)
        {
            RunWorkflow();
        }
        #endregion

        #region Methods
        protected override async Task RunWorkflow()
        {
            await Task.Run(async () =>
            {
                var step = new WorkflowStep()
                {
                    Name = "Start Test",
                    IsCompleted = true,
                };
                step.SubSteps.Add(new WorkflowStep()
                {
                    Type = WorkflowStepType.TimeStamp,
                    Name = "Start Test",
                    UserName = "Default User",
                    DateTimeStamp = DateTime.Now.ToString(),
                    IsCompleted = true,
                });
                await OnAddStep(step);

                for (int i = 1; i <= 10; i++)
                {
                    var name = $"Vessel Temperatures {i}";
                    step = new WorkflowStep()
                    {
                        Name = name,
                    };

                    var subStep = new WorkflowStep()
                    {
                        Type = WorkflowStepType.UserInteraction,
                        Description = $"Please confirm.",
                        Name = name,
                        UserName = "Default User",
                        IconResourceId = "Baskets.png",
                    };
                    step.SubSteps.Add(subStep);

                    await OnAddStep(step);

                    await WaitForStepToBeCompleted(subStep);

                    MarkCurrentStepAsCompleted();
                }
            });
        }
        #endregion
    }
}

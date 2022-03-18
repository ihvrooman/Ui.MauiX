using SkiaSharp;
using Components.Enums;
using Components.Models;
using Ui.Helpers;
using Ui.MauiX.CustomControls.ViewModels;
using Ui.MauiX.Enums;
using Ui.MauiX.Helpers;
using Ui.MauiX.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Ui.MauiX.Test.ViewModels
{
    public class ProcessWorkflowViewModel : BaseProcessWorkflowViewModel
    {
        #region Contructor
        public ProcessWorkflowViewModel()
        {
            RunWorkflow();
        }
        #endregion

        #region Methods
        protected override async Task RunWorkflow()
        {
            await Task.Run(async () =>
            {
                for (int i = 1; i <= 10; i++)
                {
                    WorkflowStep currentSubStep = null;
                    await OnAddStep(new WorkflowStep() { Name = $"Process {i}", IconResourceId = "DeviceInfo.png", });

                    await Task.Delay(500);
                    currentSubStep = new WorkflowStep()
                    {
                        Type = WorkflowStepType.UserInteraction,
                        Description = $"Please confirm.",
                        Name = $"Vessel Temperatures",
                        UserName = "Default User",
                        IconResourceId = "Baskets.png",
                    };
                    await OnAddSubStep(currentSubStep);

                    await WaitForStepToBeCompleted(currentSubStep);

                    await Task.Delay(500);
                    currentSubStep = new SelectionWorkflowStep()
                    {
                        Type = WorkflowStepType.Selection,
                        Description = "Please select item",
                        Name = "Item selected",
                        UserName = "Default User",
                        Selections = new List<BaseSelectableItem>()
                        {
                            new BaseSelectableItem(){DisplayText = "Item 1"},
                            new BaseSelectableItem(){DisplayText = "Item 2"},
                            new BaseSelectableItem(){DisplayText = "Item 3"},
                        },
                    };
                    await OnAddSubStep(currentSubStep);

                    await WaitForStepToBeCompleted(currentSubStep);

                    await Task.Delay(100);
                    currentSubStep = new WorkflowStep()
                    {
                        Type = WorkflowStepType.TimeStamp,
                        Name = $"Process {i} Started",
                        UserName = "Default User",
                        DateTimeStamp = DateTime.Now.ToString(),
                        IsCompleted = true
                    };
                    await OnAddSubStep(currentSubStep);

                    for (int j = 1; j <= 2; j++)
                    {
                        await Task.Delay(500);
                        currentSubStep = new WorkflowStep()
                        {
                            Type = WorkflowStepType.Info,
                            Description = $"Step {j} is running...",
                            Name = $"Step {j}",
                            UserName = "Default User",
                        };

                        await OnAddSubStep(currentSubStep);

                        await Task.Delay(1000);
                        currentSubStep.IsCompleted = true;
                        currentSubStep.DateTimeStamp = DateTime.Now.ToString();
                    }

                    await Task.Delay(500);
                    var graphStep = new GraphWorkflowStep()
                    {
                        Type = WorkflowStepType.Graph,
                        Name = "UV Analysis",
                        UserName = "Default User",
                        Description = "Running Extraction...",
                    };
                    graphStep.GraphSettings.DataSeriesRenderingInfo[0].Color = SKColors.Blue;
                    graphStep.GraphSettings.XAxisLabel = "Wavelength [nm]";
                    graphStep.GraphSettings.YAxisLabel = "Absorbance";
                    graphStep.GraphSettings.IsXAxisLabelVisible = true;
                    graphStep.GraphSettings.IsYAxisLabelVisible = true;
                    graphStep.GraphSettings.Title = "UV Analysis Results";
                    await OnAddSubStep(graphStep);

                    //Add graph data
                    graphStep.DataSeries.Add(new ObservableCollection<SKPoint>());
                    graphStep.DataSeries.Add(new ObservableCollection<SKPoint>());
                    await Task.WhenAll(Task.Run(async () =>
                    {
                        for (int x = 0; x <= 50; x += 5)
                        {
                            var inverseX = 100 - x;
                            var y = (float)((-Math.Pow(inverseX, 4) / 1000000) + 100);
                            graphStep.DataSeries[0].Add(new SKPoint(x, y));
                            await Task.Delay(500);
                        }
                    }), Task.Run(async () =>
                    {
                        for (int x = 0; x <= 50; x += 5)
                        {
                            var inverseX = 100 - x;
                            var y = (float)((-Math.Pow(inverseX, 4) / 1000000) + 100) + 5;
                            graphStep.DataSeries[1].Add(new SKPoint(x, y));
                            await Task.Delay(800);
                        }
                    }));
                    await Task.Delay(250);
                    graphStep.DateTimeStamp = DateTime.Now.ToString();
                    //graphStep.PreserveAspectRatio = false;
                    graphStep.IsCompleted = true;

                    await Task.Delay(500);
                    currentSubStep = new ValidateWorkflowStep()
                    {
                        Type = WorkflowStepType.Scan,
                        ValidationValue = "00145",
                        Description = $"Please scan sample.",
                        Name = $"Scan Sample",
                        UserName = "Default User",
                    };
                    await OnAddSubStep(currentSubStep);

                    await WaitForStepToBeCompleted(currentSubStep);

                    await Task.Delay(500);
                    currentSubStep = new WorkflowStep()
                    {
                        Type = WorkflowStepType.TimeStamp,
                        Name = $"Process {i} Finished",
                        UserName = "Default User",
                        DateTimeStamp = DateTime.Now.ToString(),
                        IsCompleted = true
                    };
                    await OnAddSubStep(currentSubStep);

                    await Task.Delay(2000);
                    CurrentStep.IsCompleted = true;
                    await Task.Delay(500);
                }
            });
        }
        #endregion
    }
}

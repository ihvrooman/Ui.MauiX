using SkiaSharp;
using Components.Enums;
using Components.Models;
using Ui.MauiX.Enums;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Ui.MauiX.Models
{
    public class GraphWorkflowStep : WorkflowStep
    {
        #region Fields
        private ObservableCollection<ObservableCollection<SKPoint>> _dataSeries = new ObservableCollection<ObservableCollection<SKPoint>>();
        private GraphSettings _graphSettings = new GraphSettings();
        #endregion

        #region Properties
        public ObservableCollection<ObservableCollection<SKPoint>> DataSeries { get => _dataSeries; set { SetProperty(ref _dataSeries, value); } }

        public GraphSettings GraphSettings { get => _graphSettings; set { SetProperty(ref _graphSettings, value); } }
        #endregion

        #region Constructor
        public GraphWorkflowStep()
        {
            Type = WorkflowStepType.Graph;
        }

        public GraphWorkflowStep(WorkflowStep workflowStep)
        {
            if (workflowStep.Type != WorkflowStepType.Graph)
            {
                throw new ArgumentException("Invalid WorkflowStepType", nameof(workflowStep.Type));
            }

            CopyFromWorkflowStep(workflowStep);
        }
        #endregion
    }
}

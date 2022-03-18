using Components.Enums;
using Components.Models;
using Ui.MauiX.CustomControls;
using Ui.MauiX.Enums;
using Ui.MauiX.Models;
using System;
using Xamarin.Forms;

namespace Ui.MauiX.Helpers
{
    public class WorkflowStepTypeTemplateSelector : DataTemplateSelector
    {
        public DataTemplate WorkflowStepTemplate { get; set; }

        public DataTemplate ScanTemplate { get; set; }

        public DataTemplate GraphTemplate { get; set; }

        public DataTemplate SelectionTemplate { get; set; }

        public DataTemplate MultiAlphaNumericInputTemplate { get; set; }

        public DataTemplate ExpanderSummaryTemplate { get; set; }

        public DataTemplate HeaderSummaryTemplate { get; set; }

        public DataTemplate MenuSummaryTemplate { get; set; }

        public DataTemplate ManualEntryTemplate { get; set; }

        public DataTemplate UserInteractionTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch (((WorkflowStep)item).Type)
            {
                case WorkflowStepType.Scan:
                    return ScanTemplate;
                case WorkflowStepType.Graph:
                    return GraphTemplate;
                case WorkflowStepType.Selection:
                    return SelectionTemplate;
                case WorkflowStepType.MultiAlphaNumericInput:
                    return MultiAlphaNumericInputTemplate;
                case WorkflowStepType.ExpanderSummary:
                    return ExpanderSummaryTemplate;
                case WorkflowStepType.HeaderSummary:
                    return HeaderSummaryTemplate;
                case WorkflowStepType.MenuSummary:
                    return MenuSummaryTemplate;
                case WorkflowStepType.ManualEntry:
                    return ManualEntryTemplate;
                case WorkflowStepType.UserInteraction:
                    return UserInteractionTemplate;

                default:
                    return WorkflowStepTemplate;
            }
        }

        public static Type GetViewTypeFromWorkflowStep(WorkflowStep workflowStep)
        {
            /* This method is used by some controls like the WorkflowMenu that
             * display a workflow step cell. For the sake of keeping all of the
             * selection-from-steptype code in one place, I've put this method 
             * here. That way when more step types are added we can add the new
             * templates and cell selections in one place rather than several.
             */
            switch (workflowStep.Type)
            {
                case WorkflowStepType.Scan:
                    return typeof(ScanWorkflowStepCell);
                case WorkflowStepType.Graph:
                    return typeof(GraphWorkflowStepCell);
                case WorkflowStepType.Selection:
                    return typeof(SelectionWorkflowStepCell);
                case WorkflowStepType.MultiAlphaNumericInput:
                    return typeof(MultiAlphaNumericInputWorkflowStepCell);
                case WorkflowStepType.MenuSummary:
                    return typeof(WorkflowMenu);
                case WorkflowStepType.ManualEntry:
                    return typeof(ManualEntryWorkflowStepCell);
                case WorkflowStepType.UserInteraction:
                    return typeof(UserInteractionWorkflowStepCell);

                default:
                    return typeof(WorkflowStepCell);
            }
        }
    }
}

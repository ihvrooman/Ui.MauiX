using System;
using System.Collections.Generic;
using System.Text;

namespace Components.Enums
{
    /// <summary>
    /// Indicates the type of the workflow step.
    /// </summary>
    public enum WorkflowStepType
    {
        /// <summary>
        /// Indicates that the step requires user interaction.
        /// </summary>
        UserInteraction,

        /// <summary>
        /// Indicates that the step is informative.
        /// </summary>  
        Info,

        /// <summary>
        /// Indicates that the step displays a timestamp.
        /// </summary>
        TimeStamp,

        /// <summary>
        /// Indicates that the step requires the user to scan an item.
        /// </summary>
        Scan,

        /// <summary>
        /// Indicates that the step shows data plotted on a graph.
        /// </summary>
        Graph,

        /// <summary>
        /// Indicates that the step requires the user to select an item.
        /// </summary>
        Selection,

        /// <summary>
        /// Indicates that the step requires the user to enter one or more alpha-numeric values.
        /// </summary>
        MultiAlphaNumericInput,

        /// <summary>
        /// Indicates that the step is a summary step requiring expanders in the ui.
        /// </summary>
        ExpanderSummary,

        /// <summary>
        /// Indicates that the step is a summary step with a menu.
        /// </summary>
        MenuSummary,

        /// <summary>
        /// Indicates that the step displays results.
        /// </summary>
        Results,

        /// <summary>
        /// Indicates the step that requires manual string entry by the user
        /// </summary>
        ManualEntry,

        /// <summary>
        /// Indicates that the step is a summary step requiring headers in the ui.
        /// </summary>
        HeaderSummary,
    }
}

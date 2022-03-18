using Components.Models;
using Ui.Helpers;
using Ui.MauiX.Helpers;
using Ui.MauiX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleWorkflowStepView : ContentView
    {
        #region Properties
        public WorkflowStep WorkflowStep { get => (WorkflowStep)GetValue(WorkflowStepProperty); set => SetValue(WorkflowStepProperty, value); }

        public static readonly BindableProperty WorkflowStepProperty = BindableProperty.Create(nameof(WorkflowStep), typeof(WorkflowStep), typeof(SingleWorkflowStepView), propertyChanged: (bindable, oldValue, newValue) =>
        {
            ((SingleWorkflowStepView)bindable).UpdateWorkflowView();
        });
        #endregion

        #region Constructor
        public SingleWorkflowStepView()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void UpdateWorkflowView()
        {
            WorkflowView.Content = WorkflowStep == null ? null : (View)CustomActivator.GetInstance(WorkflowStepTypeTemplateSelector.GetViewTypeFromWorkflowStep(WorkflowStep));
            WorkflowView.Content.BindingContext = WorkflowStep;
        }
        #endregion
    }
}
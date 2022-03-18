using Components.Models;
using Ui.MauiX.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkflowStepCell : ContentView
    {
        #region Properties
        private WorkflowStep WorkflowStep => (WorkflowStep)BindingContext;
        #endregion

        #region Constructor
        public WorkflowStepCell()
        {
            InitializeComponent();
        }
        #endregion
    }
}
using Components.Models;
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
    public partial class SelectionWorkflowStepCell : ContentView
    {
        #region Properties
        private SelectionWorkflowStep WorkflowStep => (SelectionWorkflowStep)BindingContext;
        #endregion

        #region Constructor

        public SelectionWorkflowStepCell()
        {
            InitializeComponent();

            itemsList.SelectedItemsChanged += OnSelectedItemsChanged;
        }

        #endregion

        #region Methods

        private void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            CompleteStep();
        }

        private void CompleteStep()
        {
            WorkflowStep.IsCompleted = true;
            WorkflowStep.DateTimeStamp = DateTime.Now.ToString();
        }

        #endregion

        #region Event Handlers

        private void OnSelectedItemsChanged(object sender, IEnumerable<object> items)
        {
            WorkflowStep.SelectedItem = (BaseSelectableItem)items.FirstOrDefault();
            if (!WorkflowStep.RequireSelectionConfirmation)
            {
                CompleteStep();
            }
        }

        #endregion
    }
}
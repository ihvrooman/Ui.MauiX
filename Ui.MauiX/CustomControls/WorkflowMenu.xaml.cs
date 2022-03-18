using Components.Models;
using Ui.Enums;
using Ui.MauiX.Enums;
using Ui.MauiX.Helpers;
using Ui.MauiX.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkflowMenu : ContentViewWithSetPropertyMethod
    {
        #region Fields
        private List<CustomMenuItem> _customMenuItems;
        private int _currentStepIndex;
        #endregion

        #region Properties
        private WorkflowStep WorkflowStep => BindingContext is WorkflowStep w ? w : null;

        public List<CustomMenuItem> CustomMenuItems { get => _customMenuItems; private set => SetProperty(ref _customMenuItems, value); }

        public Alignment MenuAlignment { get => (Alignment)GetValue(MenuAlignmentProperty); set => SetValue(MenuAlignmentProperty, value); }

        public static readonly BindableProperty MenuAlignmentProperty = BindableProperty.Create(nameof(MenuAlignment), typeof(Alignment), typeof(WorkflowMenu), Alignment.Left);
        #endregion

        #region Constructor
        public WorkflowMenu()
        {
            InitializeComponent();
            BindingContextChanged += WorkflowMenu_BindingContextChanged;
            Task.Run(async () =>
            {
                /* This task monitors the current workflow step. Once the current
                 * workflow step has been completed, it will enable the next
                 * step.
                 */
                while (true)
                {
                    var currentStep = WorkflowStep?.SubSteps?[_currentStepIndex];
                    while (currentStep != null && !currentStep.IsCompleted)
                    {
                        await Task.Delay(50);
                    }

                    if (currentStep != null && CustomMenuItems != null)
                    {
                        if (CustomMenuItems.Count > _currentStepIndex + 1)
                        {
                            var nextMenuItem = CustomMenuItems[++_currentStepIndex];

                            nextMenuItem.IsEnabled = true;

                            if (WorkflowStep.AutoAdvanceSubSteps)
                            {
                                MainTabView.NavigateToCustomMenuItem(nextMenuItem);
                            }
                        }
                        else if (_currentStepIndex == WorkflowStep.SubSteps.Count - 1)
                        {
                            //If the last sub step is complete, mark the whole workflow step as complete
                            WorkflowStep.IsCompleted = true;
                            break;
                        }
                    }
                }
            });
        }
        #endregion

        #region Methods
        private void WorkflowMenu_BindingContextChanged(object sender, EventArgs e)
        {
            SetCustomMenuItems();
        }

        private void TabView_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
        {
            var workflowStep = WorkflowStep.SubSteps[((TabView)sender).SelectedTabMenuItemIndex];
            e.NewSelectedTabView.BindingContext = workflowStep;
        }

        private void SetCustomMenuItems()
        {
            var customMenuItems = new List<CustomMenuItem>();
            WorkflowStep?.SubSteps?.ForEach(s => customMenuItems.Add(new CustomMenuItem()
            {
                IconResourceId = s.IconResourceId,
                Title = s.Name,
                TargetType = WorkflowStepTypeTemplateSelector.GetViewTypeFromWorkflowStep(s),
                IsEnabled = s == WorkflowStep?.SubSteps?.FirstOrDefault(),
            }));
            _currentStepIndex = 0;
            CustomMenuItems = customMenuItems;

            #if DEBUG
            if (Debugger.IsAttached)
            {
                Debug.WriteLine($"** Setting WorkflowMenu.CustomMenuItems.");
            }
            #endif
        }
        #endregion
    }
}
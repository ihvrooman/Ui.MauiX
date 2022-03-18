using Components.Models;
using Ui.Enums;
using Ui.MauiX.CustomControls.ViewModels;
using Ui.MauiX.Enums;
using Ui.MauiX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Components;

namespace Ui.MauiX.Test.ViewModels
{
    public class ChatWorkflowViewModel : BaseChatWorkflowViewModel
    {
        #region Methods
        public ChatWorkflowViewModel()
        {
            RunWorkflow();
        }
        #endregion

        #region Methods
        protected override async Task RunWorkflow()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    var step1 = new SelectionWorkflowStep()
                    {
                        Selections = new List<BaseSelectableItem>()
                        {
                            new BaseSelectableItem()
                            {
                                DisplayText = "Choice 1",
                            },
                            new BaseSelectableItem()
                            {
                                DisplayText = "Choice 2",
                            },
                        },
                        RequireSelectionConfirmation = false,
                        SelectionWorkflowStyle = SelectionWorkflowStyle.StandardButton,
                    };
                    step1.Settings.ButtonSize = 50;
                    step1.Settings.ButtonShape = ButtonShape.Pill;
                    step1 = (SelectionWorkflowStep)await SetCurrentStepAndWaitForUserToCompleteIt(step1, "What would you like to do?");
                    var selectedItem = step1.SelectedItem;
                    AddMessageToChat(ChatMessageType.UserSourced, selectedItem.DisplayText);

                    if (selectedItem.DisplayText == "Choice 2")
                    {
                        var step2 = new MultiAlphaNumericInputWorkflowStep();
                        step2.InputInfo.Add(new AlphaNumericWorkflowInfo()
                        {
                            IconResourceId = "DeviceInfo.png",
                            ValueSuffix = "g",
                            Label = "Weight",
                            Value = "Starting Weight",
                        });
                        step2.InputInfo.Add(new NumericWorkflowInfo()
                        {
                            IconResourceId = "DeviceInfo.png",
                            ValueSuffix = "kg",
                            Label = "Weight",
                            Value = 144.6,
                            Min = 0,
                            Max = 200,
                            MaxNumberOfDecimalPlaces = 1,
                            MinNumberOfDecimalPlaces = 1,
                        });
                        step2.InputInfo.Add(new AlphaNumericWorkflowInfo()
                        {
                            IconResourceId = "DeviceInfo.png",
                            Label = "Name",
                            Value = "John Doe",
                        });

                        step2 = (MultiAlphaNumericInputWorkflowStep)await SetCurrentStepAndWaitForUserToCompleteIt(step2, "Please fill out this form and the doctor will be right with you.", "Thank you.");

                        AddMessageToChat(ChatMessageType.UserSourced, "I'm finished with the form.");
                        AddMessageToChat(ChatMessageType.NonUserSourced, $"{step2.InputInfo[2].Value}, the doctor will see you now.");
                    }
                }
            });
        }
        #endregion
    }
}

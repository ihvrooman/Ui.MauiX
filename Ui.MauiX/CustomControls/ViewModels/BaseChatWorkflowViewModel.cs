using Components.Models;
using Ui.MauiX.Enums;
using Ui.MauiX.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ui.MauiX.CustomControls.ViewModels
{
    public abstract class BaseChatWorkflowViewModel : BaseWorkflowViewModel
    {
        #region Fields
        private ObservableCollection<ChatMessage> _messages = new ObservableCollection<ChatMessage>();
        #endregion

        #region Properties
        public ObservableCollection<ChatMessage> Messages { get => _messages; private set => SetProperty(ref _messages, value); }
        #endregion

        #region Constructor
        public BaseChatWorkflowViewModel()
        {
            WorkflowType = WorkflowType.Chat;
            AutoScrollLastItemIntoView = true;
        }
        #endregion

        #region Methods
        protected async Task<WorkflowStep> SetCurrentStepAndWaitForUserToCompleteIt(WorkflowStep step, string chatPrompt, string chatReply = null)
        {
            AddMessageToChat(ChatMessageType.NonUserSourced, chatPrompt);
            Device.BeginInvokeOnMainThread(() =>
            {
                CurrentStep = step;
            });            
            await WaitForStepToBeCompleted(step);
            if (chatReply != null)
            {
                AddMessageToChat(ChatMessageType.UserSourced, chatReply);
            }
            return step;
        }

        protected void AddMessageToChat(ChatMessageType messageType, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Messages.Add(new ChatMessage() { Type = messageType, Message = message });
            });
        }

        protected void ClearChatMessages()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Messages.Clear();
            });
        }
        #endregion
    }
}

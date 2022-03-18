using Ui.MauiX.Enums;
using Ui.MauiX.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Ui.MauiX.Helpers
{
    public class ChatMessageTypeTemplateSelector : DataTemplateSelector
    {
        public DataTemplate UserMessageDataTemplate { get; set; }
        public DataTemplate NonUserMessageDataTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch (((ChatMessage)item).Type)
            {
                case ChatMessageType.UserSourced:
                    return UserMessageDataTemplate;
                case ChatMessageType.NonUserSourced:
                    return NonUserMessageDataTemplate;
                default:
                    return null;
            }
        }
    }
}

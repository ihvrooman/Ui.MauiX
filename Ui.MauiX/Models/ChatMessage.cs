using Components.Helpers;
using Ui.MauiX.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ui.MauiX.Models
{
    public class ChatMessage : NotifyPropertyChanged
    {
        #region Fields
        private ChatMessageType _type;
        private string _message;
        #endregion

        #region Properties
        public ChatMessageType Type { get => _type; set => SetProperty(ref _type, value); }

        public string Message { get => _message; set => SetProperty(ref _message, value); }
        #endregion
    }
}

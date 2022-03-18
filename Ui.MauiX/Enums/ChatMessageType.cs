using System;
using System.Collections.Generic;
using System.Text;
using Ui.MauiX.Models;

namespace Ui.MauiX.Enums
{
    public enum ChatMessageType
    {
        /// <summary>
        /// Indicates that the <see cref="ChatMessage"/> originated from the user.
        /// </summary>
        UserSourced,
        /// <summary>
        /// Indicates that the <see cref="ChatMessage"/> originated from a source other than the user.
        /// </summary>
        NonUserSourced,
    }
}

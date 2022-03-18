using System;
using System.Collections.Generic;
using System.Text;

namespace Ui.Enums
{
    /// <summary>
    /// Indicates the mode of a menu.
    /// </summary>
    public enum MenuMode
    {
        /// <summary>
        /// Indicates that the menu will pop over the rest of the UI.
        /// </summary>
        Popover,

        /// <summary>
        /// Indicates that the UI will be split on the portrait direction.
        /// </summary>
        SplitOnPortrait,
    }
}

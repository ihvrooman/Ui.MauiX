using System;
using System.Collections.Generic;
using System.Text;

namespace Ui.Enums
{
    /// <summary>
    /// Indicates the input mode of a control.
    /// </summary>
    public enum InputMode
    {
        /// <summary>
        /// Indicates that the input will be entered using alpha numeric values.
        /// </summary>
        AlphaNumberic,

        /// <summary>
        /// Indicates that the input will be entered as using only numeric values.
        /// </summary>
        Numeric,

        /// <summary>
        /// Indicates that the input will be toggled.
        /// </summary>
        Toggle,

        /// <summary>
        /// Indicates that the input will be selected from a collection of items.
        /// </summary>
        Selection,
    }
}

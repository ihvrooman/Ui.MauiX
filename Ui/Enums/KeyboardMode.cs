using System;
using System.Collections.Generic;
using System.Text;

namespace Ui.Enums
{
    /// <summary>
    /// Indicates the mode of a Keyboard.
    /// </summary>
    public enum KeyboardMode
    {
        /// <summary>
        /// Indicates that they keyboard is displaying uppercase letters.
        /// </summary>
        Uppercase,

        /// <summary>
        /// Indicates that they keyboard is displaying lowercase letters.
        /// </summary>
        Lowercase,

        /// <summary>
        /// Indicates that they keyboard is displaying numbers and special characters.
        /// </summary>
        NumbersAndSpecialCharacters,

        /// <summary>
        /// Indicates that the keyboard is locked in uppercase mode.
        /// </summary>
        CapsLock,

        /// <summary>
        /// Indicates that the keyboard is displaying additional special characters.
        /// </summary>
        AdditionalSpecialCharacters,
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Ui.Models
{
    /// <summary>
    /// The result from a popup.
    /// </summary>
    public class PopupResult
    {
        #region Properties
        /// <summary>
        /// Indicates whether or not the user cancelled the operation from the popup.
        /// </summary>
        public bool UserCancelled { get; private set; }
        /// <summary>
        /// The value returned from the popup.
        /// </summary>
        public object Value { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new <see cref="PopupResult"/>.
        /// </summary>
        /// <param name="userCancelled">Indicates whether or not the user cancelled the operation from the popup.</param>
        /// <param name="value">The value returned from the popup.</param>
        public PopupResult(bool userCancelled, object value)
        {
            UserCancelled = userCancelled;
            Value = value;
        }
        #endregion
    }
}

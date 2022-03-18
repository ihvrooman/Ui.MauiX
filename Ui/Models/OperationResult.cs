using System;
using System.Collections.Generic;
using System.Text;

namespace Ui.Models
{
    /// <summary>
    /// A class for recording the result of an operation.
    /// </summary>
    public class OperationResult
    {
        #region Properties
        /// <summary>
        /// Indicates whether or not the operation was successful.
        /// </summary>
        public bool Succeeded { get; set; }
        /// <summary>
        /// The value returned from the operation.
        /// </summary>
        public object Value { get; set; }
        #endregion
    }
}

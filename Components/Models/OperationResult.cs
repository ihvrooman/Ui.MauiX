using System;
using System.Collections.Generic;
using System.Text;

namespace Components.Models
{
    //
    // Summary:
    //     A class for recording the result of an operation.
    public class OperationResult
    {
        //
        // Summary:
        //     Indicates whether or not the operation was successful.
        public bool Succeeded { get; set; }
        //
        // Summary:
        //     The value returned from the operation.
        public object Value { get; set; }
    }
}

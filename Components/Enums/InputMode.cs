using System;
using System.Collections.Generic;
using System.Text;

namespace Components.Enums
{
    //
    // Summary:
    //     Indicates the input mode of a control.
    public enum InputMode
    {
        //
        // Summary:
        //     Indicates that the input will be entered using alpha numeric values.
        AlphaNumberic = 0,
        //
        // Summary:
        //     Indicates that the input will be entered as using only numeric values.
        Numeric = 1,
        //
        // Summary:
        //     Indicates that the input will be toggled.
        Toggle = 2,
        //
        // Summary:
        //     Indicates that the input will be selected from a collection of items.
        Selection = 3
    }
}

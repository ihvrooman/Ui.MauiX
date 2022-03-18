using Ui.MauiX.CustomControls;

namespace Ui.MauiX.Enums
{
    /// <summary>
    /// Indicates the button style of a <see cref="Dialog"/>.
    /// </summary>
    public enum DialogButtonStyle
    {
        /// <summary>
        /// Indicates that the <see cref="Dialog"/> will only have an affirmative button.
        /// </summary>
        Affirmative,

        /// <summary>
        /// Indicates that the <see cref="Dialog"/> will have an affirmative and negative button.
        /// </summary>
        AffirmativeAndNegative,

        /// <summary>
        /// Indicates that the <see cref="Dialog"/> will have an affirmative, negative, and one auxiliary button.
        /// </summary>
        AffirmativeAndNegativeAndSingleAuxiliary,

        /// <summary>
        /// Indicates that the <see cref="Dialog"/> will have an affirmative, negative, and two auxiliary buttons.
        /// </summary>
        AffirmativeAndNegativeAndDoubleAuxiliary,
    }
}

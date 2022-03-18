using Ui.MauiX.CustomControls;

namespace Ui.MauiX.Enums
{
    /// <summary>
    /// The result of a <see cref="Dialog"/>.
    /// <para>Note: This enum is also used to set the default button of a <see cref="Dialog"/>.</para>
    /// </summary>
    public enum DialogResult
    {
        /// <summary>
        /// A null result.
        /// </summary>
        Null,

        /// <summary>
        /// Indicates that the negative button was tapped by the user.
        /// </summary>
        Negative,

        /// <summary>
        /// Indicates that the affirmative button was tapped by the user.
        /// </summary>
        Affirmative,

        /// <summary>
        /// Indicates that the first auxiliary button was tapped by the user.
        /// </summary>
        FirstAuxiliary,

        /// <summary>
        /// Indicates that the second auxiliary button was tapped by the user.
        /// </summary>
        SecondAuxiliary,
    }
}

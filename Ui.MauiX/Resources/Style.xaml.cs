
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.Resources
{
    /// <summary>
    /// A common style sheet.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Style : ResourceDictionary
    {
        /// <summary>
        /// Creates a new <see cref="Style"/>.
        /// </summary>
        public Style()
        {
            InitializeComponent();
        }
    }
}
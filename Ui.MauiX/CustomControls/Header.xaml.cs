using Ui.MauiX.CustomControls.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    /// <summary>
    /// A reusable header.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Header : ContentView
    {
        #region Properties
        /// <summary>
        /// The control's viewModel.
        /// </summary>
        public BaseHeaderViewModel ViewModel
        {
            set
            {
                BindingContext = value;
            }
        }
        #endregion

        #region Constructor   
        /// <summary>
        /// Creates a new <see cref="Header"/>.
        /// </summary>
        public Header()
        {
            InitializeComponent();
        }
        #endregion
    }
}
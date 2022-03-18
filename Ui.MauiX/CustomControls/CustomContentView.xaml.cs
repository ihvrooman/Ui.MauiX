using Ui.MauiX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    /// <summary>
    /// A custom <see cref="ContentView"/> with a <see cref="CustomToolbarItems"/> property for use in a <see cref="CustomMasterDetailPage"/>.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomContentView : ContentView
    {
        #region Properties
        /// <summary>
        /// The <see cref="CustomToolbarItem"/>s for display in a <see cref="CustomMasterDetailPage"/>.
        /// </summary>
        public List<CustomToolbarItem> CustomToolbarItems { get; } = new List<CustomToolbarItem>();
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new <see cref="CustomContentView"/>.
        /// </summary>
        public CustomContentView()
        {
            InitializeComponent();
        }
        #endregion
    }
}
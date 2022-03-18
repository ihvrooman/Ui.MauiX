using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValueView : ContentView
    {
        #region Constructor
        public ValueView()
        {
            InitializeComponent();
        }
        #endregion
    }
}
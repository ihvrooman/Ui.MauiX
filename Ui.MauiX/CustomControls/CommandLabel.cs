using System.Windows.Input;
using Xamarin.Forms;

namespace Ui.MauiX.CustomControls
{
    public class CommandLabel : Label
    {
        #region Properties
        public ICommand Command { get; set; }
        public object CommandParameter { get; set; }
        #endregion

        #region Constructor
        public CommandLabel()
        {

        }
        #endregion
    }
}
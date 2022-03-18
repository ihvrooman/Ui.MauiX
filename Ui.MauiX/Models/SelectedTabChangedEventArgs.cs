using Xamarin.Forms;

namespace Ui.MauiX.Models
{
    public class SelectedTabChangedEventArgs
    {
        #region Properties
        public View OldSelectedTabView { get; private set; }
        public View NewSelectedTabView { get; private set; }
        #endregion

        #region Constructor
        public SelectedTabChangedEventArgs(View oldSelectedTabView, View newSelectedTabView)
        {
            OldSelectedTabView = oldSelectedTabView;
            NewSelectedTabView = newSelectedTabView;
        }
        #endregion
    }
}

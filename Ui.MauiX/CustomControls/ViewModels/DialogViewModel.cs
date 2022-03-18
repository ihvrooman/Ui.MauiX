using Ui.MauiX.Models;
using System.Windows.Input;
using Xamarin.Forms;
using Components.Helpers;
using Ui.MauiX.Enums;

namespace Ui.MauiX.CustomControls.ViewModels
{
    internal class DialogViewModel : NotifyPropertyChanged
    {
        #region Fields
        #region Commands
        private ICommand _setDialogResultCommand;
        #endregion
        private DialogSettings _dialogSettings;
        #endregion

        #region Properties
        #region Commands
        public ICommand SetDialogResultCommand
        {
            get
            {
                if (_setDialogResultCommand == null)
                {
                    _setDialogResultCommand = new Xamarin.Forms.Command<DialogResult>(SetDialogResult);
                }
                return _setDialogResultCommand;
            }
        }
        #endregion
        public DialogSettings DialogSettings { get => _dialogSettings; set { SetProperty(ref _dialogSettings, value); } }
        #endregion

        #region Methods
        private void SetDialogResult(DialogResult dialogResult)
        {
            DialogSettings.Result = dialogResult;
        }
        #endregion
    }
}

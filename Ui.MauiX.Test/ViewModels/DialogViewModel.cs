using Ui.MauiX.CustomControls;
using Ui.MauiX.Enums;
using Ui.MauiX.Models;
using Ui.MauiX.Test.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ui.MauiX.Test.ViewModels
{
    public class DialogViewModel : BaseViewModel
    {
        #region Fields
        private Command _showDialogCommand;
        private Command _resetDialogSettingsCommand;
        private DialogSettings _dialogSettings;
        private DialogResult _dialogResult = DialogResult.Null;
        #endregion

        #region Properties
        public List<DialogButtonStyle> AvailableButtonStyles { get; } = new List<DialogButtonStyle>()
        { 
            DialogButtonStyle.Affirmative,
            DialogButtonStyle.AffirmativeAndNegative,
            DialogButtonStyle.AffirmativeAndNegativeAndSingleAuxiliary,
            DialogButtonStyle.AffirmativeAndNegativeAndDoubleAuxiliary,
        };

        public List<DialogResult> AvailableDialogResults { get; } = new List<DialogResult>()
        {
            DialogResult.Affirmative,
            DialogResult.Negative,
            DialogResult.FirstAuxiliary,
            DialogResult.SecondAuxiliary,
            DialogResult.Null,
        };

        public Command ShowDialogCommand
        {
            get
            {
                if (_showDialogCommand == null)
                {
                    _showDialogCommand = new Command(ShowDialog);
                }
                return _showDialogCommand;
            }
        }

        public Command ResetDialogSettingsCommand
        {
            get
            {
                if (_resetDialogSettingsCommand == null)
                {
                    _resetDialogSettingsCommand = new Command(ResetDialogSettings);
                }
                return _resetDialogSettingsCommand;
            }
        }

        public DialogSettings DialogSettings { get => _dialogSettings; set => SetProperty(ref _dialogSettings, value); }

        public DialogResult DialogResult { get => _dialogResult; set => SetProperty(ref _dialogResult, value); }
        #endregion

        #region Constructor
        public DialogViewModel()
        {
            SupplementalPageControlTypes.Add(typeof(DialogSupplementalPageControls));

            ResetDialogSettings();

            base.PropertyChanged += BaseViewModel_PropertyChanged;
        }
        #endregion

        #region Methods
        private void BaseViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BorderThickness))
            {
                DialogSettings.ButtonBorderThickness = BorderThickness;
            }
            else if (e.PropertyName == nameof(CornerRadius))
            {
                DialogSettings.CornerRadius = CornerRadius;
            }
            else if (e.PropertyName == nameof(IconSize))
            {
                DialogSettings.IconSize = IconSize;
            }
            else if (e.PropertyName == nameof(FontSize))
            {
                DialogSettings.MessageFontsize = FontSize;
            }
            else if (e.PropertyName == nameof(AreIconsVisible) || e.PropertyName == nameof(IconResourceId))
            {
                DialogSettings.IconResourceId = AreIconsVisible ? IconResourceId : null;
            }
        }

        private async void ShowDialog(object obj)
        {
            DialogSettings.IsVisible = true; 
            while (DialogSettings.Result == DialogResult.Null)
            {
                await Task.Delay(50);
            }
            DialogSettings.IsVisible = false;
            DialogResult = DialogSettings.Result;
            DialogSettings.Result = DialogResult.Null;
        }

        private void ResetDialogSettings()
        {
            DialogSettings = new DialogSettings()
            {
                Title = "Example Title",
                Message = "This is an example message.",
                AffirmativeButtonText = "Ok",
            };

            CornerRadius = DialogSettings.CornerRadius;
            BorderThickness = DialogSettings.ButtonBorderThickness;
            DialogSettings.IconResourceId = IconResourceId;
            IconSize = DialogSettings.IconSize;
            FontSize = DialogSettings.MessageFontsize;

            DialogPopup.DefaultDialogSettings = DialogSettings;
        }
        #endregion
    }
}

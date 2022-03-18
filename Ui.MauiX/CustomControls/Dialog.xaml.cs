using Ui.MauiX.CustomControls.ViewModels;
using Ui.MauiX.Enums;
using Ui.MauiX.Models;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    /// <summary>
    /// A control for showing a message dialog to the user.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dialog : ContentView
    {
        #region Properties
        public DialogSettings DialogSettings
        {
            get
            {
                return (DialogSettings)GetValue(DialogSettingsProperty);
            }
            set
            {
                SetValue(DialogSettingsProperty, value);
            }
        }

        public static readonly BindableProperty DialogSettingsProperty =
  BindableProperty.Create(nameof(DialogSettings), typeof(DialogSettings), typeof(Dialog), new DialogSettings(), propertyChanged: DialogSettings_Changed);

        internal DialogViewModel ViewModel { get => (DialogViewModel)MainGrid.BindingContext; }
        #endregion

        #region Constructor
        public Dialog()
        {
            InitializeComponent();
            SetButtonColorBindings();
        }
        #endregion

        #region Methods
        private static void DialogSettings_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var dialog = (Dialog)bindable;
            var oldDialogSettings = (DialogSettings)oldValue;
            if (oldDialogSettings != null)
            {
                oldDialogSettings.PropertyChanged -= dialog.DialogSettings_PropertyChanged;
            }
            var newDialogSettings = (DialogSettings)newValue;
            if (newDialogSettings != null)
            {
                newDialogSettings.PropertyChanged += dialog.DialogSettings_PropertyChanged;
            }
            dialog.ViewModel.DialogSettings = newDialogSettings;
            dialog.SetButtonColorBindings();
        }

        private void DialogSettings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DialogSettings.DefaultButton))
            {
                SetButtonColorBindings();
            }
        }

        private void SetButtonColorBindings()
        {
            if (ViewModel?.DialogSettings == null)
            {
                return;
            }

            var buttons = new List<CustomButton>()
            {
                AffirmativeButton,
                NegativeButton,
                FirstAuxiliaryButton,
                SecondAuxiliaryButton
            };
            var defaultButton = AffirmativeButton;
            var buttonColorPath = string.Empty;
            var lightAccentButtonColorPath = "DialogSettings.LightAccentButtonColor";

            switch (ViewModel.DialogSettings.DefaultButton)
            {
                case DialogResult.Affirmative:
                    defaultButton = AffirmativeButton;
                    break;
                case DialogResult.Negative:
                    defaultButton = NegativeButton;
                    break;
                case DialogResult.FirstAuxiliary:
                    defaultButton = FirstAuxiliaryButton;
                    break;
                case DialogResult.SecondAuxiliary:
                    defaultButton = SecondAuxiliaryButton;
                    break;
                default:
                    defaultButton = null;
                    break;
            }

            buttons.ForEach(b =>
            {
                b.RemoveBinding(CustomButton.BackgroundColorProperty);
                b.RemoveBinding(CustomButton.BorderColorProperty);
                b.RemoveBinding(CustomButton.TextColorProperty);
                b.RemoveBinding(CustomButton.BorderThicknessProperty);

                if (b == AffirmativeButton)
                {
                    buttonColorPath = "AffirmativeButtonColor";
                }
                else if (b == NegativeButton)
                {
                    buttonColorPath = "NegativeButtonColor";
                }
                else if (b == FirstAuxiliaryButton)
                {
                    buttonColorPath = "FirstAuxiliaryButtonColor";
                }
                else if (b == SecondAuxiliaryButton)
                {
                    buttonColorPath = "SecondAuxiliaryButtonColor";
                }
                buttonColorPath = "DialogSettings." + buttonColorPath;

                b.SetBinding(CustomButton.BorderColorProperty, buttonColorPath);
                b.SetBinding(CustomButton.BorderThicknessProperty, "DialogSettings.ButtonBorderThickness");
                if (b == defaultButton)
                {
                    b.SetBinding(CustomButton.BackgroundColorProperty, buttonColorPath);
                    b.SetBinding(CustomButton.TextColorProperty, lightAccentButtonColorPath);
                }
                else
                {
                    b.SetBinding(CustomButton.BackgroundColorProperty, lightAccentButtonColorPath);
                    b.SetBinding(CustomButton.TextColorProperty, buttonColorPath);
                }
            });
        }
        #endregion
    }
}
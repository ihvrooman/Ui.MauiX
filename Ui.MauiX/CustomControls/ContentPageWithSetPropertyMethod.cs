using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Xamarin.Forms;

namespace Ui.MauiX.CustomControls
{
    public class ContentPageWithSetPropertyMethod : ContentPage
    {
        #region Methods
        protected bool SetProperty<T>(ref T backingField, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingField, newValue))
            {
                return false;
            }
            backingField = newValue;
            OnPropertyChanged(propertyName);

            return true;
        }
        #endregion
    }
}
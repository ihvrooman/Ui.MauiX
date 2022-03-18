using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Components.Helpers
{
    /// <summary>
    /// An implementation of the <see cref="INotifyPropertyChanged"/> interface.
    /// </summary>
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        #region Events
        /// <summary>
        /// The PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets a property to the specified value if the value is different from the property's current value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="backingField">The field which stores the property's value.</param>
        /// <param name="newValue">The new property value.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T backingField, T newValue, [CallerMemberName]string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingField, newValue))
            {
                return false;
            }
            backingField = newValue;
            RaisePropertyChanged(propertyName);

            return true;
        }
        #endregion
    }
}

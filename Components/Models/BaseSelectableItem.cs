using Components.Helpers;

namespace Components.Models
{
    /// <summary>
    /// Base object for use in selection cells
    /// </summary>
    public class BaseSelectableItem : NotifyPropertyChanged
    {
        #region Fields
        //private object _item;
        private string _displayText;
        private bool _isEnabled = true;
        private bool _isSelected;
        #endregion

        #region Properties
        ///// <summary>
        ///// The selectable item as a generic object
        ///// </summary>
        //public object Item { get => _item; set { SetProperty(ref _item, value); } }

        /// <summary>
        /// Text to show for selection
        /// </summary>
        public string DisplayText { get => _displayText; set { SetProperty(ref _displayText, value); } }

        /// <summary>
        /// Determines if the object is selectable
        /// </summary>
        public bool IsEnabled { get => _isEnabled; set { SetProperty(ref _isEnabled, value); } }

        /// <summary>
        /// Determines of the object is selected
        /// </summary>
        public bool IsSelected { get => _isSelected; set { SetProperty(ref _isSelected, value); } }
        #endregion
    }
}

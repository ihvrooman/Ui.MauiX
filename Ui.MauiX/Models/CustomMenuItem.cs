using Components.Helpers;
using Components.Models;
using System;

namespace Ui.MauiX.Models
{
    public class CustomMenuItem : BaseSelectableItem
    {
        #region Fields
        private string _title;
        private string _iconResourceId;
        #endregion

        #region Properties
        public string Title { get => _title; set => SetProperty(ref _title, value); }

        public string IconResourceId { get => _iconResourceId; set => SetProperty(ref _iconResourceId, value); }

        public Type TargetType { get; set; }
        #endregion

        #region Constructors
        public CustomMenuItem(string title, string iconResourceId, Type targetType)
        {
            Title = title;
            IconResourceId = iconResourceId;
            TargetType = targetType;
        }

        public CustomMenuItem()
        {
            //Parameter-less constructor
        }
        #endregion
    }
}

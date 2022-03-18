using Components.Models;
using Ui.MauiX.Models;
using Ui.MauiX.Test.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ui.MauiX.Test.Models
{
    public class RandomItem : BaseSelectableItem
    {
        #region Fields
        private RandomEnum _randomEnum;
        #endregion
        
        #region Properties
        public RandomEnum RandomEnum
        {
            get => _randomEnum;
            set
            {
                _randomEnum = value;
                DisplayText = RandomEnum.ToString();
            }
        }
        #endregion

        #region Constructor
        public RandomItem(RandomEnum randomEnum)
        {
            RandomEnum = randomEnum;
        }
        #endregion
    }
}

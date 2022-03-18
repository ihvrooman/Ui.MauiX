using System;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.Helpers
{
    public class ResourceImage : IMarkupExtension
    {
        public string ResourceId
        {
            get;
            set;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return GetImageFromResource.GetImageFromString(ResourceId);
        }
    }
}

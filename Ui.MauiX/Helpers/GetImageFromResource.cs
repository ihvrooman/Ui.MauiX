using Ui.Enums;
using Ui.Icons;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace Ui.MauiX.Helpers
{
    public static class GetImageFromResource
    {
        #region Fields
        private static Assembly _assembly = typeof(EmptyIconClass).GetTypeInfo().Assembly;
        private static string[] _manifestResourceNames = _assembly.GetManifestResourceNames();
        private static string _resourceIdPrefix = "Ui.Icons.Icons.";
        private static ImageSource _fallbackImageSource = ImageSource.FromResource(_resourceIdPrefix + "ImageNotFound.png", _assembly);
        #endregion

        #region Methods
        /// <summary>
        /// Gets an <see cref="ImageSource"/> from an image resource id.
        /// </summary>
        public static ImageSource GetImageFromString(string ResourceId)
        {
            var imageResourceId = _resourceIdPrefix + ResourceId;
            if (!_manifestResourceNames.Contains(imageResourceId))
            {
                return _fallbackImageSource;
            }

            return ImageSource.FromResource(imageResourceId, _assembly);
        }
        #endregion
    }
}

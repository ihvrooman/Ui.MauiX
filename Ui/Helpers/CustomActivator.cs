using System;
using System.Collections.Generic;
using System.Text;

namespace Ui.Helpers
{
    /// <summary>
    /// A custom actvator with caching functionality.
    /// </summary>
    public class CustomActivator
    {
        #region Fields
        private static Dictionary<Type, object> _cache = new Dictionary<Type, object>();
        #endregion

        #region Methods
        public static object GetInstance(Type type, bool cacheItem = false)
        {
            if (cacheItem)
            {
                if (_cache.ContainsKey(type))
                {
                    return _cache[type];
                }
                else
                {
                    var item = Activator.CreateInstance(type);
                    _cache.Add(type, item);
                    return item;
                }
            }
            else
            {
                return Activator.CreateInstance(type);
            }
        }

        public static void ClearCache(Type type)
        {
            _cache.Remove(type);
        }

        public static void ClearCache()
        {
            _cache.Clear();
        }
        #endregion
    }
}

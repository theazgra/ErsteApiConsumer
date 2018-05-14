using System;
using System.Configuration;

namespace ErsteApi.Configuration
{
    static class ConfigurationHelper
    {
        /// <summary>
        /// Get value or default from AppSettings.
        /// </summary>
        /// <typeparam name="T">Type to which parse string value from app settings.</typeparam>
        /// <param name="key">AppSetting key.</param>
        /// <returns>Parsed value or default(T)</returns>
        internal static T GetValue<T>(string key)
        {
            //Setting is defined in xml.
            string val = ConfigurationManager.AppSettings.Get(key);
            if (string.IsNullOrEmpty(val))
                return default(T);
            
            try
            {
                T value = (T)Convert.ChangeType(val, typeof(T));
                return value;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Tries to get value from AppSetting and parse it into specified type.
        /// </summary>
        /// <typeparam name="T">Type to which parse string value from app setting.</typeparam>
        /// <param name="key">AppSetting key.</param>
        /// <param name="value">Parsed value.</param>
        /// <returns>True if key was found and conversion was succesfull.</returns>
        internal static bool TryGetValue<T>(string key, out T value) where T : IConvertible
        {
            value = default(T);

            //Setting is defined in xml.
            string val = ConfigurationManager.AppSettings.Get(key);
            if (string.IsNullOrEmpty(val))
                return false;

            try
            {
                value = (T)Convert.ChangeType(val, typeof(T));
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

    }
}

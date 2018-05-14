using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ErsteApi.Json
{
    internal static class Converter
    {
        /// <summary>
        /// Deserialize JSON into Enumerable of T
        /// </summary>
        /// <typeparam name="T">Type of elements in IEnumerable.</typeparam>
        /// <param name="json">JSON string.</param>
        /// <returns>Enumerable with T elements.</returns>
        internal static IEnumerable<T> DeserializeCollection<T>(string json)
        {
            return Deserialize<IEnumerable<T>>(json);
        }
    
        /// <summary>
        /// Deserialize JSON into given type.
        /// </summary>
        /// <typeparam name="T">Type to which deserialize json.</typeparam>
        /// <param name="json">JSON string.</param>
        /// <returns>Deserialized object or default(T).</returns>
        internal static T Deserialize<T>(string json)
        {
            try
            {
                T parsed = JsonConvert.DeserializeObject<T>(json);
                return parsed;
            }
            catch (Exception)
            {
                if (Configuration.ErsteApiConfig.ThrowOnException)
                    throw;

                return default(T);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using ErsteApi.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ErsteApi.Json
{
    internal static class Converter
    {

#if DEBUG
        private class JsonTraceWriter : ITraceWriter
        {
            public TraceLevel LevelFilter { get; set; }

            //TraceLevel ITraceWriter.LevelFilter => throw new NotImplementedException();

            public void Trace(TraceLevel level, string message, Exception ex)
            {
                Console.WriteLine("Trace level: {0}, message: {1}", level, message);
            }
        }
#endif

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

        private static void ErrorLog(object sender, ErrorEventArgs error)
        {
            Console.WriteLine(error);
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
#if DEBUG
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    Error = new EventHandler<ErrorEventArgs>(ErrorLog),
                    TraceWriter = new JsonTraceWriter(),
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    Culture = System.Threading.Thread.CurrentThread.CurrentCulture


                };
#endif


                T parsed = JsonConvert.DeserializeObject<T>(json);
                return parsed;
            }
            catch (Exception)
            {
                if (ConfigSingleton.Instance.ApiConfig.ThrowOnException)
                    throw;

                return default(T);
            }
        }
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace ErsteApi.Configuration
{
    /// <summary>
    /// Main configuration class of ErseApiWrapper.
    /// </summary>
    internal class ConfigSingleton
    {
        private static object _lock = new object();
        private static ConfigSingleton _instance;

        internal ErsteApiConfig ApiConfig { get; }

        internal static ConfigSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConfigSingleton();
                        }
                    }
                }
                return _instance;
            }
        }


        /// <summary>
        /// Initiliaze configuration properties.
        /// </summary>
        private ConfigSingleton()
        {
            string jsonString;
            using (StreamReader reader = new StreamReader("ErsteConfig.json"))
            {
                jsonString = reader.ReadToEnd();
            }

            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };


            ApiConfig = JsonConvert.DeserializeObject<ErsteApiConfig>(jsonString, new JsonSerializerSettings() { ContractResolver = contractResolver });
        }

    }
}

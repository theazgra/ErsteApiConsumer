using ErsteApi.Configuration;
using ErsteApi.Rest;
using System;
using System.Linq;
using System.Text;

namespace ErsteApi
{
    /// <summary>
    /// Ancestor for Rest API consumers.
    /// </summary>
    public abstract class ApiConsumer
    {
        /// <summary>
        /// URL of the requested API.
        /// </summary>
        internal protected string baseUrl;
        /// <summary>
        /// Web api key used for authentication.
        /// </summary>
        internal protected string apiKey;
        /// <summary>
        /// Languge of response.
        /// </summary>
        internal protected string language;

        internal ApiConsumer(string apiKey, string language)
        {
            this.baseUrl = ConfigSingleton.Instance.ApiConfig.BaseApiUrl;
            this.apiKey = apiKey;
            this.language = language;
        }

        /// <summary>
        /// Add API key to header and language setting.
        /// </summary>
        /// <param name="baseRestClient">Rest client.</param>
        private void AddBasicParameters(BaseClient baseRestClient)
        {
            baseRestClient.HeaderFields.Add(new HeaderField("web-api-key", apiKey));
            baseRestClient.RestQueryParameters.Add(new RestParameter("lang", language));
        }

        private string CombineUrl(params string[] urls)
        {
            if (urls.Length == 0)
                return string.Empty;

            StringBuilder urlBuilder = new StringBuilder(urls[0]);
            for (int i = 1; i < urls.Length; i++)
            {
                if (urlBuilder.ToString().Last() != '/')
                {
                    if (urls[i].First() == '/')
                    {
                        urlBuilder.Append(urls[i]);
                    }
                    else
                    {
                        urlBuilder.Append('/' + urls[i]);
                    }
                }
                else
                {
                    if (urls[i].First() == '/')
                    {
                        urlBuilder.Append(urls[i].Substring(1));
                    }
                    else
                    {
                        urlBuilder.Append(urls[i]);
                    }
                }
            }

            string combined = urlBuilder.ToString();
            return combined;
        }

        /// <summary>
        /// Get prepared client with url, api key and language set.
        /// </summary>
        /// <param name="apiUrl">Requested api location.</param>
        /// <returns>Common Rest client.</returns>
        internal Client GetClient(string apiGroupUrl, string methodUrl)
        {

            Client restClient = new Client(CombineUrl(baseUrl, apiGroupUrl, methodUrl));
            AddBasicParameters(restClient);
            return restClient;
        }

        /// <summary>
        /// Get prepared client with url, api key and language set.
        /// </summary>
        /// <typeparam name="T">Type to which parse response.</typeparam>
        /// /// <param name="apiUrl">Requested api location.</param>
        /// <returns>Typed rest client.</returns>
        internal Client<T> GetClient<T>(string apiGroupUrl, string methodUrl)
        {
            Client<T> restClient = new Client<T>(CombineUrl(baseUrl, apiGroupUrl, methodUrl));
            AddBasicParameters(restClient);
            return restClient;
        }

        protected abstract string GetGroupUrl();

        public bool HealthCheck()
        {
            string groupUrl = GetGroupUrl();
            string healthMethodUrl = "health";

            var client = GetClient(groupUrl, healthMethodUrl);
            var response = client.ExecuteRequest();

            return (response.StatusCode == System.Net.HttpStatusCode.NoContent);
        }

    }
}

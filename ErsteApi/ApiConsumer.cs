using ErsteApi.Rest;

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
            this.baseUrl = Configuration.ErsteApiConfig.BaseApiUrl;
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

        private string CombineUrl(string baseUrl, string apiUrl)
        {
            if (baseUrl.Substring(baseUrl.Length - 1) != "/")
                baseUrl += "/";

            string combined = baseUrl + apiUrl;
            return combined;
        }

        /// <summary>
        /// Get prepared client with url, api key and language set.
        /// </summary>
        /// <param name="apiUrl">Requested api location.</param>
        /// <returns>Common Rest client.</returns>
        internal Client GetClient(string apiUrl)
        {

            Client restClient = new Client(CombineUrl(baseUrl, apiUrl));
            AddBasicParameters(restClient);
            return restClient;
        }

        /// <summary>
        /// Get prepared client with url, api key and language set.
        /// </summary>
        /// <typeparam name="T">Type to which parse response.</typeparam>
        /// /// <param name="apiUrl">Requested api location.</param>
        /// <returns>Typed rest client.</returns>
        internal Client<T> GetClient<T>(string apiUrl) where T : new()
        {
            Client<T> restClient = new Client<T>(CombineUrl(baseUrl, apiUrl));
            AddBasicParameters(restClient);
            return restClient;
        }

    }
}

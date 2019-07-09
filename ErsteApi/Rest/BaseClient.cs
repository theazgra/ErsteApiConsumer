using ErsteApi.Configuration;
using RestSharp;
using System.Collections.Generic;

namespace ErsteApi.Rest
{
    internal abstract class BaseClient
    {
        #region Properties
        /// <summary>
        /// Default timeout of rest request.
        /// </summary>
        public int DefaultTimeout { get; set; } = 5000;

        /// <summary>
        /// Header fields.
        /// </summary>
        public List<HeaderField> HeaderFields { get; private set; }

        /// <summary>
        /// Rest query parameters.
        /// </summary>
        public List<RestParameter> RestQueryParameters { get; private set; }
        #endregion
        protected string _url;

        /// <summary>
        /// Create HeaderFields and RestQueryParameters.
        /// </summary>
        /// <param name="url"></param>
        public BaseClient(string url)
        {
            _url = url;
            HeaderFields = new List<HeaderField>();
            RestQueryParameters = new List<RestParameter>();
        }

        #region protected implementation

        /// <summary>
        /// Return new RestClient with default timeout.
        /// </summary>
        /// <returns>Rest client with default timeout.</returns
        protected virtual RestClient GetClient()
        {
            RestClient restClient = new RestClient(ConfigSingleton.Instance.ApiConfig.BaseApiUrl)
            {
                Timeout = DefaultTimeout
            };

            return restClient;
        }

        /// <summary>
        /// Add header and query parameters to rest request.
        /// </summary>
        /// <param name="request">Rest request.</param>
        protected void AddParameters(RestRequest request)
        {
            foreach (HeaderField headerField in HeaderFields)
            {
                request.AddHeader(headerField.Name(), headerField.Value());
            }

            foreach (RestParameter restParam in RestQueryParameters)
            {
                request.AddParameter(restParam.Name(), restParam.Value(), restParam.ParamType());
            }
        }

        /// <summary>
        /// Create rest request.
        /// </summary>
        /// <param name="method">Request method.</param>
        /// <returns>Rest request with header fields and rest query parameters.</returns>
        protected IRestRequest CreateRequest(Method restMethod = Method.GET)
        {
            RestRequest restRequest = new RestRequest(_url)
            {
                Method = Method.GET,
                RequestFormat = DataFormat.Json
            };

            AddParameters(restRequest);
            return restRequest;
        }
        #endregion
    }
}
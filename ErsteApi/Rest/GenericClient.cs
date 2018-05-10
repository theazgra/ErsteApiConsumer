using System;
using RestSharp;
using System.Diagnostics;
using RestSharp.Deserializers;

namespace ErsteApi.Rest
{
    /// <summary>
    /// Rest client wrapper. Parse response to given type.
    /// </summary>
    /// <typeparam name="T">Type to which parse rest response.</typeparam>
    internal class Client<T> : BaseClient where T : new()
    {
        internal delegate void TypedRequestFinished(IRestResponse<T> restResponse);
        /// <summary>
        /// Event fired when request is finished.
        /// </summary>
        internal event TypedRequestFinished OnTypedRequestFinished;

        private IDeserializer _customJsonSerialized;

        public Client(string url, IDeserializer jsonDeserializer = null) : base(url)
        {
            _customJsonSerialized = jsonDeserializer;
        }

        protected override RestClient GetClient()
        {
            RestClient restClient = new RestClient()
            {
                Timeout = DefaultTimeout
            };

            if (_customJsonSerialized != null)
                restClient.AddHandler("application/json", _customJsonSerialized);

            return restClient;
        }

        /// /// <summary>
        /// Execute request and return IRestResponse with specified type T.
        /// </summary>
        /// <param name="restRequest">Request to execute.</param>
        /// <param name="succes">True if request was succesfull.</param>
        /// <returns>Rest response or null if request was not succesfull.</returns>
        private IRestResponse<T> ExecuteRequest(IRestRequest restRequest, out bool succes)
        {
            RestClient restClient = GetClient();

            try
            {
                IRestResponse<T> response = restClient.Execute<T>(restRequest);
                succes = true;
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error executing rest request: " + e.Message);
                succes = false;
                return null;
            }
        }

        /// <summary>
        /// Execute request and call callback function when request is finished.
        /// </summary>
        /// <param name="restRequest">Request to execute.</param>
        /// <param name="callback">Callback to be called when request is finished.</param>
        /// <returns>RestRequestAsyncHandle to request.</returns>
        private RestRequestAsyncHandle ExecuteRequestAsync(IRestRequest restRequest, Action<IRestResponse<T>> callback)
        {
            RestClient restClient = GetClient();

            try
            {
                RestRequestAsyncHandle handle = restClient.ExecuteAsync<T>(restRequest, callback);
                return handle;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error executing rest request: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Invoke OnTypedRequestFinished event.
        /// </summary>
        /// <param name="restResponse">Typed rest response.</param>
        private void TypedCallback(IRestResponse<T> restResponse)
        {
            OnTypedRequestFinished?.Invoke(restResponse);
        }

        /// <summary>
        /// Execute rest request and return response of given type.
        /// </summary>
        /// <returns>Rest response of given type.</returns>
        internal IRestResponse<T> ExecuteRequest() 
        {
            IRestRequest request = CreateRequest();

            IRestResponse<T> response = ExecuteRequest(request, out bool success);

            if (success)
                return response;
            else
                throw new RestException();
        }

        /// <summary>
        /// Execute rest request and call callback with response of given type, when request is finished.
        /// </summary>
        internal void ExecuteRequestAsync()
        {
            IRestRequest request = CreateRequest();
            ExecuteRequestAsync(request, TypedCallback);
        }
    }
}

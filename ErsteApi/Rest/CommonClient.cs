using System;
using RestSharp;
using System.Diagnostics;

namespace ErsteApi.Rest
{
    /// <summary>
    /// Rest client wrapper.
    /// </summary>
    internal class Client : BaseClient
    {
        internal delegate void RequestFinished(IRestResponse restResponse);
        /// <summary>
        /// Event fired when request is finished.
        /// </summary>
        internal event RequestFinished OnRequestFinished;

        public Client(string url) : base(url)
        {

        }

        /// <summary>
        /// Execute request and return generic IRestResponse.
        /// </summary>
        /// <param name="restRequest">Request to execute.</param>
        /// <param name="succes">True if request was succesfull.</param>
        /// <returns>Rest response or null if request was not succesfull.</returns>
        private IRestResponse ExecuteRequest(IRestRequest restRequest, out bool succes)
        {
            RestClient restClient = GetClient();

            try
            {
                IRestResponse response = restClient.Execute(restRequest);
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
        private RestRequestAsyncHandle ExecuteRequestAsync(IRestRequest restRequest, Action<IRestResponse> callback)
        {
            RestClient restClient = GetClient();

            try
            {
                RestRequestAsyncHandle handle = restClient.ExecuteAsync(restRequest, callback);
                return handle;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error executing rest request: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Invoke OnRequestFinished event.
        /// </summary>
        /// <param name="restResponse">Rest response.</param>
        private void Callback(IRestResponse restResponse)
        {
            OnRequestFinished?.Invoke(restResponse);
        }

        /// <summary>
        /// Execute rest request and return generic response.
        /// </summary>
        /// <returns>Generic rest response.</returns>
        internal IRestResponse ExecuteRequest()
        {
            IRestRequest request = CreateRequest();

            IRestResponse response = ExecuteRequest(request, out bool success);

            if (success)
                return response;
            else
                throw new RestException();
        }

        /// <summary>
        /// Execute rest request and call callback with generic response, when request is finished.
        /// </summary>
        internal void ExecuteRequestAsync()
        {
            IRestRequest request = CreateRequest();

            ExecuteRequestAsync(request, Callback);
        }
    }
}

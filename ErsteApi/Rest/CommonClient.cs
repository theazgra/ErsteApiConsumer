using System;
using RestSharp;
using System.Diagnostics;
using ErsteApi.Configuration;

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

                if (ErsteApiConfig.ThrowOnException)
                    throw;

                return null;
            }
        }

        /// <summary>
        /// Execute request and call callback function when request is finished.
        /// </summary>
        /// <param name="restRequest">Request to execute.</param>
        /// <param name="callback">Callback to be called when request is finished.</param>
        /// <returns>RestRequestAsyncHandle to request.</returns>
        private RestRequestAsyncHandle ExecRequestAsync(IRestRequest restRequest, Action<IRestResponse> callback)
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

                if (ErsteApiConfig.ThrowOnException)
                    throw;

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
        /// <param name="method">Request method.</param>
        /// <returns>Generic rest response.</returns>
        internal IRestResponse ExecuteRequest(Method method = Method.GET)
        {
            IRestRequest request = CreateRequest(method);

            IRestResponse response = ExecuteRequest(request, out bool success);

            if (success)
                return response;
            else
                throw new RestException();
        }

        /// <summary>
        /// Execute rest request and invoke callback, when request is finished.
        /// </summary>
        /// <param name="callback">Callback to be invoked when request is finished.</param>
        /// <param name="method">Request method.</param>
        internal void ExecuteRequestAsync(Action<IRestResponse> callback, Method method = Method.GET)
        {
            IRestRequest request = CreateRequest(method);
            ExecRequestAsync(request, callback);
        }

        /// <summary>
        /// Execute rest request and invoke OnRequestFinishedresponse, when request is finished.
        /// </summary>
        /// <param name="method">Request method.</param>
        internal void ExecuteRequestAsync(Method method = Method.GET)
        {
            IRestRequest request = CreateRequest(method);

            ExecRequestAsync(request, Callback);
        }
    }
}

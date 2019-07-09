using System;
using RestSharp;
using System.Diagnostics;
using RestSharp.Deserializers;
using ErsteApi.Configuration;
using ErsteApi.Json;

namespace ErsteApi.Rest
{
    /// <summary>
    /// Rest client wrapper. Parse response to given type.
    /// </summary>
    /// <typeparam name="T">Type to which parse rest response.</typeparam>
    internal class Client<T> : BaseClient
    {
        internal delegate void TypedRequestFinished(T result);
        /// <summary>
        /// Event fired when request is finished.
        /// </summary>
        internal event TypedRequestFinished OnTypedRequestFinished;

        public Client(string url) : base(url)
        {
        }

        protected override RestClient GetClient()
        {
            RestClient restClient = new RestClient(ConfigSingleton.Instance.ApiConfig.BaseApiUrl)
            {
                Timeout = DefaultTimeout
            };

            return restClient;
        }

        /// /// <summary>
        /// Execute request and return IRestResponse with specified type T.
        /// </summary>
        /// <param name="restRequest">Request to execute.</param>
        /// <param name="succes">True if request was succesfull.</param>
        /// <returns>Rest response or null if request was not succesfull.</returns>
        private IRestResponse PrivateExecuteRequest(IRestRequest restRequest, out bool succes)
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

                if (ConfigSingleton.Instance.ApiConfig.ThrowOnException)
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

                if (ConfigSingleton.Instance.ApiConfig.ThrowOnException)
                    throw;

                return null;
            }
        }

        /// <summary>
        /// Invoke OnTypedRequestFinished event.
        /// </summary>
        /// <param name="restResponse">Typed rest response.</param>
        private void TypedCallback(IRestResponse restResponse)
        {
            T result = Converter.Deserialize<T>(restResponse.Content);
            OnTypedRequestFinished?.Invoke(result);
        }

        /// <summary>
        /// Execute rest request and return response of given type.
        /// </summary>
        /// <param name="method">Request method.</param>
        /// <returns>Rest response of given type.</returns>
        internal T ExecuteRequest(Method method = Method.GET)
        {
            IRestRequest request = CreateRequest(method);

            IRestResponse response = PrivateExecuteRequest(request, out bool success);

            if (success)
            {
                T result = Converter.Deserialize<T>(response.Content);
                return result;
            }
            else
            {
                if (ConfigSingleton.Instance.ApiConfig.ThrowOnException)
                    throw new RestException("Error executing rest request.");

                return default;
            }
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
        /// Execute rest request and invoke OnRequestFinished with response of given type, when request is finished.
        /// </summary>
        /// <param name="method">Request method.</param>
        internal void ExecuteRequestAsync(Method method = Method.GET)
        {
            IRestRequest request = CreateRequest(method);
            ExecRequestAsync(request, TypedCallback);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using RestSharp;

namespace ErsteApi.Rest
{
    internal class Client
    {
        #region Private implementation

        private static int DEFAULT_TIMEOUT = 5000;

        /// <summary>
        /// Return new RestClient with default timeout.
        /// </summary>
        /// <returns></returns>
        private static RestClient GetClient()
        {
            RestClient restClient = new RestClient()
            {
                Timeout = DEFAULT_TIMEOUT
            };

            return restClient;
        }

        /// <summary>
        /// Add header and query parameters to rest request.
        /// </summary>
        /// <param name="request">Rest request.</param>
        /// <param name="headers">Header fields.</param>
        /// <param name="queryParams">Query params.</param>
        private static void AddParameters(RestRequest request, HeaderField[] headers, RestParameter[] queryParams)
        {
            if (headers != null)
            {
                foreach (HeaderField headerField in headers)
                {
                    request.AddHeader(headerField.Name(), headerField.Value());
                }
            }

            if (queryParams != null)
            {
                foreach (RestParameter restParam in queryParams)
                {
                    request.AddQueryParameter(restParam.Name(), restParam.Value());
                }
            }
        }

        /// <summary>
        /// Create rest request.
        /// </summary>
        /// <param name="url">URL of request.</param>
        /// <param name="headerFields">Header fields.</param>
        /// <param name="restParameters">Rest query parameters.</param>
        /// <returns>Rest request with header fields and rest query parameters.</returns>
        private static IRestRequest CreateRequest(string url, HeaderField[] headerFields, RestParameter[] restParameters)
        {
            RestRequest restRequest = new RestRequest(url, Method.GET);
            AddParameters(restRequest, headerFields, restParameters);

            return restRequest;
        }

        /// <summary>
        /// Execute request and return generic IRestResponse.
        /// </summary>
        /// <param name="restRequest">Request to execute.</param>
        /// <param name="succes">True if request was succesfull.</param>
        /// <returns>Rest response or null if request was not succesfull.</returns>
        private static IRestResponse ExecuteRequest(IRestRequest restRequest, out bool succes)
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

        /// /// <summary>
        /// Execute request and return IRestResponse with specified type T.
        /// </summary>
        /// <typeparam name="T">Type of response.</typeparam>
        /// <param name="restRequest">Request to execute.</param>
        /// <param name="succes">True if request was succesfull.</param>
        /// <returns>Rest response or null if request was not succesfull.</returns>
        private static IRestResponse<T> ExecuteRequest<T>(IRestRequest restRequest, out bool succes) where T : new()
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
        /// <param name="succes">True if request was succesfull.</param>
        /// <returns>RestRequestAsyncHandle to request.</returns>
        private static RestRequestAsyncHandle ExecuteRequestAsync(IRestRequest restRequest, Action<IRestResponse> callback)
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
        /// Execute request and call callback function when request is finished.
        /// </summary>
        /// <typeparam name="T">Type of response.</typeparam>
        /// <param name="restRequest">Request to execute.</param>
        /// <param name="callback">Callback to be called when request is finished.</param>
        /// <param name="succes">True if request was succesfull.</param>
        /// <returns>RestRequestAsyncHandle to request.</returns>
        private static RestRequestAsyncHandle ExecuteRequestAsync<T>(IRestRequest restRequest, Action<IRestResponse<T>> callback) where T : new()
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
        #endregion

        #region Internal API

        /// <summary>
        /// Set default timeout used by rest client.
        /// </summary>
        /// <param name="timeout">Timeout in milliseconds.</param>
        internal static void SetDefaultTimeout(int timeout)
        {
            DEFAULT_TIMEOUT = timeout;
        }

        /// <summary>
        /// Execute rest request and return generic response.
        /// </summary>
        /// <param name="url">URL where to execute request.</param>
        /// <param name="headerFields">Rest header fields.</param>
        /// <param name="restParameters">Rest query parameters.</param>
        /// <returns>Generic rest response.</returns>
        internal static IRestResponse ExecuteRequest(
            string url, HeaderField[] headerFields = null, RestParameter[] restParameters = null)
        {
            IRestRequest request = CreateRequest(url, headerFields, restParameters);

            IRestResponse response = ExecuteRequest(request, out bool success);

            if (success)
                return response;
            else
                throw new RestException();
        }

        /// <summary>
        /// Execute rest request and return response of given type.
        /// </summary>
        /// <typeparam name="T">Type of given request.</typeparam>
        /// <param name="url">URL where to execute request.</param>
        /// <param name="headerFields">Rest header fields.</param>
        /// <param name="restParameters">Rest query parameters.</param>
        /// <returns>Rest response of given type.</returns>
        internal static IRestResponse<T> ExecuteRequest<T>(
            string url, HeaderField[] headerFields = null, RestParameter[] restParameters = null) where T : new()
        {
            IRestRequest request = CreateRequest(url, headerFields, restParameters);

            IRestResponse<T> response = ExecuteRequest<T>(request, out bool success);

            if (success)
                return response;
            else
                throw new RestException();
        }

        /// <summary>
        /// Execute rest request and call callback with generic response, when request is finished.
        /// </summary>
        /// <param name="url">URL where to execute request.</param>
        /// <param name="callback"></param>
        /// <param name="headerFields">Rest header fields.</param>
        /// <param name="restParameters">Rest query parameters.</param>
        internal static void ExecuteRequestAsync(string url, Action<IRestResponse> callback, HeaderField[] headerFields = null, RestParameter[] restParameters = null)
        {
            IRestRequest request = CreateRequest(url, headerFields, restParameters);

            ExecuteRequestAsync(request, callback);
        }


        /// <summary>
        /// Execute rest request and call callback with response of given type, when request is finished.
        /// </summary>
        /// <typeparam name="T">Type of response.</typeparam>
        /// <param name="url">URL where to execute request.</param>
        /// <param name="callback"></param>
        /// <param name="headerFields">Rest header fields.</param>
        /// <param name="restParameters">Rest query parameters.</param>
        internal static void ExecuteRequestAsync<T>(string url, Action<IRestResponse<T>> callback, HeaderField[] headerFields = null, RestParameter[] restParameters = null) where T : new()
        {
            IRestRequest request = CreateRequest(url, headerFields, restParameters);
            ExecuteRequestAsync<T>(request, callback);
        }
        #endregion
    }
}

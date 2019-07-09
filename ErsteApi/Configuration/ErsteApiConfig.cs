using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ErsteApi.Configuration
{
    public class ErsteApiConfig
    {
        /// <summary>
        /// Default language of the response.
        /// </summary>
        public string DefaultLanguage { get; set; }

        /// <summary>
        /// Default timeout of the REST client.
        /// </summary>
        public int DefaultTimeout { get; set; }

        /// <summary>
        /// If exception should be propagated.
        /// </summary>
        public bool ThrowOnException { get; set; }

        /// <summary>
        /// Base URL of the erste API.
        /// </summary>
        public string BaseApiUrl { get; set; }

        /// <summary>
        /// Configuration of the exchnage rate API.
        /// </summary>
        public ExchangeRateConfig ExchangeRateConfig { get; set; }
    }
}

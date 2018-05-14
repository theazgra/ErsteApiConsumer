namespace ErsteApi.Configuration
{
    /// <summary>
    /// Main configuration class of ErseApiWrapper.
    /// </summary>
    internal static class ErsteApiConfig
    {
        /// <summary>
        /// If exception should be propagated.
        /// </summary>
        internal static bool ThrowOnException { get; }

        /// <summary>
        /// Base URL of the erste API.
        /// </summary>
        internal static string BaseApiUrl { get; }

        /// <summary>
        /// Configuration of the exchnage rate API.
        /// </summary>
        internal static ExchangeRateConfig ExchangeRateConfig { get; }

        /// <summary>
        /// Initiliaze configuration properties.
        /// </summary>
        static ErsteApiConfig()
        {
            ThrowOnException = ConfigurationHelper.GetValue<bool>("throwOnException");
            ExchangeRateConfig = new ExchangeRateConfig();

            if (ConfigurationHelper.TryGetValue("baseApiUrl", out string baseUrl))
                BaseApiUrl = baseUrl;
            else
                BaseApiUrl = @"https://webapi.developers.erstegroup.com/api/csas/sandbox/v1";
        }

    }
}

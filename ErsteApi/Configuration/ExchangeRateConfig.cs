namespace ErsteApi.Configuration
{
    internal class ExchangeRateConfig
    {
        internal string CurrenciesUrl { get; }
        internal string ExchangeRatesUrl { get; }
        internal string CrossRatesUrl { get; }
        internal string ExchangeRatesDayTimesUrl { get; }
        internal string HealthCheckUrl { get; }

        public ExchangeRateConfig()
        {
            if (ConfigurationHelper.TryGetValue("currenciesUrl", out string currUrl))
                CurrenciesUrl = currUrl;
            else
                CurrenciesUrl = "rates/exchangerates/currencies";

            if (ConfigurationHelper.TryGetValue("exchangeRatesUrl", out string exchgRtsUrl))
                ExchangeRatesUrl = exchgRtsUrl;
            else
                ExchangeRatesUrl = "rates/exchangerates";

            if (ConfigurationHelper.TryGetValue("crossRatesUrl", out string crossUrl))
                CrossRatesUrl = crossUrl;
            else
                CrossRatesUrl = "rates/exchangerates/cross";

            if (ConfigurationHelper.TryGetValue("exchangeRatesDayTimesUrl", out string dayTimesRatesUrl))
                ExchangeRatesDayTimesUrl = dayTimesRatesUrl;
            else
                ExchangeRatesDayTimesUrl = "rates/exchangerates/times";

            if (ConfigurationHelper.TryGetValue("healthCheck", out string healthUrl))
                HealthCheckUrl = healthUrl;
            else
                HealthCheckUrl = "rates/exchangerates/health";   
        }
    }
}

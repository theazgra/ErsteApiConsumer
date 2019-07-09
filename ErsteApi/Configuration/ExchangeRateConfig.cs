using Newtonsoft.Json;

namespace ErsteApi.Configuration
{
    public class ExchangeRateConfig : ApiGroupConfig
    {
        public string CurrenciesUrl { get; set; }
        public string ExchangeRatesUrl { get; set; }
        public string CrossRatesUrl { get; set; }
        public string ExchangeRatesDayTimesUrl { get; set; }
        public string HealthCheckUrl { get; set; }
    }
}

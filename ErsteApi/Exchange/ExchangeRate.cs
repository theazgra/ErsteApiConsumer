using ErsteApi.Exchange.Model;
using ErsteApi.Rest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using ErsteApi.Json;

namespace ErsteApi.Exchange
{
    public class ExchangeRate : ApiConsumer
    {
        private Configuration.ExchangeRateConfig _config;
        internal static IEnumerable<Currency> currencies;

        public ExchangeRate(string apiKey, string language = "cs") : base(apiKey, language)
        {
            _config = Configuration.ConfigSingleton.Instance.ApiConfig.ExchangeRateConfig;
            //currencies = GetAllCurrencies();
        }

        /// <summary>
        /// Get enumerable of all currencies.
        /// </summary>
        /// <returns>Enumerable of all currencies.</returns>
        public IEnumerable<Currency> GetAllCurrencies()
        {
            Client<IEnumerable<Currency>> client =
                GetClient<IEnumerable<Currency>>(_config.BaseUrl, _config.CurrenciesUrl);

            currencies = client.ExecuteRequest();

            return currencies;
        }




        /// <summary>
        /// Get enumerable of all current exchange rates.
        /// </summary>
        /// <returns>Enumerable of all current exchange rates.</returns>
        public IEnumerable<Rate> GetAllExchangeRates()
        {
            Client<IEnumerable<Rate>> client = GetClient<IEnumerable<Rate>>(_config.BaseUrl, _config.ExchangeRatesUrl);
            IEnumerable<Rate> rates = client.ExecuteRequest();
            return rates;
        }




        /// <summary>
        /// Get enumerable of all exchange rates. Additionaly you can specify Date range.
        /// </summary>
        /// <param name="from">From when to get exchanche rate. Can be ommited.</param>
        /// <param name="to">To when get exchange rates. Can be ommited.</param>
        /// <returns>Enumerable of all exchange rates in specified perion.</returns>
        public IDictionary<DateTime, IEnumerable<Rate>> GetAllExchangeRatesInPeriod(DateTime? from = null, DateTime? to = null)
        {
            //TODO: What is response for this API call? API does not work at this moment so we don't know.
            //Client<IEnumerable<Rate>> client = GetClient<IEnumerable<Rate>>(ErsteApiConfig.ExchangeRateConfig.ExchangeRatesUrl);
            Client client = GetClient(_config.BaseUrl, _config.ExchangeRatesUrl);

            if (from.HasValue)
                client.RestQueryParameters.Add(new RestParameter("fromDate", from.Value.ToRestFormat()));
            if (to.HasValue)
                client.RestQueryParameters.Add(new RestParameter("toDate", to.Value.ToRestFormat()));

            var response = client.ExecuteRequest();

            /*
            IEnumerable<Rate> rates = client.ExecuteRequest();
            return rates;
            */
            throw new NotImplementedException();
        }





        public Rate GetExchangeRate(string currencyCode)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Rate> GetExchangeRateInPeriod(string currencyCode, DateTime? from = null, DateTime? to = null)
        {
            throw new NotImplementedException();
        }

        protected override string GetGroupUrl() => _config.BaseUrl;


        #region AsyncWrappers

        /// <summary>
        /// Get enumerable of all currencies async.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel this operation.</param>
        /// <returns>Enumerable of all currencies.</returns>
        public Task<IEnumerable<Currency>>
            GetAllCurrenciesAsync(CancellationToken cancellationToken = default(CancellationToken)) => Task.Run(() => GetAllCurrencies(), cancellationToken);

        /// <summary>
        /// Get enumerable of all exchange rates async.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel this operation.</param>
        /// <returns>Enumerable of all exchange rates.</returns>
        public Task<IEnumerable<Rate>>
            GetAllExchangeRatesAsync(CancellationToken cancellationToken = default(CancellationToken)) => Task.Run(() => GetAllExchangeRates(), cancellationToken);

        public Task<IDictionary<DateTime, IEnumerable<Rate>>> GetAllExchangeRatesInPeriodAsync
          (DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default(CancellationToken))
          => Task.Run(() => GetAllExchangeRatesInPeriod(from, to), cancellationToken);

        public Task<Rate> GetExchangeRateAsync(string currencyCode, CancellationToken cancellationToken = default(CancellationToken))
            => Task.Run(() => GetExchangeRate(currencyCode), cancellationToken);

        public Task<IEnumerable<Rate>> GetExchangeRateInPeriod
            (string currencyCode, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default(CancellationToken))
            => Task.Run(() => GetExchangeRateInPeriod(currencyCode, from, to), cancellationToken);

        #endregion



    }
}

using ErsteApi.Exchange.Model;
using ErsteApi.Rest;
using System;
using System.Collections.Generic;
using ErsteApi.Configuration;
using System.Threading.Tasks;
using System.Threading;
using ErsteApi.Json;

namespace ErsteApi.Exchange
{
    public class ExchangeRate : ApiConsumer
    {
        internal static IEnumerable<Currency> currencies;

        public ExchangeRate(string apiKey, string language = "cs") : base(apiKey, language)
        {
            currencies = GetAllCurrencies();
        }

        /// <summary>
        /// Get enumerable of all currencies.
        /// </summary>
        /// <returns>Enumerable of all currencies.</returns>
        public IEnumerable<Currency> GetAllCurrencies()
        {
            Client<IEnumerable<Currency>> client =
                GetClient<IEnumerable<Currency>>(ErsteApiConfig.ExchangeRateConfig.CurrenciesUrl);

            currencies = client.ExecuteRequest();

            return currencies;
        }

        /// <summary>
        /// Get enumerable of all currencies async.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel this operation.</param>
        /// <returns>Enumerable of all currencies.</returns>
        public Task<IEnumerable<Currency>> GetAllCurrenciesAsync(CancellationToken cancellationToken = default(CancellationToken))
            => Task.Run(() => GetAllCurrencies(), cancellationToken);

       
        /// <summary>
        /// Get enumerable of all current exchange rates.
        /// </summary>
        /// <returns>Enumerable of all current exchange rates.</returns>
        public IEnumerable<Rate> GetAllExchangeRates()
        {
            Client<IEnumerable<Rate>> client = GetClient<IEnumerable<Rate>>(ErsteApiConfig.ExchangeRateConfig.ExchangeRatesUrl);
            IEnumerable<Rate> rates = client.ExecuteRequest();
            return rates;
        }

        /// <summary>
        /// Get enumerable of all exchange rates async.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel this operation.</param>
        /// <returns>Enumerable of all exchange rates.</returns>
        public Task<IEnumerable<Rate>> GetAllExchangeRatesAsync(CancellationToken cancellationToken = default(CancellationToken))
            => Task.Run(() => GetAllExchangeRates(), cancellationToken);


        /// <summary>
        /// Get enumerable of all exchange rates. Additionaly you can specify Date range.
        /// </summary>
        /// <param name="from">From when to get exchanche rate. Can be ommited.</param>
        /// <param name="to">To when get exchange rates. Can be ommited.</param>
        /// <returns>Enumerable of all exchange rates in specified perion.</returns>
        public IDictionary<DateTime, IEnumerable<Rate>> GetAllExchangeRatesInPeriod(DateTime? from = null, DateTime? to = null)
        {
            //this is TODO.
            throw new NotImplementedException();
            Client<IEnumerable<Rate>> client = GetClient<IEnumerable<Rate>>(ErsteApiConfig.ExchangeRateConfig.ExchangeRatesUrl);

            if (from.HasValue)
                client.RestQueryParameters.Add(new RestParameter("fromDate", from.Value.ToRestFormat()));
            if (to.HasValue)
                client.RestQueryParameters.Add(new RestParameter("toDate", to.Value.ToRestFormat()));

            IEnumerable<Rate> rates = client.ExecuteRequest();
            return rates;
        }

        public Rate GetExchangeRate(string currencyCode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rate> GetExchangeRateInPeriod(string currencyCode, DateTime? from = null, DateTime? to = null)
        {
            throw new NotImplementedException();
        }


    }
}

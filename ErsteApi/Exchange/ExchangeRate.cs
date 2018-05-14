using ErsteApi.Exchange.Model;
using ErsteApi.Rest;
using ErsteApi.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using ErsteApi.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ErsteApi.Exchange
{
    public class ExchangeRate : ApiConsumer
    {

        private IEnumerable<Currency> _currencies;
        public ExchangeRate(string apiKey, string language = "cs") : base(apiKey, language)
        { }

        /// <summary>
        /// Get enumerable of all currencies.
        /// </summary>
        /// <returns>Enumerable of all currencies.</returns>
        public IEnumerable<Currency> GetAllCurrencies()
        {
            Client client = GetClient(ErsteApiConfig.ExchangeRateConfig.CurrenciesUrl);

            IRestResponse response = client.ExecuteRequest();
            _currencies = Converter.DeserializeCollection<Currency>(response.Content);

            return _currencies;
        }

        /// <summary>
        /// Get enumerable of all currencies async.
        /// </summary>
        /// <returns>Enumerable of all currencies.</returns>
        public Task<IEnumerable<Currency>> GetAllCurrenciesAsync()
        {
            return Task.Run(() => GetAllCurrencies());
        }

        /// <summary>
        /// Get enumerable of all currencies async.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel this operation.</param>
        /// <returns>Enumerable of all currencies.</returns>
        public Task<IEnumerable<Currency>> GetAllCurrenciesAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => GetAllCurrencies(), cancellationToken);
        }

        public IEnumerable<Rate> GetAllExchangeRates()
        {
            //If we want to deserialize JSON with RestSharp, we have to create class
            //RateCollection, because Client<T> T can be only one object with new(),
            //RateCollection have enumerable inside it, with some JSON Attribute on it.
            //But it is probably better to use Json.NET

            Client client = GetClient(ErsteApiConfig.ExchangeRateConfig.ExchangeRatesUrl);
            IRestResponse response = client.ExecuteRequest();


            //IEnumerable<Rate> rates = JsonConvert.DeserializeObject<IEnumerable<Rate>>(response.Content);
            IEnumerable<Rate> rates = Converter.DeserializeCollection<Rate>(response.Content);

            throw new NotImplementedException();
        }

        public IEnumerable<Rate> GetAllExchangeRatesFrom(DateTime from)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rate> GetAllExchangeRatesTo(DateTime to)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rate> GetAllExchangeRatesInPeriod(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public Rate GetExchangeRate(string currencyCode)
        {
            throw new NotImplementedException();
        }


    }
}

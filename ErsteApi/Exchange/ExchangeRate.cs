using ErsteApi.Exchange.Model;
using ErsteApi.Rest;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ErsteApi.Exchange
{
    public class ExchangeRate : ApiConsumer
    {
        private IEnumerable<Currency> _currencies;
        private string _currenciesUrl = @"rates/exchangerates/currencies";
        public ExchangeRate(string baseUrl, string apiKey, string language = "cs") : base(baseUrl, apiKey, language)
        {

        }

        public IEnumerable<Currency> GetAllCurrencies()
        {
            Client client = GetClient(_currenciesUrl);
            IRestResponse response = client.ExecuteRequest();

            _currencies = JsonConvert.DeserializeObject<IEnumerable<Currency>>(response.Content);
            return _currencies;
        }

        public IEnumerable<Rate> GetAllExchangeRates()
        {
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

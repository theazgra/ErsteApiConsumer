using ErsteApi.Exchange;
using System;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ExchangeRate er = new ExchangeRate(@"");

            er.GetAllExchangeRatesInPeriod(from: DateTime.Today.AddDays(-5), to: DateTime.Today.AddDays(-1));
        }
    }
}

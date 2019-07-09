using ErsteApi.Exchange;
using System;
using System.IO;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string apiKey;
            using (StreamReader reader = new StreamReader("api-secret.txt"))
            {
                apiKey = reader.ReadToEnd();
            }
            ExchangeRate er = new ExchangeRate(apiKey);
            Console.WriteLine("ExchangeRate API is {0}.", er.HealthCheck() ? "working" : "not working");
            //er.GetAllExchangeRatesInPeriod(from: DateTime.Today.AddDays(-5), to: DateTime.Today.AddDays(-1));
        }
    }
}

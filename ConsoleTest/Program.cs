using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErsteApi.Exchange;
namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ExchangeRate exchangeRate = new ExchangeRate(
                @"https://webapi.developers.erstegroup.com/api/csas/sandbox/v1",
                @"52d55cea-09d1-4e66-b70f-798f2b4feb32",
                "en");

            var curs = exchangeRate.GetAllCurrencies();
        }
    }
}

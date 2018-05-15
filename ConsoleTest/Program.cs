﻿using System;
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
            

            Test();
            Console.ReadLine();
           
        }

        static async void Test()
        {
            ExchangeRate exchangeRate = new ExchangeRate(
                @"52d55cea-09d1-4e66-b70f-798f2b4feb32",
                "en");

            var curs = await exchangeRate.GetAllCurrenciesAsync();

            Console.WriteLine("I have waited.");

        }
    }
}
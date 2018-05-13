using System;
using System.Collections.Generic;
using System.Text;

namespace ErsteApi.Exchange.Model
{
    public class CrossBetweenTwoCurrencies
    {
        /// <summary>
        /// First currency.
        /// </summary>
        public Currency CurrencyA { get; }
        /// <summary>
        /// Second currency.
        /// </summary>
        public Currency CurrencyB { get; }
        /// <summary>
        /// Course between selected currencies.
        /// </summary>
        public float CurrencyAmount { get; internal set; }
        /// <summary>
        /// DateTime from which exchange rate is valid.
        /// </summary>
        public DateTime ValidFrom { get; internal set; }
        /// <summary>
        /// Exchange rate for cash transactions - buy.
        /// </summary>
        public float BuyCashRate { get; internal set; }
        /// <summary>
        /// Exchange rate for cash transactions - sell.
        /// </summary>
        public float SellCashRate { get; internal set; }
        /// <summary>
        /// Exchange rate for cash transactions - middle.
        /// </summary>
        public float MiddleCashRate { get; internal set; }
        /// <summary>
        /// Exchange rate for non-cash transactions - buy.
        /// </summary>
        public float BuyCurrencyRate { get; internal set; }
        /// <summary>
        /// Exchange rate for non-cash transactions - sell.
        /// </summary>
        public float SellCurrencyRate { get; internal set; }
        /// <summary>
        /// Exchange rate for non-cash transactions - middle.
        /// </summary>
        public float MiddleCurrencyRate { get; internal set; }
        /// <summary>
        /// Change of exchange rates value opossite last state.
        /// </summary>
        public float CurrencyMove { get; internal set; }
        /// <summary>
        /// Middle exchange rate of Czech National Bank.
        /// </summary>
        public float CNBMiddleRate { get; internal set; }
        /// <summary>
        /// Order of exchange rate for selected day.
        /// </summary>
        public int Version { get; internal set; }
    }
}

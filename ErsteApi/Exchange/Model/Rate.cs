using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace ErsteApi.Exchange.Model
{
    public class Rate : IEquatable<Rate>
    {
        [JsonProperty("shortName")]
        internal string ShortName
        {
            set
            {
                Currency = ExchangeRate.currencies.FirstOrDefault(c => c.CurrencyCode == value);
            }
        }

        /// <summary>
        /// Currency of the rate.
        /// </summary>
        [JsonIgnore]
        public Currency Currency { get; internal set;}
        /// <summary>
        /// Amount of selected currency for conversion.
        /// </summary>
        [JsonProperty("amount")]
        public float Amount { get; internal set;}
        /// <summary>
        /// DateTime from which is this exchange rate valid.
        /// </summary>
        [JsonProperty("validFrom")]
        public DateTime ValidFrom { get; internal set;}
        /// <summary>
        /// Exchange rate for cash transactions - buy.
        /// </summary>
        [JsonProperty("valBuy")]
        public float BuyCashRate { get; internal set;}
        /// <summary>
        /// Exchange rate for cash transactions - sell.
        /// </summary>
        [JsonProperty("valSell")]
        public float SellCashRate { get; internal set;}
        /// <summary>
        /// Exchange rate for cash transactions - middle.
        /// </summary>
        [JsonProperty("valMid")]
        public float MiddleCashRate { get; internal set;}
        /// <summary>
        /// Exchange rate for non-cash transactions - buy.
        /// </summary>
        [JsonProperty("currBuy")]
        public float BuyCurrenceRate { get; internal set;}
        /// <summary>
        /// Exchange rate for non-cash transactions - sell.
        /// </summary>
        [JsonProperty("currSell")]
        public float SellCurrenceRate { get; internal set;}
        /// <summary>
        /// Exchange rate for non-cash transactions - middle.
        /// </summary>
        [JsonProperty("currMid")]
        public float MiddleCurrenceRate { get; internal set;}
        /// <summary>
        /// Change of exchange rates value opossite last state.
        /// </summary>
        [JsonProperty("move")]
        public float ChangeFromLastState { get; internal set;}
        /// <summary>
        /// Middle exchange rate of Czech National Bank.
        /// </summary>
        [JsonProperty("cnbMid")]
        public float CNBMiddleRate { get; internal set;}
        /// <summary>
        /// Order of exchange rate for selected day.
        /// </summary>
        [JsonProperty("version")]
        public int DayVersion { get; internal set;}

        public override bool Equals(object obj)
        {
            if (obj is Rate er)
                return Equals(er);

            return base.Equals(obj);
        }

        public bool Equals(Rate other)
        {
            return (
                this.Currency == other.Currency && this.ValidFrom == other.ValidFrom
                );
        }

        /// <summary>
        /// Generated.
        /// </summary>
        /// <returns>Hash code.</returns>
        public override int GetHashCode()
        {
            var hashCode = -521618878;
            hashCode = hashCode * -1521134295 + EqualityComparer<Currency>.Default.GetHashCode(Currency);
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTime>.Default.GetHashCode(ValidFrom);
            return hashCode;
        }
    }
}
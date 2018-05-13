using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ErsteApi.Exchange.Model
{
    public class Currency : IEquatable<Currency>
    {
        /// <summary>
        /// Name of country.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; internal set; }
        /// <summary>
        /// Name of currency.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; }
        /// <summary>
        /// Code of currency.
        /// </summary>
        [JsonProperty("shortName")]
        public string CurrencyCode { get; internal set; }
        /// <summary>
        /// Long name of currency.
        /// </summary>
        [JsonProperty("longName")]
        public string LongName { get; internal set; }

        public override string ToString()
        {
            return CurrencyCode;
        }

        public override bool Equals(object obj)
        {
            if (obj is Currency currency)
                return (Equals(currency));
            return base.Equals(obj);
        }

        public bool Equals(Currency other)
        {
            return (this.Country == other.Country && this.Name == other.Name && this.CurrencyCode == other.CurrencyCode && this.LongName == other.LongName);
        }

        public override int GetHashCode()
        {
            var hashCode = 1232445994;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Country);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CurrencyCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LongName);
            return hashCode;
        }
    }
}

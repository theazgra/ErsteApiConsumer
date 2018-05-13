using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ErsteApi.Exchange.Model
{
    class ConvertRequest
    {
        /// <summary>
        /// Code of currency from which to convert.
        /// </summary>
        [JsonProperty(PropertyName = "from")]
        public string From { get; set; }

        /// <summary>
        /// Code of currency to which to convert.
        /// </summary>
        [JsonProperty (PropertyName = "to")]
        public string To { get; set; }

        /// <summary>
        /// Type of conversion.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ConversionType ConversionType { get; set; }

        /// <summary>
        /// Amount of currency to convert.
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        public float Amount { get; set; }

        /// <summary>
        /// True if buy currency.
        /// </summary>
        [JsonProperty(PropertyName = "buy")]
        public bool Buy { get; set; }

        public ConvertRequest(string from, string to, ConversionType conversionType, float amount)
        {
            From = from;
            To = to;
            ConversionType = conversionType;
            Amount = amount;
            Buy = true;
        }

        public ConvertRequest(Currency from, Currency to, ConversionType conversionType, float amount) : this(from.CurrencyCode, to.CurrencyCode, conversionType, amount)
        {

        }
    }
}

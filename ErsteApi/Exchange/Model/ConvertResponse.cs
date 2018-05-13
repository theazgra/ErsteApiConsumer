using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ErsteApi.Exchange.Model
{
    public class ConvertResponse
    {
        public Currency FromCurrency { get; internal set;  }
        public Currency ToCurrency { get; internal set;  }

        /// <summary>
        /// Code of currency from which was converted.
        /// </summary>
        [JsonProperty(PropertyName = "from")]
        public string From { get; internal set;  }

        /// <summary>
        /// Code of currency to which was converted.
        /// </summary>
        [JsonProperty(PropertyName = "to")]
        public string To { get; internal set;  }

        /// <summary>
        /// Type of performed conversion.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ConversionType ConversionType { get; internal set;  }

        /// <summary>
        /// Amount of converted currency.
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        public float Amount { get; internal set;  }

        /// <summary>
        /// IF true count 'result' by value sell, IF false count 'result' by value buy
        /// </summary>
        [JsonProperty(PropertyName = "buy")]
        public bool Buy { get; internal set;  }

        /// <summary>
        /// The resulting amount for transfers to the selected currency.
        /// </summary>
        [JsonProperty(PropertyName = "result")]
        public float Result { get; internal set;  }

        /// <summary>
        /// Date from which exchange rate is valid.
        /// </summary>
        [JsonProperty(PropertyName = "validFrom")]
        public DateTime ValidFrom { get; internal set;  }
        

        
    }
}

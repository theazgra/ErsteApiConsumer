using System;
using System.Collections.Generic;

namespace ErsteApi.Exchange.Model
{
    public class Rate : IEquatable<Rate>
    {
        /// <summary>
        /// Currency of the rate.
        /// </summary>
        public Currency Currency { get; }

        /// <summary>
        /// Amount of selected currency for conversion.
        /// </summary>
        public float Amount { get; }
        /// <summary>
        /// DateTime from which is this exchange rate valid.
        /// </summary>
        public DateTime ValidFrom { get; }
        /// <summary>
        /// Exchange rate for cash transactions - buy.
        /// </summary>
        public float BuyCashRate { get; }
        /// <summary>
        /// Exchange rate for cash transactions - sell.
        /// </summary>
        public float SellCashRate { get; }
        /// <summary>
        /// Exchange rate for cash transactions - middle.
        /// </summary>
        public float MiddleCashRate { get; }
        /// <summary>
        /// Exchange rate for non-cash transactions - buy.
        /// </summary>
        public float BuyCurrenceRate { get; }
        /// <summary>
        /// Exchange rate for non-cash transactions - sell.
        /// </summary>
        public float SellCurrenceRate { get; }
        /// <summary>
        /// Exchange rate for non-cash transactions - middle.
        /// </summary>
        public float MiddleCurrenceRate { get; }
        /// <summary>
        /// Change of exchange rates value opossite last state.
        /// </summary>
        public float ChangeFromLastState { get; }
        /// <summary>
        /// Middle exchange rate of Czech National Bank.
        /// </summary>
        public float CNBMiddleRate { get; }
        /// <summary>
        /// Order of exchange rate for selected day.
        /// </summary>
        public int DayVersion { get; }

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
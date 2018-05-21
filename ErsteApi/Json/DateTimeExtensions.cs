using System;
using System.Collections.Generic;
using System.Text;

namespace ErsteApi.Json
{
    internal static class DateTimeExtensions
    {
        /// <summary>
        /// Format DateTime to YYYY-MM-DD format.
        /// </summary>
        /// <param name="dateTime">DateTime to parse</param>
        /// <returns>DateTime formatted to YYYY-MM-DD format.</returns>
        internal static string ToRestFormat(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }
    }
}

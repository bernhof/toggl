using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Toggl.Models.Reports
{
    /// <summary>
    /// Represents a partial (paged) result set.
    /// </summary>
    /// <typeparam name="T">Type of result</typeparam>
    public class PagedReportResult<T> : PagedResult<T>
    {
        /// <summary>
        /// Total amount of logged time in current report in milliseconds
        /// </summary>
        [JsonProperty("total_grand")]
        public int TotalMilliseconds { get; set; }

        /// <summary>
        /// Total amount of logged time in current report
        /// </summary>
        [JsonIgnore]
        public TimeSpan TotalTime => TimeSpan.FromMilliseconds(TotalMilliseconds);

        /// <summary>
        /// Total amount of billable time in current report in milliseconds
        /// </summary>
        [JsonProperty("total_billable")]
        public int TotalBillableMilliseconds { get; set; }

        /// <summary>
        /// Total amount of billable time in current report
        /// </summary>
        [JsonIgnore]
        public TimeSpan TotalBillableTime => TimeSpan.FromMilliseconds(TotalBillableMilliseconds);

        /// <summary>
        /// Specifies total amount in relevant currencies
        /// </summary>
        [JsonProperty("total_currencies")]
        public List<CurrencyAmount> CurrencyAmounts { get; set; }
    }
}

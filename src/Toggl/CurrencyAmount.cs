using Newtonsoft.Json;

namespace Toggl.Models
{
    /// <summary>
    /// Describes an amount of money in a specific currency
    /// </summary>
    public class CurrencyAmount
    {
        /// <summary>
        /// Currency in which <see cref="Amount"/> is specified
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }
        /// <summary>
        /// Amount in specified <see cref="Currency"/>
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
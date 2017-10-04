using Newtonsoft.Json;
using System.Collections.Generic;

namespace Toggl.Models
{
    /// <summary>
    /// Represents a partial (paged) result set.
    /// </summary>
    /// <typeparam name="T">Type of result</typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// Returns total number of items in result set, of which potentially only a subset is represented by this instance
        /// </summary>
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        /// <summary>
        /// Name of field by which items are sorted
        /// </summary>
        [JsonProperty("sort_field")]
        public string SortField { get; set; }

        /// <summary>
        /// Sort order of items (ascending or descending)
        /// </summary>
        [JsonProperty("sort_order")]
        public string SortOrder { get; set; }

        /// <summary>
        /// Current page number represented by this result set
        /// </summary>
        [JsonProperty("page")]
        public int Page { get; set; }

        /// <summary>
        /// Number of items per page
        /// </summary>
        [JsonProperty("per_page")]
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// Items on this page of the result set
        /// </summary>
        [JsonProperty("data")]
        public List<T> Items { get; set; }
    }
}

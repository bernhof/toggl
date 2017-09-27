using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toggl.Models
{
    public class PagedResult<T>
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
        [JsonProperty("sort_field")]
        public string SortField { get; set; }
        [JsonProperty("sort_order")]
        public string SortOrder { get; set; }
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("per_page")]
        public int ItemsPerPage { get; set; }
        [JsonProperty("data")]
        public List<T> Items { get; set; }
    }
}

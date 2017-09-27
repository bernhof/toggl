using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toggl.Models
{
    public partial class TimeEntry : IBaseModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("workspace_id")]
        public long WorkspaceId { get; set; }
        [JsonProperty("project_id")]
        public long? ProjectId { get; set; }
        [JsonProperty("task_id")]
        public long? TaskId { get; set; }
        [JsonProperty("billable")]
        public bool Billable { get; set; }
        [JsonProperty("start")]
        public DateTimeOffset Start { get; set; }
        [JsonProperty("stop")]
        public DateTimeOffset? Stop { get; set; }
        [JsonProperty("duration")]
        public int DurationSeconds { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("tag_names")]
        public IList<string> TagNames { get; set; }
        [JsonProperty("tag_ids")]
        public IList<long> TagIds { get; set; }
        [JsonProperty("at"), JsonIgnore]
        public DateTimeOffset LastUpdated { get; set; }
        [JsonProperty("server_deleted_at")]
        public DateTimeOffset? ServerDeletedAt { get; set; }
        [JsonProperty("user_id")]
        public long UserId { get; set; }
        /// <summary>
        /// Name of client application that was used to create the entry.
        /// </summary>
        [JsonProperty("created_with")]
        public string CreatedWith { get; set; }
    }
}

using System;
using Newtonsoft.Json;

namespace Toggl.Models
{
    public partial class Task : IBaseModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("project_id")]
        public long ProjectId { get; set; }
        [JsonProperty("workspace_id")]
        public long WorkspaceId { get; set; }
        [JsonProperty("uid")]
        public long? UserId { get; set; }
        [JsonProperty("estimated_seconds")]
        public long EstimatedSeconds { get; set; }
        [JsonProperty("active")]
        public bool Active { get; set; }
        [JsonProperty("at"), JsonIgnore]
        public DateTimeOffset LastUpdated { get; set; }
        [JsonProperty("tracked_seconds")]
        public long TrackedSeconds { get; set; }
    }
}
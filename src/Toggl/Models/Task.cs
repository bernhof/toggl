using System;
using Newtonsoft.Json;

namespace Toggl.Models
{
    /// <summary>
    /// A task.
    /// </summary>
    public partial class Task : IEntity
    {
        /// <summary>
        /// Task ID
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }
        
        /// <summary>
        /// Task name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// ID of project to which task belongs
        /// </summary>
        [JsonProperty("project_id")]
        public long ProjectId { get; set; }

        /// <summary>
        /// ID of workspace to which task belongs
        /// </summary>
        [JsonProperty("workspace_id")]
        public long WorkspaceId { get; set; }

        [JsonProperty("uid")]
        public long? UserId { get; set; }

        /// <summary>
        /// Task estimated duration in seconds
        /// </summary>
        [JsonProperty("estimated_seconds")]
        public long EstimatedSeconds { get; set; }

        /// <summary>
        /// Specifies whether task is active. If false, task is considered archived. Default is true.
        /// </summary>
        [JsonProperty("active")]
        public bool Active { get; set; } = true;

        /// <summary>
        /// Date/time on which task was last modified
        /// </summary>
        [JsonProperty("at"), JsonIgnore]
        public DateTimeOffset LastModified { get; set; }

        /// <summary>
        /// Total number of seconds tracked on this task
        /// </summary>
        [JsonProperty("tracked_seconds")]
        public long TrackedSeconds { get; set; }
    }
}
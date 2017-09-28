using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toggl.Models
{
    /// <summary>
    /// A time entry.
    /// </summary>
    public partial class TimeEntry : IEntity
    {
        /// <summary>
        /// Time entry ID
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// ID of workspace to which entry belongs
        /// </summary>
        [JsonProperty("workspace_id")]
        public long WorkspaceId { get; set; }

        /// <summary>
        /// ID of project to which entry is associated
        /// </summary>
        [JsonProperty("project_id")]
        public long? ProjectId { get; set; }

        /// <summary>
        /// ID of task to which entry is associated
        /// </summary>
        [JsonProperty("task_id")]
        public long? TaskId { get; set; }

        /// <summary>
        /// Specifies whether time logged in this entry is billable
        /// </summary>
        [JsonProperty("billable")]
        public bool Billable { get; set; }

        /// <summary>
        /// Start time of entry
        /// </summary>
        [JsonProperty("start")]
        public DateTimeOffset Start { get; set; }

        /// <summary>
        /// End time of entry
        /// </summary>
        [JsonProperty("stop")]
        public DateTimeOffset? Stop { get; set; }

        /// <summary>
        /// Total time in seconds represented by this entry
        /// </summary>
        [JsonProperty("duration")]
        public int DurationSeconds { get; set; }

        /// <summary>
        /// Description associated with entry
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Names of tags associated with this entry
        /// </summary>
        [JsonProperty("tag_names")]
        public IList<string> TagNames { get; set; }

        /// <summary>
        /// IDs of tags associated with this entry
        /// </summary>
        [JsonProperty("tag_ids")]
        public IList<long> TagIds { get; set; }

        /// <summary>
        /// Date/time on which entry was last modified
        /// </summary>
        [JsonProperty("at"), JsonIgnore]
        public DateTimeOffset LastModified { get; set; }

        /// <summary>
        /// Date/time on which entry was deleted
        /// </summary>
        [JsonProperty("server_deleted_at")]
        public DateTimeOffset? ServerDeletedAt { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// Name of client application that was used to create the entry.
        /// This value is read from User-Agent header included in the request that created the entry, see <see cref="TogglClient.UserAgent"/>.
        /// </summary>
        [JsonProperty("created_with")]
        public string CreatedWith { get; set; }
    }
}

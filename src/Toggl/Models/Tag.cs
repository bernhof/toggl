using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toggl.Models
{
    /// <summary>
    /// A tag.
    /// </summary>
    public partial class Tag : IBaseModel
    {
        /// <summary>
        /// Tag ID
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// ID of workspace to which tag belongs
        /// </summary>
        [JsonProperty("workspace_id")]
        public long WorkspaceId { get; set; }

        /// <summary>
        /// Tag name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Date/time on which tag was last modified
        /// </summary>
        [JsonProperty("at"), JsonIgnore]
        public DateTimeOffset LastModified { get; set; }

        //[JsonConstructor]
        //public Tag(long id, long workspaceId, string name, DateTimeOffset? at)
        //{
        //    Id = id;
        //    WorkspaceId = workspaceId;
        //    Name = name;
        //    At = at ?? new DateTimeOffset(DateTime.UtcNow);
        //}
    }
}

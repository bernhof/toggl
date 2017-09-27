using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toggl.Models
{
    public partial class Tag : IBaseModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("workspace_id")]
        public long WorkspaceId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("at"), JsonIgnore]
        public DateTimeOffset LastUpdated { get; set; }

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

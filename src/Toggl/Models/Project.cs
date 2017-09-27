using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toggl.Models
{
    /*
     {"id":62092015,
     "workspace_id":2220820,
     "client_id":27279539,
     "name":"blah",
     "is_private":false,
     "active":true,
     "at":"2017-08-22T16:07:58+00:00",
     "created_at":"2017-08-22T16:07:58+00:00",
     "server_deleted_at":null,
     "color":"#06aaf5",
     "billable":true,
     "template":false,
     "auto_estimates":false,
     "estimated_hours":null,
     "rate":null,
     "currency":null,
     "actual_hours":0,
     "wid":2220820,
     "cid":27279539}
     */

    public partial class Project : IBaseModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("client_id")]
        public long? ClientId { get; set; }

        [JsonProperty("workspace_id")]
        public long WorkspaceId { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("billable")]
        public bool Billable { get; set; }

        [JsonProperty("is_private")]
        public bool Private { get; set; }

        [JsonProperty("active")]
        public bool Active { get => m_Active; set => m_Active = value; }
        private bool m_Active = true;
    }
}

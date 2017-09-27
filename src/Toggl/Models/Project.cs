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

    /// <summary>
    /// A project.
    /// </summary>
    public partial class Project : IBaseModel
    {
        /// <summary>
        /// Project ID
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Project name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// ID of client to which project belongs
        /// </summary>
        [JsonProperty("client_id")]
        public long? ClientId { get; set; }

        /// <summary>
        /// ID of workspace to which project belongs
        /// </summary>
        [JsonProperty("workspace_id")]
        public long WorkspaceId { get; set; }

        /// <summary>
        /// Project color (HTML/CSS style, e.g. &quot;#0033ff&quot;)
        /// </summary>
        [JsonProperty("color")]
        public string Color { get; set; }

        /// <summary>
        /// Specifies whether time logged on project is billable. Default is false.
        /// </summary>
        [JsonProperty("billable")]
        public bool Billable { get; set; }
        
        [JsonProperty("is_private")]
        public bool Private { get; set; }

        /// <summary>
        /// Specifies whether project is active. If false, project is considered archived.
        /// Default is true.
        /// </summary>
        [JsonProperty("active")]
        public bool Active { get => m_Active; set => m_Active = value; }
        private bool m_Active = true; // default value: true
    }
}

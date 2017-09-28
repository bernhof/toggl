using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toggl.Models
{
    /// <summary>
    /// A client.
    /// </summary>
    public partial class Client : IEntity
    {
        /*
         {
         "id":27279539,
         "wid":2220820,
         "name":"blah",
         "at":"2017-08-22T08:18:19+00:00"
         }
        */

        /// <summary>
        /// Client ID
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Workspace ID
        /// </summary>
        [JsonProperty("wid")]
        public long WorkspaceId { get; set; }

        /// <summary>
        /// Client name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Date/time on which client was last modified
        /// </summary>
        [JsonProperty("at"), JsonIgnore]
        public DateTimeOffset LastModified { get; set; }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toggl.Models
{
    public partial class Client : IBaseModel
    {
        /*
         {
         "id":27279539,
         "wid":2220820,
         "name":"blah",
         "at":"2017-08-22T08:18:19+00:00"
         }
        */
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("wid")]
        public long WorkspaceId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("at")]
        public DateTime LastUpdated { get; set; }

    }
}

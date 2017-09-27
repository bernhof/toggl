using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toggl.Models
{
    public partial class WorkspaceUser
    {
        /*
         * [{"
         * id":3248907,"
         * fullname":"Søren Dommerby","
         * email":"sd@nddata.dk","
         * is_active":true,"
         * is_admin":false,"
         * inactive":false},{"
         * id":3216134,"
         * fullname":"Mikkel Bernhof Jakobsen","
         * email":"mj@nddata.dk","
         * is_active":true,"
         * is_admin":true,"
         * inactive":false}]
        */

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        [JsonProperty("is_active")]
        public bool Active { get => _active; set => _active = value; }
        private bool _active = true;

        [JsonProperty("is_admin")]
        public bool Admin { get; set; }
    }
}

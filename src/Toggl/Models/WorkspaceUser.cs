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
         * fullname":"Somebody","
         * email":"her@email.com","
         * is_active":true,"
         * is_admin":false,"
         * inactive":false},{"
         * id":3216134,"
         * fullname":"Another somebody","
         * email":"his@email.com","
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

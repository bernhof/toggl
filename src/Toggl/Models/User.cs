using Newtonsoft.Json;

namespace Toggl.Models
{
    /// <summary>
    /// A user in a workspace
    /// </summary>
    public partial class User
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

        /// <summary>
        /// Workspace user ID
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Email of user
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Full name of user
        /// </summary>
        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        /// <summary>
        /// Specifies whether the user is active. If false, user is deactivated and cannot access the workspace. Default is true.
        /// </summary>
        [JsonProperty("is_active")]
        public bool Active { get; set; } = true;

        /// <summary>
        /// Specifies whether user is a workspace administrator. Default is false.
        /// </summary>
        [JsonProperty("is_admin")]
        public bool IsAdmin { get; set; }
    }
}

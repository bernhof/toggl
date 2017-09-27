using Newtonsoft.Json;

namespace Toggl.Models
{
    /// <summary>
    /// A workspace.
    /// </summary>
    public partial class Workspace : IBaseModel
    {
        /*
         v8:
         
        "id":3134975,
		"name":"John's personal ws",
		"premium":true,
		"admin":true,
		"default_hourly_rate":50,
		"default_currency":"USD",
		"only_admins_may_create_projects":false,
		"only_admins_see_billable_rates":true,
		"rounding":1,
		"rounding_minutes":15,
		"at":"2013-08-28T16:22:21+00:00",
		"logo_url":"my_logo.png"

            v9:

            {"id":2220820,
            "name":"blah",
            "profile":100,
            "premium":true,
            "business_ws":false,
            "admin":true,
            "suspended_at":null,
            "server_deleted_at":null,
            "default_hourly_rate":0,
            "default_currency":"USD",
            "only_admins_may_create_projects":false,
            "only_admins_see_billable_rates":false,
            "only_admins_see_team_dashboard":false,
            "projects_billable_by_default":true,
            "rounding":0,
            "rounding_minutes":0,
            "api_token":"--",
            "at":"2017-08-22T08:09:26+00:00",
            "logo_url":"https://assets.toggl.com/images/workspace.jpg",
            "ical_url":"/ical/workspace_user/somethingsomethine",
            "ical_enabled":true,
            "csv_upload":null,
            "subscription":null}
        */

        /// <summary>
        /// Workspace ID
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Workspace name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        // todo: missing properties
    }
}

using Newtonsoft.Json;
using System;

namespace Toggl.Models
{
    /// <summary>
    /// User information
    /// </summary>
    public partial class CurrentUser : IEntity
    {
        /// <summary>
        /// User ID
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// User API token
        /// </summary>
        [JsonProperty("api_token")]
        public string ApiToken { get; set; }

        /// <summary>
        /// ID of default workspace
        /// </summary>
        [JsonProperty("default_workspace_id")]
        public long DefaultWorkspaceId { get; set; }

        /// <summary>
        /// User email address
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Full name of user
        /// </summary>
        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        /// <summary>
        /// Determines how time of day is displayed to user
        /// </summary>
        [JsonProperty("timeofday_format")]
        public string TimeOfDayFormat { get; set; }

        /// <summary>
        /// Determines how dates are displayed to user
        /// </summary>
        [JsonProperty("date_format")]
        public string DateFormat { get; set; }

        /// <summary>
        /// Determines whether start and stop times are stored with time entries. If false, only duration is stored.
        /// </summary>
        [JsonProperty("store_start_and_stop_time")]
        public bool StoreStartAndStopTime { get; set; }

        /// <summary>
        /// Determines which day is considered the first day of the week
        /// </summary>
        [JsonProperty("beginning_of_week")]
        public DayOfWeek BeginningOfWeek { get; set; }

        /// <summary>
        /// User's interface language
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// URL to user avatar image
        /// </summary>
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("sidebar_piechart")]
        public bool SidebarPiechart { get; set; }

        /// <summary>
        /// Date/time on which user was last modified
        /// </summary>
        [JsonProperty("at"), JsonIgnore]
        public DateTimeOffset At { get; set; }

        [JsonProperty("retention")]
        public int Retention { get; set; }

        /// <summary>
        /// Determines whether to record a timeline for user
        /// </summary>
        [JsonProperty("record_timeline")]
        public bool RecordTimeline { get; set; }

        /// <summary>
        /// Determines whether to render timeline to user
        /// </summary>
        [JsonProperty("render_timeline")]
        public bool RenderTimeline { get; set; }

        /// <summary>
        /// Determines whether the timeline feature is enabled for user
        /// </summary>
        [JsonProperty("timeline_enabled")]
        public bool TimelineEnabled { get; set; }

        //[JsonProperty("timeline_experiment")]
        //public bool TimelineExperiment { get; set; }
    }
}
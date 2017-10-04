using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Toggl.Models.Reports
{
    public class DetailedReportItem
    {
        /*
        {
            "id":43669578,
            "pid":1930589,
            "tid":null,
            "uid":777,
            "description":"tegin tööd",
            "start":"2013-05-20T06:55:04",
            "end":"2013-05-20T10:55:04",
            "updated":"2013-05-20T13:56:04",
            "dur":14400000,
            "user":"John Swift",
            "use_stop":true,
            "client":"Avies",
            "project":"Toggl Desktop",
            "project_color":"0",
            "project_hex_color":"#000000",
            "task":null,
            "billable":8.00,
            "is_billable":true,
            "cur":"EUR",
            "tags":["paid"]
          },{
            "id":43669579,
            "pid":1930625,
            "tid":1334973,
            "uid":7776,
            "description":"agee",
            "start":"2013-05-20T09:37:00",
            "end":"2013-05-20T12:01:41",
            "updated":"2013-05-20T15:01:41",
            "dur":8645000,
            "user":"John Swift",
            "use_stop":true,
            "client":"Apprise",
            "project":"Development project",
            "project_color":"0",
            "project_hex_color":"#000000",
            "task":"Work hard",
            "billable":120.07,
            "is_billable":true,
            "cur":"EUR",
            "tags":[]
          } 
          */

        /// <summary>
        /// Time entry ID
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Project ID
        /// </summary>
        [JsonProperty("pid")]
        public long ProjectId { get; set; }

        /// <summary>
        /// Task ID
        /// </summary>
        [JsonProperty("tid")]
        public long TaskId { get; set; }

        /// <summary>
        /// User ID
        /// </summary>
        [JsonProperty("uid")]
        public long UserId { get; set; }

        /// <summary>
        /// Time entry description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Time entry start time
        /// </summary>
        [JsonProperty("start")]
        public DateTimeOffset Start { get; set; }

        /// <summary>
        /// Time entry end time
        /// </summary>
        [JsonProperty("end")]
        public DateTimeOffset End { get; set; }

        /// <summary>
        /// Date/time time entry was last updated
        /// </summary>
        [JsonProperty("updated")]
        public DateTimeOffset LastModified { get; set; }

        /// <summary>
        /// Time entry duration in milliseconds
        /// </summary>
        [JsonProperty("dur")]
        public long DurationMilliseconds { get; set; }

        /// <summary>
        /// Time entry duration
        /// </summary>
        [JsonIgnore]
        public TimeSpan Duration => TimeSpan.FromMilliseconds(DurationMilliseconds);

        /// <summary>
        /// Name of user who created time entry
        /// </summary>
        [JsonProperty("user")]
        public string UserName { get; set; }

        /// <summary>
        /// Determines whether stop time is saved on entry. Depends on user's personal settings.
        /// </summary>
        [JsonProperty("use_stop")]
        public bool StopTimeAvailable { get; set; }

        /// <summary>
        /// Client name
        /// </summary>
        [JsonProperty("client")]
        public string ClientName { get; set; }

        /// <summary>
        /// Project name
        /// </summary>
        [JsonProperty("project")]
        public string ProjectName { get; set; }

        /// <summary>
        /// Project color
        /// </summary>
        [JsonProperty("project_ color")]
        public int ProjectColor { get; set; }

        /// <summary>
        /// Project color (hexadecimal, e.g. #aa33ff)
        /// </summary>
        [JsonProperty("project_hex_color")]
        public string ProjectColorHex { get; set; }

        /// <summary>
        /// Task name
        /// </summary>
        [JsonProperty("task")]
        public string TaskName { get; set; }

        /// <summary>
        /// Determines whether time is billable
        /// </summary>
        [JsonProperty("is_billable")]
        public bool IsBillable { get; set; }

        /// <summary>
        /// Billable amount in specified <see cref="Currency"/>
        /// </summary>
        [JsonProperty("billable")]
        public decimal BillableAmount { get; set; }

        /// <summary>
        /// Currency in which <see cref="BillableAmount"/> is specified
        /// </summary>
        [JsonProperty("cur")]
        public string Currency { get; set; }

        /// <summary>
        /// Tags assocated with entry
        /// </summary>
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
    }
}

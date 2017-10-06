using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggl.Models.Reports
{
    /// <summary>
    /// Fields that weekly reports can be ordered by
    /// </summary>
    public enum WeeklyReportOrderBy
    {
        Title,
        Day1,
        Day2,
        Day3,
        Day4,
        Day5,
        Day6,
        Day7,
        WeekTotal,
    }

    /// <summary>
    /// Fields that summary reports can be ordered by
    /// </summary>
    public enum SummaryReportOrderBy
    {
        Title,
        Duration,
        Amount,
    }

    /// <summary>
    /// Fields that detailed reports can be ordered by
    /// </summary>
    public enum DetailedReportOrderBy
    {
        Date,
        Description,
        Duration,
        User,
    }

    /// <summary>
    /// Options for weekly reports
    /// </summary>
    public class WeeklyReportOptions : ReportOptionsBase
    {
        private WeeklyReportOrderBy? _orderBy;

        /// <summary>
        /// Determines which field to order results by
        /// </summary>
        public WeeklyReportOrderBy? OrderBy
        {
            get => _orderBy;
            set
            {
                _orderBy = value;
                switch (value)
                {
                    case WeeklyReportOrderBy.Title: OrderField = "title"; break;
                    case WeeklyReportOrderBy.Day1: OrderField = "day1"; break;
                    case WeeklyReportOrderBy.Day2: OrderField = "day2"; break;
                    case WeeklyReportOrderBy.Day3: OrderField = "day3"; break;
                    case WeeklyReportOrderBy.Day4: OrderField = "day4"; break;
                    case WeeklyReportOrderBy.Day5: OrderField = "day5"; break;
                    case WeeklyReportOrderBy.Day6: OrderField = "day6"; break;
                    case WeeklyReportOrderBy.Day7: OrderField = "day7"; break;
                    case WeeklyReportOrderBy.WeekTotal: OrderField = "week_total"; break;
                    case null: OrderField = null; break;
                    default: throw new NotSupportedException("Specified order field is not supported");
                }
            }
        }
    }

    /// <summary>
    /// Options for summary reports
    /// </summary>
    public class SummaryReportOptions : ReportOptionsBase
    {
        private SummaryReportOrderBy? _orderBy;

        /// <summary>
        /// Determines which field to order results by
        /// </summary>
        public SummaryReportOrderBy? OrderBy
        {
            get => _orderBy;
            set
            {
                _orderBy = value;
                switch (value)
                {
                    case SummaryReportOrderBy.Title: OrderField = "title"; break;
                    case SummaryReportOrderBy.Duration: OrderField = "duration"; break;
                    case SummaryReportOrderBy.Amount: OrderField = "amount"; break;
                    case null: OrderField = null; break;
                    default: throw new NotSupportedException("Specified order field is not supported");
                }
            }
        }
    }

    /// <summary>
    /// Options for detailed reports
    /// </summary>
    public class DetailedReportOptions : ReportOptionsBase
    {
        private DetailedReportOrderBy? _orderBy;

        /// <summary>
        /// Determines which field to order results by
        /// </summary>
        public DetailedReportOrderBy? OrderBy
        {
            get => _orderBy;
            set
            {
                _orderBy = value;
                switch (value)
                {
                    case DetailedReportOrderBy.Date: OrderField = "date"; break;
                    case DetailedReportOrderBy.Description: OrderField = "description"; break;
                    case DetailedReportOrderBy.Duration: OrderField = "duration"; break;
                    case DetailedReportOrderBy.User: OrderField = "user"; break;
                    case null: OrderField = null; break;
                    default: throw new NotSupportedException("The specified order field is not supported");
                }
            }
        }
    }

    /// <summary>
    /// Common report options
    /// </summary>
    public abstract class ReportOptionsBase
    {
        //[JsonProperty("user_agent")]
        //public string UserAgent { get; set; }

        //[JsonProperty("workspace_id")]
        //public int WorkspaceId { get; set; }

        /// <summary>
        /// Earliest date to include in report
        /// </summary>
        [JsonProperty("since")]
        public DateTimeOffset? Since { get; set; }

        /// <summary>
        /// Latest date to include in report
        /// </summary>
        [JsonProperty("until")]
        public DateTimeOffset? Until { get; set; }

        /// <summary>
        /// Filters results to include only billable entries, non-billable entries or both.
        /// </summary>
        [JsonProperty("billable")]
        public BothBool? Billable { get; set; }

        /// <summary>
        /// Filters results to include only time entries associated with the specified client IDs. Add 0 to the list to include time entries without a client.
        /// </summary>
        [JsonProperty("client_ids")]
        public List<long> ClientIds { get; set; }

        /// <summary>
        /// Filters results to include only time entries associated with these projects. Use 0 to include time entries without a project.
        /// </summary>
        [JsonProperty("project_ids")]
        public List<long> ProjectIds { get; set; }

        /// <summary>
        /// Filters results to include only time entries created by these users.
        /// </summary>
        [JsonProperty("user_ids")]
        public List<long> UserIds { get; set; }

        /// <summary>
        /// Filters results to include only time entries created by members of the specified groups. This will limit user IDs specified in <see cref="UserIds"/> to members of these groups.
        /// </summary>
        [JsonProperty("members_of_group_ids")]
        public List<long> MembersOfGroupIds { get; set; }

        [JsonProperty("or_members_of_group_ids")]
        public List<long> OrMembersOfGroupIds { get; set; }

        /// <summary>
        /// Filters results to include only time entries with the specified tags. Use 0 to include time entries without any tags.
        /// </summary>
        public List<long> TagIds { get; set; }

        /// <summary>
        /// Filters results to include only time entries associated with the specified tasks. Use 0 to include time entries not associated with any task.
        /// </summary>
        public List<long> TaskIds { get; set; }

        /// <summary>
        /// Filters results to include only the specified time entries.
        /// </summary>
        public List<long> TimeEntryIds { get; set; }

        /// <summary>
        /// Filters results to include only time entries whose description matches this string
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// If false, entries without a description will not be included in results
        /// </summary>
        public bool? WithoutDescription { get; set; }

        /// <summary>
        /// Name of field by which results are ordered
        /// </summary>
        protected string OrderField { get; set; }

        /// <summary>
        /// Specifies whether to return results in descending order.
        /// </summary>
        public bool? OrderDescending { get; set; }

        public bool? DistinctRates { get; set; }

        /// <summary>
        /// Specifies whether to round time according to workspace settings
        /// </summary>
        public bool? Rounding { get; set; }

        /// <summary>
        /// Specifies whether to display hours as a decimal number. If false, hours are displayed with minutes rather than as a decimal number.
        /// </summary>
        public bool? DisplayHoursAsDecimal { get; set; }

        /// <summary>
        /// Builds a query string that can be passed to report endpoints, not including the ?.
        /// </summary>
        /// <returns>Query string</returns>
        public override string ToString()
        {
            var queryString = new StringBuilder();
            void append(string propertyName, object propertyValue)
            {
                if (propertyValue == null) return;
                if (propertyValue is string stringValue && string.IsNullOrEmpty(stringValue)) return;
                if (queryString.Length > 0) queryString.Append("&");
                if (propertyValue is System.Collections.IList list)
                {
                    queryString.Append($"{propertyName}={string.Join(",", list.Cast<object>())}");
                }
                else
                {
                    queryString.Append($"{propertyName}={propertyValue}");
                }
            }

            //append("user_agent", UserAgent);
            //append("workspace_id", WorkspaceId);
            append("since", Since?.ToString("o"));
            append("until", Until?.ToString("o"));
            append("billable", Billable?.ToYesNoBoth());
            append("client_ids", ClientIds);
            append("project_ids", ProjectIds);
            append("user_ids", UserIds);
            append("members_of_group_ids", MembersOfGroupIds);
            append("or_members_of_group_ids", OrMembersOfGroupIds);
            append("tag_ids", TagIds);
            append("task_ids", TaskIds);
            append("time_entry_ids", TimeEntryIds);
            append("description", System.Net.WebUtility.UrlEncode(Description));
            append("without_description", OnOff(WithoutDescription));
            append("order_field", OrderField);
            append("order_desc", OnOff(OrderDescending));
            append("distinct_rates", OnOff(DistinctRates));
            append("rounding", OnOff(Rounding));
            append("display_hours",
                (DisplayHoursAsDecimal == null) ? null :
                (DisplayHoursAsDecimal.Value ? "decimal" : "minutes"));

            return queryString.ToString();
        }

        /// <summary>
        /// Yields "on" if <paramref name="value"/> is true, "off" if <paramref name="value"/> is false, null if value is null
        /// </summary>
        /// <param name="value">Nullable boolean value</param>
        /// <returns>A string; "on", "off" or null.</returns>
        protected string OnOff(bool? value)
        {
            if (value == null) return null;
            return value.Value ? "on" : "off";
        }
    }
}

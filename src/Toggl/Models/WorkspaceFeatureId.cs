namespace Toggl.Models
{
    // taken from https://github.com/toggl/mobileapp/blob/develop/Toggl.Multivac/WorkspaceFeatureId.cs

    /// <summary>
    /// Workspace feature IDs
    /// </summary>
    public enum WorkspaceFeatureId
    {
        Free = 0,
        Pro = 13,
        ScheduledReports = 50,
        TimeAudits = 51,
        LockingTimeEntries = 52,
        EditTeamMemberTimeEntries = 53,
        EditTeamMemberProfile = 54,
        TrackingReminders = 55,
        TimeEntryConstraints = 56,
        PrioritySupport = 57,
        LabourCost = 58,
        ReportEmployeeProfitability = 59,
        ReportProjectProfitability = 60,
        ReportComparative = 61,
        ReportDataTrends = 62
    }
}
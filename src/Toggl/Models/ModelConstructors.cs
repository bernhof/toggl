namespace Toggl.Models
{
    public partial class Client
    {
        [System.Diagnostics.DebuggerStepThrough]
        public Client() { }

        public Client(Client basedOn)
        {
            Id = basedOn.Id;
            WorkspaceId = basedOn.WorkspaceId;
            Name = basedOn.Name;
            LastModified = basedOn.LastModified;
        }
    }

    public partial class CurrentUser
    {
        [System.Diagnostics.DebuggerStepThrough]
        public CurrentUser() { }

        public CurrentUser(CurrentUser basedOn)
        {
            Id = basedOn.Id;
            ApiToken = basedOn.ApiToken;
            DefaultWorkspaceId = basedOn.DefaultWorkspaceId;
            Email = basedOn.Email;
            Fullname = basedOn.Fullname;
            TimeOfDayFormat = basedOn.TimeOfDayFormat;
            DateFormat = basedOn.DateFormat;
            StoreStartAndStopTime = basedOn.StoreStartAndStopTime;
            BeginningOfWeek = basedOn.BeginningOfWeek;
            Language = basedOn.Language;
            ImageUrl = basedOn.ImageUrl;
            SidebarPiechart = basedOn.SidebarPiechart;
            At = basedOn.At;
            Retention = basedOn.Retention;
            RecordTimeline = basedOn.RecordTimeline;
            RenderTimeline = basedOn.RenderTimeline;
            TimelineEnabled = basedOn.TimelineEnabled;
        }
    }

    public partial class Project
    {
        [System.Diagnostics.DebuggerStepThrough]
        public Project() { }

        public Project(Project basedOn)
        {
            Id = basedOn.Id;
            Name = basedOn.Name;
            ClientId = basedOn.ClientId;
            WorkspaceId = basedOn.WorkspaceId;
            Color = basedOn.Color;
            Billable = basedOn.Billable;
            Private = basedOn.Private;
            Active = basedOn.Active;
        }
    }

    public partial class Tag
    {
        [System.Diagnostics.DebuggerStepThrough]
        public Tag() { }

        public Tag(Tag basedOn)
        {
            Id = basedOn.Id;
            WorkspaceId = basedOn.WorkspaceId;
            Name = basedOn.Name;
            LastModified = basedOn.LastModified;
        }
    }

    public partial class Task
    {
        [System.Diagnostics.DebuggerStepThrough]
        public Task() { }

        public Task(Task basedOn)
        {
            Id = basedOn.Id;
            Name = basedOn.Name;
            ProjectId = basedOn.ProjectId;
            WorkspaceId = basedOn.WorkspaceId;
            UserId = basedOn.UserId;
            EstimatedSeconds = basedOn.EstimatedSeconds;
            Active = basedOn.Active;
            LastModified = basedOn.LastModified;
            TrackedSeconds = basedOn.TrackedSeconds;
        }
    }

    public partial class TimeEntry
    {
        [System.Diagnostics.DebuggerStepThrough]
        public TimeEntry() { }

        public TimeEntry(TimeEntry basedOn)
        {
            Id = basedOn.Id;
            WorkspaceId = basedOn.WorkspaceId;
            ProjectId = basedOn.ProjectId;
            TaskId = basedOn.TaskId;
            Billable = basedOn.Billable;
            Start = basedOn.Start;
            Stop = basedOn.Stop;
            DurationSeconds = basedOn.DurationSeconds;
            Description = basedOn.Description;
            TagNames = basedOn.TagNames;
            TagIds = basedOn.TagIds;
            LastModified = basedOn.LastModified;
            ServerDeletedAt = basedOn.ServerDeletedAt;
            UserId = basedOn.UserId;
            CreatedWith = basedOn.CreatedWith;
        }
    }

    public partial class User
    {
        [System.Diagnostics.DebuggerStepThrough]
        public User() { }

        public User(User basedOn)
        {
            Id = basedOn.Id;
            Email = basedOn.Email;
            Fullname = basedOn.Fullname;
            Active = basedOn.Active;
            IsAdmin = basedOn.IsAdmin;
        }
    }

    public partial class Workspace
    {
        [System.Diagnostics.DebuggerStepThrough]
        public Workspace() { }

        public Workspace(Workspace basedOn)
        {
            Id = basedOn.Id;
            Name = basedOn.Name;
            Admin = basedOn.Admin;
            Premium = basedOn.Premium;
            BusinessWs = basedOn.BusinessWs;
            SuspendedAt = basedOn.SuspendedAt;
            ServerDeletedAt = basedOn.ServerDeletedAt;
            DefaultHourlyRate = basedOn.DefaultHourlyRate;
            DefaultCurrency = basedOn.DefaultCurrency;
            OnlyAdminsMayCreateProjects = basedOn.OnlyAdminsMayCreateProjects;
            OnlyAdminsSeeBillableRates = basedOn.OnlyAdminsSeeBillableRates;
            OnlyAdminsSeeTeamDashboard = basedOn.OnlyAdminsSeeTeamDashboard;
            ProjectsBillableByDefault = basedOn.ProjectsBillableByDefault;
            Rounding = basedOn.Rounding;
            RoundingMinutes = basedOn.RoundingMinutes;
            At = basedOn.At;
            LogoUrl = basedOn.LogoUrl;
            ICalUrl = basedOn.ICalUrl;
            ICalEnabled = basedOn.ICalEnabled;
        }
    }

    public partial class WorkspaceFeature
    {
        [System.Diagnostics.DebuggerStepThrough]
        public WorkspaceFeature() { }

        public WorkspaceFeature(WorkspaceFeature basedOn)
        {
            Name = basedOn.Name;
            FeatureId = basedOn.FeatureId;
            Enabled = basedOn.Enabled;
        }
    }

    public partial class WorkspaceFeatureCollection
    {
        [System.Diagnostics.DebuggerStepThrough]
        public WorkspaceFeatureCollection() { }

        public WorkspaceFeatureCollection(WorkspaceFeatureCollection basedOn)
        {
            WorkspaceId = basedOn.WorkspaceId;
            Features = basedOn.Features;
        }
    }
}

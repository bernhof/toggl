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

    public partial class Workspace
    {
        [System.Diagnostics.DebuggerStepThrough]
        public Workspace() { }

        public Workspace(Workspace basedOn)
        {
            Id = basedOn.Id;
            Name = basedOn.Name;
        }
    }

    public partial class WorkspaceUser
    {
        [System.Diagnostics.DebuggerStepThrough]
        public WorkspaceUser() { }

        public WorkspaceUser(WorkspaceUser basedOn)
        {
            Id = basedOn.Id;
            Email = basedOn.Email;
            Fullname = basedOn.Fullname;
            Admin = basedOn.Admin;
        }
    }
}
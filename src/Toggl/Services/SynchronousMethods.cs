using System;
using System.Collections.Generic;
using Toggl.Models;

namespace Toggl.Services
{
    public partial class ClientService
    {
        /// <summary>Synchronous (blocking) version of <see cref="CreateClientAsync"/>.</summary>
        public Client CreateClient(Client client)
            => CreateClientAsync(client).GetAwaiter().GetResult();
    }

    public partial class CurrentUserService
    {
        /// <summary>Synchronous (blocking) version of <see cref="GetTimeEntriesAsync"/>.</summary>
        public List<TimeEntry> GetTimeEntries(DateTimeOffset since)
            => GetTimeEntriesAsync(since).GetAwaiter().GetResult();
    }

    public partial class ProjectService
    {
        /// <summary>Synchronous (blocking) version of <see cref="GetProjectsAsync"/>.</summary>
        public PagedResult<Project> GetProjects(long workspaceId, ActiveState active, int page)
            => GetProjectsAsync(workspaceId, active, page).GetAwaiter().GetResult();
    
        /// <summary>Synchronous (blocking) version of <see cref="GetAllProjectsAsync"/>.</summary>
        public List<Project> GetAllProjects(long workspaceId, ActiveState active)
            => GetAllProjectsAsync(workspaceId, active).GetAwaiter().GetResult();
    
        /// <summary>Synchronous (blocking) version of <see cref="CreateProjectAsync"/>.</summary>
        public Project CreateProject(Project project)
            => CreateProjectAsync(project).GetAwaiter().GetResult();
    }

    public partial class TagService
    {
        /// <summary>Synchronous (blocking) version of <see cref="CreateTagAsync"/>.</summary>
        public Tag CreateTag(Tag tag)
            => CreateTagAsync(tag).GetAwaiter().GetResult();
    }

    public partial class TaskService
    {
        /// <summary>Synchronous (blocking) version of <see cref="CreateTaskAsync"/>.</summary>
        public Models.Task CreateTask(Models.Task task)
            => CreateTaskAsync(task).GetAwaiter().GetResult();
    
        /// <summary>Synchronous (blocking) version of <see cref="GetWorkspaceTasksAsync"/>.</summary>
        private Models.PagedResult<Models.Task> GetWorkspaceTasks(long workspaceId, int page)
            => GetWorkspaceTasksAsync(workspaceId, page).GetAwaiter().GetResult();
    
        /// <summary>Synchronous (blocking) version of <see cref="GetAllWorkspaceTasksAsync"/>.</summary>
        public List<Models.Task> GetAllWorkspaceTasks(long workspaceId)
            => GetAllWorkspaceTasksAsync(workspaceId).GetAwaiter().GetResult();
    }

    public partial class WorkspaceService
    {
        /// <summary>Synchronous (blocking) version of <see cref="GetTagsAsync"/>.</summary>
        public List<Tag> GetTags(long workspaceId)
            => GetTagsAsync(workspaceId).GetAwaiter().GetResult();
    
        /// <summary>Synchronous (blocking) version of <see cref="CreateTimeEntryAsync"/>.</summary>
        public TimeEntry CreateTimeEntry(TimeEntry entry)
            => CreateTimeEntryAsync(entry).GetAwaiter().GetResult();
    }
}
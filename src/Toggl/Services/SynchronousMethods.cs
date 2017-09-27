using System;
using System.Collections.Generic;
using Toggl.Models;

namespace Toggl.Services
{
    public partial class ClientService
    {
        /// <summary>Synchronous (blocking) version of <see cref="CreateAsync"/>.</summary>
        public Client Create(Client client)
            => CreateAsync(client).GetAwaiter().GetResult();
    }

    public partial class ProjectService
    {
        /// <summary>Synchronous (blocking) version of <see cref="ListAsync"/>.</summary>
        public PagedResult<Project> List(long workspaceId, ActiveState active, int page)
            => ListAsync(workspaceId, active, page).GetAwaiter().GetResult();
    
        /// <summary>Synchronous (blocking) version of <see cref="CreateAsync"/>.</summary>
        public Project Create(Project project)
            => CreateAsync(project).GetAwaiter().GetResult();
    }

    public partial class TagService
    {
        /// <summary>Synchronous (blocking) version of <see cref="ListAsync"/>.</summary>
        public List<Tag> List(long workspaceId)
            => ListAsync(workspaceId).GetAwaiter().GetResult();
    
        /// <summary>Synchronous (blocking) version of <see cref="CreateAsync"/>.</summary>
        public Tag Create(Tag tag)
            => CreateAsync(tag).GetAwaiter().GetResult();
    }

    public partial class TaskService
    {
        /// <summary>Synchronous (blocking) version of <see cref="CreateAsync"/>.</summary>
        public Models.Task Create(Models.Task task)
            => CreateAsync(task).GetAwaiter().GetResult();
    
        /// <summary>Synchronous (blocking) version of <see cref="ListAsync"/>.</summary>
        private Models.PagedResult<Models.Task> List(long workspaceId, int page)
            => ListAsync(workspaceId, page).GetAwaiter().GetResult();
    }


    public partial class WorkspaceService
    {
    }
}
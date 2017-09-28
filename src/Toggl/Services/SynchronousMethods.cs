using System;
using System.Collections.Generic;
using System.Threading;
using Toggl.Models;

namespace Toggl.Services
{
    public partial class ClientService
    {
        /// <summary>
        /// Synchronous (blocking) version of <see cref="CreateAsync"/>.
        /// Automatically generated and provided for convenience.
        /// It is highly recommended to use Async variant of method instead.
        /// </summary>
        public Client Create(Client client, CancellationToken cancellationToken = default(CancellationToken))
            => CreateAsync(client, cancellationToken).GetAwaiter().GetResult();
    }

    public partial class ProjectService
    {
        /// <summary>
        /// Synchronous (blocking) version of <see cref="ListAsync"/>.
        /// Automatically generated and provided for convenience.
        /// It is highly recommended to use Async variant of method instead.
        /// </summary>
        public PagedResult<Project> List(long workspaceId, ActiveState active, int page, CancellationToken cancellationToken = default(CancellationToken))
            => ListAsync(workspaceId, active, page, cancellationToken).GetAwaiter().GetResult();
    
        /// <summary>
        /// Synchronous (blocking) version of <see cref="CreateAsync"/>.
        /// Automatically generated and provided for convenience.
        /// It is highly recommended to use Async variant of method instead.
        /// </summary>
        public Project Create(Project project, CancellationToken cancellationToken = default(CancellationToken))
            => CreateAsync(project, cancellationToken).GetAwaiter().GetResult();
    
        /// <summary>
        /// Synchronous (blocking) version of <see cref="UpdateAsync"/>.
        /// Automatically generated and provided for convenience.
        /// It is highly recommended to use Async variant of method instead.
        /// </summary>
        public Project Update(Project current, Project before = null, CancellationToken cancellationToken = default(CancellationToken))
            => UpdateAsync(current, before, cancellationToken).GetAwaiter().GetResult();
    }

    public partial class TagService
    {
        /// <summary>
        /// Synchronous (blocking) version of <see cref="ListAsync"/>.
        /// Automatically generated and provided for convenience.
        /// It is highly recommended to use Async variant of method instead.
        /// </summary>
        public List<Tag> List(long workspaceId, CancellationToken cancellationToken = default(CancellationToken))
            => ListAsync(workspaceId, cancellationToken).GetAwaiter().GetResult();
    
        /// <summary>
        /// Synchronous (blocking) version of <see cref="CreateAsync"/>.
        /// Automatically generated and provided for convenience.
        /// It is highly recommended to use Async variant of method instead.
        /// </summary>
        public Tag Create(Tag tag, CancellationToken cancellationToken = default(CancellationToken))
            => CreateAsync(tag, cancellationToken).GetAwaiter().GetResult();
    }

    public partial class TaskService
    {
        /// <summary>
        /// Synchronous (blocking) version of <see cref="CreateAsync"/>.
        /// Automatically generated and provided for convenience.
        /// It is highly recommended to use Async variant of method instead.
        /// </summary>
        public Models.Task Create(Models.Task task, CancellationToken cancellationToken = default(CancellationToken))
            => CreateAsync(task, cancellationToken).GetAwaiter().GetResult();
    
        /// <summary>
        /// Synchronous (blocking) version of <see cref="ListAsync"/>.
        /// Automatically generated and provided for convenience.
        /// It is highly recommended to use Async variant of method instead.
        /// </summary>
        public Models.PagedResult<Models.Task> List(long workspaceId, int page, CancellationToken cancellationToken = default(CancellationToken))
            => ListAsync(workspaceId, page, cancellationToken).GetAwaiter().GetResult();
    }


    public partial class WorkspaceService
    {
        /// <summary>
        /// Synchronous (blocking) version of <see cref="ListMineAsync"/>.
        /// Automatically generated and provided for convenience.
        /// It is highly recommended to use Async variant of method instead.
        /// </summary>
        public List<Workspace> ListMine(CancellationToken cancellationToken = default(CancellationToken))
            => ListMineAsync(cancellationToken).GetAwaiter().GetResult();
    }
}
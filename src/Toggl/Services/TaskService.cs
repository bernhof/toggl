using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Toggl.Services
{
    /// <summary>
    /// Manages tasks
    /// </summary>
    public partial class TaskService
    {
        private readonly TogglClient _client;

        /// <summary>
        /// Creates a new <see cref="TaskService"/>
        /// </summary>
        /// <param name="client">Current <see cref="TogglClient"/></param>
        public TaskService(TogglClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Creates a new task
        /// </summary>
        /// <param name="task">A new task</param>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns>The new task as presented by server</returns>
        public async Task<Models.Task> CreateAsync(Models.Task task, CancellationToken cancellationToken = default(CancellationToken))
        {
            string uri = $"workspaces/{task.WorkspaceId}/projects/{task.ProjectId}/tasks";
            var result = await _client.Post(uri, cancellationToken, task);
            return result;
        }

        /// <summary>
        /// Lists all tasks in a workspace
        /// </summary>
        /// <param name="workspaceId">Workspace ID</param>
        /// <param name="page"></param>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns></returns>
        public async Task<Models.PagedResult<Models.Task>> ListAsync(long workspaceId, int page, CancellationToken cancellationToken = default(CancellationToken))
        {
            Utilities.CheckPageArgument(page);
            string uri = $"workspaces/{workspaceId}/tasks?active=both&page={page}";
            var result = await _client.Get<Models.PagedResult<Models.Task>>(uri, cancellationToken);
            return result;
        }
    }
}

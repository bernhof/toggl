using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Toggl.Services
{
    public partial class TaskService
    {
        private readonly TogglClient _client;

        public TaskService(TogglClient client)
        {
            _client = client;
        }

        public async Task<Models.Task> CreateAsync(Models.Task task)
        {
            string uri = $"workspaces/{task.WorkspaceId}/projects/{task.ProjectId}/tasks";
            var result = await _client.Post(uri, task);
            return result;
        }

        public async Task<Models.PagedResult<Models.Task>> ListAsync(long workspaceId, int page)
        {
            Utilities.CheckPageArgument(page);
            string uri = $"workspaces/{workspaceId}/tasks?active=both&page={page}";
            var result = await _client.Get<Models.PagedResult<Models.Task>>(uri);
            return result;
        }
    }
}

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

        public async Task<Models.Task> CreateTaskAsync(Models.Task task)
        {
            string uri = $"workspaces/{task.WorkspaceId}/projects/{task.ProjectId}/tasks";
            var result = await _client.Post(uri, task);
            return result;
        }

        private async Task<Models.PagedResult<Models.Task>> GetWorkspaceTasksAsync(long workspaceId, int page)
        {
            if (page < 1) throw new ArgumentException("Page index must be 1 or higher", nameof(page));
            string uri = $"workspaces/{workspaceId}/tasks?active=both&page={page}";
            var result = await _client.Get<Models.PagedResult<Models.Task>>(uri);
            return result;
        }

        public async Task<List<Models.Task>> GetAllWorkspaceTasksAsync(long workspaceId)
        {
            bool more;
            var tasks = new List<Models.Task>();
            int page = 0;

            // run through all "pages" of result set and store all results in a list.
            do
            {
                page++;
                Models.PagedResult<Models.Task> result = await GetWorkspaceTasksAsync(workspaceId, page);
                if (result.Items != null) tasks.AddRange(result.Items);
                // check if there are more items to retrieve
                more = result.TotalCount > result.Page * result.ItemsPerPage;

                // TODO: rate limiting hack...
                if (more) await Task.Delay(500);
            }
            while (more);

            return tasks;
        }

    }
}

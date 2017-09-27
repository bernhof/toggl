using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Toggl.Models;

namespace Toggl.Services
{
    public partial class ProjectService
    {
        private readonly TogglClient _client;

        public ProjectService(TogglClient client)
        {
            _client = client;
        }

        public async Task<PagedResult<Project>> GetProjectsAsync(long workspaceId, ActiveState active, int page)
        {
            if (page < 1) throw new ArgumentException("Page index must be 1 or higher", nameof(page));
            string uri = $"workspaces/{workspaceId}/projects?active={_client.GetActiveStateString(active)}&page={page}";
            var result = await _client.Get<PagedResult<Project>>(uri);
            return result;
        }

        // TODO: extract into generic method that can be used for all paged results? 
        public async Task<List<Project>> GetAllProjectsAsync(long workspaceId, ActiveState active)
        {
            bool more;
            var projects = new List<Project>();
            int page = 0;

            // run through all pages of result set and store all results in a list.
            do
            {
                page++;
                var result = await GetProjectsAsync(workspaceId, active, page);
                if (result.Items != null) projects.AddRange(result.Items);
                // check if there are more items to retrieve
                more = result.TotalCount > result.Page * result.ItemsPerPage;

                // TODO: rate limiting hack...
                if (more) await System.Threading.Tasks.Task.Delay(500);
            }
            while (more);

            return projects;
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            string uri = $"workspaces/{project.WorkspaceId}/projects";
            var result = await _client.Post(uri, project);
            return result;
        }

        public async Task<Project> UpdateProjectAsync(Project project, Project before = null)
        {
            bool hasChanges = true;
            object model;
            if (before != null)
                model = _client.CreateMinimalModelForUpdate(project, before, out hasChanges);
            else
                model = project;

            if (!hasChanges) return project;

            string uri = $"workspaces/{project.WorkspaceId}/projects/{project.Id}";
            var result = await _client.Put<Project>(uri, model);
            return result;
        }
    }
}

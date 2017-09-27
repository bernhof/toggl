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

        public async Task<PagedResult<Project>> ListAsync(long workspaceId, ActiveState active, int page)
        {
            if (page < 1) throw new ArgumentException("Page index must be 1 or higher", nameof(page));
            string uri = $"workspaces/{workspaceId}/projects?active={Utilities.GetActiveStateString(active)}&page={page}";
            var result = await _client.Get<PagedResult<Project>>(uri);
            return result;
        }

        public async Task<Project> CreateAsync(Project project)
        {
            string uri = $"workspaces/{project.WorkspaceId}/projects";
            var result = await _client.Post(uri, project);
            return result;
        }

        public async Task<Project> UpdateAsync(Project project, Project before = null)
        {
            object model = _client.GetMinimalModelForUpdate(project, before, out bool changed);
            if (!changed) return project;

            string uri = $"workspaces/{project.WorkspaceId}/projects/{project.Id}";
            var result = await _client.Put<Project>(uri, model);
            return result;
        }
    }
}

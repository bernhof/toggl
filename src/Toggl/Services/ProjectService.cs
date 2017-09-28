using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Toggl.Models;

namespace Toggl.Services
{
    /// <summary>
    /// Manages projects
    /// </summary>
    public partial class ProjectService
    {
        private readonly TogglClient _client;

        /// <summary>
        /// Creates a new <see cref="ProjectService"/>
        /// </summary>
        /// <param name="client">Current <see cref="TogglClient"/></param>
        public ProjectService(TogglClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Lists all projects in a workspace
        /// </summary>
        /// <param name="workspaceId">ID of workspace</param>
        /// <param name="active">Specifies whether active and/or archived projects are included in results</param>
        /// <param name="page">Page number to retrieve</param>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns>A <see cref="PagedResult{Project}"/> with specified page of results</returns>
        public async Task<PagedResult<Project>> ListAsync(long workspaceId, ActiveState active, int page, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (page < 1) throw new ArgumentException("Page index must be 1 or higher", nameof(page));
            string uri = $"workspaces/{workspaceId}/projects?active={Utilities.GetActiveStateString(active)}&page={page}";
            var result = await _client.Get<PagedResult<Project>>(uri, cancellationToken);
            return result;
        }

        /// <summary>
        /// Creates a new project
        /// </summary>
        /// <param name="project">A new project</param>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns>The new project as presented on server</returns>
        public async Task<Project> CreateAsync(Project project, CancellationToken cancellationToken = default(CancellationToken))
        {
            string uri = $"workspaces/{project.WorkspaceId}/projects";
            var result = await _client.Post(uri, cancellationToken, project);
            return result;
        }

        /// <summary>
        /// Updates an existing project
        /// </summary>
        /// <param name="current">Current project with changes</param>
        /// <param name="before">Copy of project before changes were made. If null, no bandwidth optimization is performed.</param>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns></returns>
        public async Task<Project> UpdateAsync(Project current, Project before = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            object model = _client.GetMinimalModelForUpdate(current, before, out bool changed);
            if (!changed) return current;

            string uri = $"workspaces/{current.WorkspaceId}/projects/{current.Id}";
            var result = await _client.Put<Project>(uri, cancellationToken, model);
            return result;
        }
    }
}

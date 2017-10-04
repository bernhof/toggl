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
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// Lists all projects in a workspace
        /// </summary>
        /// <param name="workspaceId">ID of workspace</param>
        /// <param name="active">Specifies whether active and/or archived projects are included in results. If null, uses server default.</param>
        /// <param name="page">Page number</param>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns>A <see cref="PagedResult{Project}"/> with specified page of results</returns>
        public async Task<PagedResult<Project>> ListAsync(long workspaceId, DateTimeOffset? since = null, BothBool? active = null, int page = 1, CancellationToken cancellationToken = default(CancellationToken))
        {
            Utilities.CheckPageArgument(page);
            string uri =
                $"workspaces/{workspaceId}/projects" +
                $"?active={active?.ToTrueFalseBoth()}" +
                $"&since={since?.ToUnixTimeSeconds()}" +
                $"&page={page}";

            var result = await _client.Get<PagedResult<Project>>(Apis.TogglV9, uri, cancellationToken);
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
            var result = await _client.Post(Apis.TogglV9, uri, cancellationToken, project);
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
            if (current == null) throw new ArgumentNullException(nameof(current));

            object model = Utilities.GetMinimalModelForUpdate(current, before, out bool changed);
            if (!changed) return current;

            string uri = $"workspaces/{current.WorkspaceId}/projects/{current.Id}";
            var result = await _client.Put<Project>(Apis.TogglV9, uri, cancellationToken, model);
            return result;
        }
    }
}

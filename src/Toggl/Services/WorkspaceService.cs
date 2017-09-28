using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Toggl;
using Toggl.Models;

namespace Toggl.Services
{
    /// <summary>
    /// Manages workspaces
    /// </summary>
    public partial class WorkspaceService
    {
        private readonly TogglClient _client;

        /// <summary>
        /// Creates a new <see cref="WorkspaceService"/>
        /// </summary>
        /// <param name="client">Current <see cref="TogglClient"/></param>
        public WorkspaceService(TogglClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Retrieves all workspaces that current user belongs to
        /// </summary>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns>A list of workspaces</returns>
        public async Task<List<Workspace>> ListMineAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Get<List<Workspace>>("me/workspaces", cancellationToken);
            return response;
        }
    }
}

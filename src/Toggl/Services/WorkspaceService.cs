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
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// Retrieves all workspaces that current user belongs to
        /// </summary>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns>A list of workspaces</returns>
        public Task<List<Workspace>> ListMineAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _client.Get<List<Workspace>>(Apis.TogglV9, "me/workspaces", cancellationToken);
        }

        /// <summary>
        /// Lists features for all workspaces that current user belongs to
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<WorkspaceFeatureCollection>> ListFeatures(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _client.Get<List<WorkspaceFeatureCollection>>(Apis.TogglV9, "me/features", cancellationToken);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Toggl.Models;

namespace Toggl.Services
{
    /// <summary>
    /// Manages the current user
    /// </summary>
    public class UserService
    {
        private readonly TogglClient _client;

        /// <summary>
        /// Creates a new <see cref="UserService"/>
        /// </summary>
        /// <param name="client">Current <see cref="TogglClient"/></param>
        public UserService(TogglClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// Gets information about the current user
        /// </summary>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns>An awaitable <see cref="Task{CurrentUser}"/></returns>
        public Task<CurrentUser> GetCurrentUserAsync(CancellationToken cancellationToken = default(CancellationToken))
            => _client.Get<CurrentUser>(Apis.TogglV9, "me", cancellationToken);

        /// <summary>
        /// Lists all users in a workspace
        /// </summary>
        /// <param name="workspaceId">Workspace ID</param>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns>An awaitable <see cref="Task{User}"/></returns>
        public Task<List<User>> ListAsync(long workspaceId, CancellationToken cancellationToken = default(CancellationToken))
            => _client.Get<List<User>>(Apis.TogglV9, $"workspaces/{workspaceId}/users", cancellationToken);
    }
}
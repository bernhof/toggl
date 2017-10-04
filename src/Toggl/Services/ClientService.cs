using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Toggl.Models;

namespace Toggl.Services
{
    /// <summary>
    /// Manages clients
    /// </summary>
    public partial class ClientService
    {
        private readonly TogglClient _client;

        /// <summary>
        /// Creates a new <see cref="ClientService"/>
        /// </summary>
        /// <param name="client">Current <see cref="TogglClient"/></param>
        public ClientService(TogglClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// Creates a new client
        /// </summary>
        /// <param name="client">A new client</param>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns></returns>
        public Task<Client> CreateAsync(Client client, CancellationToken cancellationToken = default(CancellationToken))
            => _client.Post(Apis.TogglV9, $"workspaces/{client.WorkspaceId}/clients", cancellationToken, client);

        /// <summary>
        /// Lists all clients in a workspace
        /// </summary>
        /// <param name="workspaceId">Workspace ID</param>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns>An awaitable <see cref="Task{T}"/> with all clients</returns>
        public Task<List<Client>> ListAsync(long workspaceId, CancellationToken cancellationToken = default(CancellationToken))
            => _client.Get<List<Client>>(Apis.TogglV8, $"workspaces/{workspaceId}/clients", cancellationToken);
    }
}

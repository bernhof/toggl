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
            _client = client;
        }

        /// <summary>
        /// Creates a new client
        /// </summary>
        /// <param name="client">A new client</param>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns></returns>
        public async Task<Client> CreateAsync(Client client, CancellationToken cancellationToken = default(CancellationToken))
        {
            string uri = $"workspaces/{client.WorkspaceId}/clients";
            var result = await _client.Post(uri, cancellationToken, client);
            return result;
        }
    }
}

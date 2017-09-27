using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Toggl.Models;

namespace Toggl.Services
{
    public partial class ClientService
    {
        private readonly TogglClient _client;

        public ClientService(TogglClient client)
        {
            _client = client;
        }

        public async Task<Client> CreateAsync(Client client)
        {
            string uri = $"workspaces/{client.WorkspaceId}/clients";
            var result = await _client.Post(uri, client);
            return result;
        }
    }
}

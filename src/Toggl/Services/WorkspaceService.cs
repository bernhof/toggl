using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Toggl;
using Toggl.Models;

namespace Toggl.Services
{
    public partial class WorkspaceService
    {
        private readonly TogglClient _client;

        public WorkspaceService(TogglClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Retrieves all workspaces that the current user has access to.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Workspace>> ListMineAsync()
        {
            var response = await _client.Get<List<Workspace>>("me/workspaces");
            return response;
        }
    }
}

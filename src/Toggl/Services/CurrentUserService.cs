using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Toggl;
using Toggl.Models;

namespace Toggl.Services
{
    public partial class CurrentUserService
    {
        const string BaseUrl = "https://www.toggl.com/api/v9/me/";

        private readonly TogglClient _client;

        public CurrentUserService(TogglClient client)
        {
            _client = client;
        }

        public async Task<List<Workspace>> GetWorkspacesAsync()
        {
            var response = await _client.Get<List<Workspace>>(BaseUrl + "workspaces");
            return response;
        }

        public async Task<List<TimeEntry>> GetTimeEntriesAsync(DateTimeOffset since)
        {
            // todo: implement since
            var response = await _client.Get<List<TimeEntry>>(BaseUrl + "time_entries");
            return response;
        }

    }
}

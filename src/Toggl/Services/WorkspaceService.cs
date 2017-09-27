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
        const string BaseUrl = "https://www.toggl.com/api/v9/workspace/";

        private readonly TogglClient _client;

        public WorkspaceService(TogglClient client)
        {
            _client = client;
        }

        public async Task<List<Tag>> GetTagsAsync(long workspaceId)
        {
            string uri = BaseUrl + $"workspaces/{workspaceId}/tags";
            var response = await _client.Get<List<Tag>>(uri);
            return response;
        }

        public async Task<TimeEntry> CreateTimeEntryAsync(TimeEntry entry)
        {
            string uri = BaseUrl + $"workspaces/{entry.WorkspaceId}/time_entries";
            var result = await _client.Post(uri, entry);
            return result;
        }

        public async Task<TimeEntry> UpdateTimeEntryAsync(TimeEntry entry, TimeEntry before = null)
        {
            bool hasChanges = true;
            object model = (before != null)
                ? _client.CreateMinimalModelForUpdate(entry, before, out hasChanges, new[] { nameof(TimeEntry.CreatedWith) })
                : entry;

            if (!hasChanges) return entry;

            string uri = $"workspaces/{entry.WorkspaceId}/time_entries/{entry.Id}";
            var result = await _client.Put<TimeEntry>(uri, model);
            return result;
        }

    }
}

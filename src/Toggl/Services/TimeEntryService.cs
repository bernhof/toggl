using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Toggl.Models;

namespace Toggl.Services
{
    public class TimeEntryService
    {
        private readonly TogglClient _client;

        public TimeEntryService(TogglClient client)
        {
            _client = client;
        }

        public async Task<List<TimeEntry>> GetMineAsync(DateTimeOffset since)
        {
            // todo: implement since
            var response = await _client.Get<List<TimeEntry>>("me/time_entries");
            return response;
        }

        public async Task<TimeEntry> CreateAsync(TimeEntry entry)
        {
            string uri = $"workspaces/{entry.WorkspaceId}/time_entries";
            var result = await _client.Post(uri, entry);
            return result;
        }

        public async Task<TimeEntry> UpdateAsync(TimeEntry entry, TimeEntry before = null)
        {
            object model = _client.GetMinimalModelForUpdate(entry, before, out bool changed, new[] { nameof(TimeEntry.CreatedWith) });
            if (!changed) return entry;

            string uri = $"workspaces/{entry.WorkspaceId}/time_entries/{entry.Id}";
            var result = await _client.Put<TimeEntry>(uri, model);
            return result;
        }
    }
}

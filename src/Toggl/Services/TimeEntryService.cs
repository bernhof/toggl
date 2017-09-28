using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Toggl.Models;

namespace Toggl.Services
{
    /// <summary>
    /// Manages time entries
    /// </summary>
    public class TimeEntryService
    {
        private readonly TogglClient _client;

        /// <summary>
        /// Creates a new <see cref="TimeEntryService"/>
        /// </summary>
        /// <param name="client">Current <see cref="TogglClient"/></param>
        public TimeEntryService(TogglClient client)
        {
            _client = client;
        }

        /// <summary>
        /// List all time entries created by current user across all accessible workspaces
        /// </summary>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns></returns>
        public async Task<List<TimeEntry>> ListMineAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // todo: implement since?
            var response = await _client.Get<List<TimeEntry>>("me/time_entries", cancellationToken);
            return response;
        }

        /// <summary>
        /// Creates a new time entry
        /// </summary>
        /// <param name="entry">A new time entry</param>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns>The new time entry as presented by server</returns>
        public async Task<TimeEntry> CreateAsync(TimeEntry entry, CancellationToken cancellationToken = default(CancellationToken))
        {
            string uri = $"workspaces/{entry.WorkspaceId}/time_entries";
            var result = await _client.Post(uri, cancellationToken, entry);
            return result;
        }

        /// <summary>
        /// Updates an existing time entry
        /// </summary>
        /// <param name="current">Time entry with changes</param>
        /// <param name="previous">Copy if time entry before changes were made. Can be null.</param>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns></returns>
        public async Task<TimeEntry> UpdateAsync(TimeEntry current, TimeEntry previous = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            object model = _client.GetMinimalModelForUpdate(current, previous, out bool changed, new[] { nameof(TimeEntry.CreatedWith) });
            if (!changed) return current;

            string uri = $"workspaces/{current.WorkspaceId}/time_entries/{current.Id}";
            var result = await _client.Put<TimeEntry>(uri, cancellationToken, model);
            return result;
        }
    }
}

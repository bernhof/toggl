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
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// List all time entries created by current user across all accessible workspaces
        /// </summary>
        /// <param name="since">Include time entries only on or after this date and time. If null, uses server default.</param>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns></returns>
        public Task<List<TimeEntry>> ListMineAsync(DateTimeOffset? since = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            string uri = $"me/time_entries?since={since?.ToUnixTimeSeconds()}";
            return _client.Get<List<TimeEntry>>(uri, cancellationToken);
        }

        /// <summary>
        /// Creates a new time entry
        /// </summary>
        /// <param name="entry">A new time entry</param>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns>The new time entry as presented by server</returns>
        public Task<TimeEntry> CreateAsync(TimeEntry entry, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));

            string uri = $"workspaces/{entry.WorkspaceId}/time_entries";
            return _client.Post(uri, cancellationToken, entry);
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
            if (current == null) throw new ArgumentNullException(nameof(current));

            object model = Utilities.GetMinimalModelForUpdate(current, previous, out bool changed, new[] { nameof(TimeEntry.CreatedWith) });
            if (!changed) return current;

            string uri = $"workspaces/{current.WorkspaceId}/time_entries/{current.Id}";
            var result = await _client.Put<TimeEntry>(uri, cancellationToken, model);
            return result;
        }
    }
}

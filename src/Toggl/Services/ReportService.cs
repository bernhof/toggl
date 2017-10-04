using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Toggl.Models;
using Toggl.Models.Reports;

namespace Toggl.Services
{
    /// <summary>
    /// Provides access to Toggl reports
    /// </summary>
    public partial class ReportService
    {
        private readonly TogglClient _client;

        /// <summary>
        /// Creates a new <see cref="ReportService"/>
        /// </summary>
        /// <param name="client">Current <see cref="TogglClient"/></param>
        public ReportService(TogglClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        private void CheckUserAgent()
        {
            if (_client.UserAgent == null || string.IsNullOrEmpty(_client.UserAgent.ToString()))
                throw new InvalidOperationException("User agent must be specified when retrieving reports");
        }

        /// <summary>
        /// Retrieves a list of time entries in a workspace
        /// </summary>
        /// <param name="workspaceId">Workspace ID</param>
        /// <param name="options">Query options</param>
        /// <param name="page">Page number</param>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns>An awaitable <see cref="Task{T}"/></returns>
        public Task<PagedReportResult<DetailedReportItem>> DetailsAsync(
            long workspaceId,
            DetailedReportOptions options = null,
            long page = 1,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckUserAgent();
            if (options == null) options = new DetailedReportOptions();
            string optionsQuery = options.ToString();
            if (!string.IsNullOrEmpty(optionsQuery)) optionsQuery = "&" + optionsQuery;

            string uri = $"details?workspace_id={workspaceId}" +
                $"&user_agent={System.Net.WebUtility.UrlEncode(_client.UserAgent.ToString())}" +
                $"{optionsQuery}&page={page}";

            return _client.Get<PagedReportResult<DetailedReportItem>>(Apis.ReportsV2, uri, cancellationToken);
        }

    }
}

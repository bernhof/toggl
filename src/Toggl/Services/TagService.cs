using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Toggl.Models;

namespace Toggl.Services
{
    /// <summary>
    /// Manages tags
    /// </summary>
    public partial class TagService
    {
        private readonly TogglClient _client;

        /// <summary>
        /// Creates a new <see cref="TagService"/>
        /// </summary>
        /// <param name="client">Current <see cref="TogglClient"/></param>
        public TagService(TogglClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// Lists all tags in a workspace
        /// </summary>
        /// <param name="workspaceId">Workspace ID</param>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns>A list of tags in the workspace</returns>
        public Task<List<Tag>> ListAsync(long workspaceId, CancellationToken cancellationToken = default(CancellationToken))
        {
            string uri = $"workspaces/{workspaceId}/tags";
            return _client.Get<List<Tag>>(uri, cancellationToken);
        }

        /// <summary>
        /// Creates a new tag
        /// </summary>
        /// <param name="tag">A new tag</param>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns>The new tag as presented by server</returns>
        public async Task<Tag> CreateAsync(Tag tag, CancellationToken cancellationToken = default(CancellationToken))
        {
            string uri = $"workspaces/{tag.WorkspaceId}/tags";
            var result = await _client.Post(uri, cancellationToken, tag);
            return result;
        }
    }
}

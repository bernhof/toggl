using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Toggl.Models;

namespace Toggl.Services
{
    public partial class TagService
    {
        private readonly TogglClient _client;

        public TagService(TogglClient client)
        {
            _client = client;
        }

        public async Task<Tag> CreateTagAsync(Tag tag)
        {
            string uri = $"workspaces/{tag.WorkspaceId}/tags";
            var result = await _client.Post(uri, tag);
            return result;
        }

    }
}

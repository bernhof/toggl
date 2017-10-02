using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Toggl.Models;

namespace Toggl.Services
{
    /// <summary>
    /// Manages the current user
    /// </summary>
    public class UserService
    {
        private readonly TogglClient _client;

        /// <summary>
        /// Creates a new <see cref="UserService"/>
        /// </summary>
        /// <param name="client">Current <see cref="TogglClient"/></param>
        public UserService(TogglClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// Gets information about the current user
        /// </summary>
        /// <param name="cancellationToken">Token to observe</param>
        /// <returns>An awaitable <see cref="Task{User}"/></returns>
        public Task<User> GetCurrentUser(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _client.Get<User>("me", cancellationToken);
        }
    }
}

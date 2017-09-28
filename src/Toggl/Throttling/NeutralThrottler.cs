using System;
using System.Threading;
using System.Threading.Tasks;

namespace Toggl.Throttling
{
    /// <summary>
    /// Performs no throttling
    /// </summary>
    internal class NeutralThrottler : IThrottler
    {
        public Task ThrottleAsync(Func<Task> func, CancellationToken cancellationToken) => func();

        public Task<T> ThrottleAsync<T>(Func<Task<T>> func, CancellationToken cancellationToken) => func();
    }
}
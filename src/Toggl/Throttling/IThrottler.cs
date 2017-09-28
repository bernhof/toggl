using System;
using System.Threading;
using System.Threading.Tasks;

namespace Toggl.Throttling
{
    /// <summary>
    /// Represents a mechanism for throttling execution of delegates
    /// </summary>
    public interface IThrottler
    {
        /// <summary>
        /// Invokes the specified asynchronous delegate function while complying with throttling specifications
        /// </summary>
        /// <param name="func">An asynchronous delegate</param>
        /// <param name="cancellationToken">Cancellation token to observe while waiting to invoke delegate</param>
        /// <returns>An awaitable <see cref="Task"/></returns>
        Task ThrottleAsync(Func<Task> func, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Invokes the specified asynchronous delegate function while complying with throttling specifications
        /// </summary>
        /// <typeparam name="T">Return value of delegate asynchronous function</typeparam>
        /// <param name="func">An asynchronous delegate</param>
        /// <param name="cancellationToken">Cancellation token to observe while waiting to invoke delegate</param>
        /// <returns>An awaitable <see cref="Task"/> with return value of type <typeparamref name="T"/></returns>
        Task<T> ThrottleAsync<T>(Func<Task<T>> func, CancellationToken cancellationToken = default(CancellationToken));
    }
}
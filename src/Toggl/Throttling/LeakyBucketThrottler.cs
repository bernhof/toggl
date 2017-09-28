using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Toggl.Throttling
{
    /// <summary>
    /// Throttles requests to comply with leaky bucket rate limiting. Thread-safe.
    /// </summary>
    public class LeakyBucketThrottler : IThrottler
    {
        private readonly SemaphoreSlim _waitLock = new SemaphoreSlim(1, 1);
        private readonly object _logLock = new object();

        private readonly int _maxRequests;
        private readonly TimeSpan _interval;
        private readonly ConcurrentQueue<DateTimeOffset> _log;

        /// <summary>
        /// Creates a new <see cref="LeakyBucketThrottler"/>
        /// </summary>
        /// <param name="interval">Interval</param>
        /// <param name="maxRequests">Max. no. of requests that are allowed within the specified <paramref name="interval"/></param>
        public LeakyBucketThrottler(TimeSpan interval, int maxRequests)
        {
            if (maxRequests < 1) throw new ArgumentException("Maximum number of requests must be 1 or higher", nameof(maxRequests));
            if (interval < TimeSpan.Zero) throw new ArgumentException("Interval must be a non-negative time span", nameof(interval));

            _log = new ConcurrentQueue<DateTimeOffset>();
            _interval = interval;
            _maxRequests = maxRequests;
        }

        /// <summary>
        /// Waits an amount of time required to comply with throttling specification before invoking specified delegate
        /// </summary>
        /// <param name="func">An asynchronous delegate</param>
        /// <param name="cancellationToken">Cancellation token to observe while waiting to invoke delegate</param>
        /// <returns>An awaitable <see cref="Task"/></returns>
        public async Task ThrottleAsync(Func<Task> func, CancellationToken cancellationToken = default(CancellationToken))
        {
            await WaitAndLog(cancellationToken);
            await func();
        }

        /// <summary>
        /// Waits an amount of time required to comply with throttling specification before invoking specified delegate
        /// </summary>
        /// <typeparam name="T">Type of return value from delegate</typeparam>
        /// <param name="func">An asynchronous delegate</param>
        /// <param name="cancellationToken">Cancellation token to observe while waiting to invoke delegate</param>
        /// <returns>An awaitable <see cref="Task"/> with return value of type <typeparamref name="T"/></returns>
        public async Task<T> ThrottleAsync<T>(Func<Task<T>> func, CancellationToken cancellationToken = default(CancellationToken))
        {
            await WaitAndLog(cancellationToken);
            var result = await func();
            return result;
        }

        /// <summary>
        /// Waits until next request can be performed and logs the request.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to observe while waiting</param>
        /// <returns></returns>
        protected internal async Task WaitAndLog(CancellationToken cancellationToken)
        {
            await _waitLock.WaitAsync(cancellationToken);
            try
            {
                // Only a single thread at a time will get a fixed wait interval.
                // Others will wait to enter the semaphore, ie. wait in line to recevie 
                await Task.Delay(NextDelay(DateTimeOffset.Now), cancellationToken);
                Log(DateTimeOffset.Now);
            }
            finally
            {
                _waitLock.Release();
            }
        }

        /// <summary>
        /// Calculates the time to wait before allowing another request
        /// </summary>
        /// <param name="nextRequestTime">
        /// Used to calculate how much time has passed since the first logged request until next request.
        /// </param>
        /// <returns>A <see cref="TimeSpan"/> specifying the time to wait before allowing another request</returns>
        protected internal virtual TimeSpan NextDelay(DateTimeOffset nextRequestTime)
        {
            int requestCount = _log.Count;
            if (requestCount >= _maxRequests && _log.TryPeek(out var firstRequest))
            {
                // We are at max. no. of requests currently.
                // How long has passed since the first logged request?
                var timeSinceFirstRequest = nextRequestTime.Subtract(firstRequest);
                if (timeSinceFirstRequest < _interval)
                {
                    // Interval has not yet passed, calculate time until interval has passed:
                    var delay = _interval.Subtract(timeSinceFirstRequest);
                    return delay;
                }
            }
            // we haven't reached max no. of requests yet, or sufficient time has passed to allow another request.
            return TimeSpan.Zero;
        }

        /// <summary>
        /// Logs that a request was performed at the specified time and trims logs entries that are beyond
        /// </summary>
        /// <param name="time">Time of request</param>
        /// <remarks>
        /// Assumes that logged request complies with throttling specifications since.
        /// </remarks>
        protected internal virtual void Log(DateTimeOffset time)
        {
            lock (_logLock) // dequeue/enqueue operation must be atomic
            {
                while (_log.Count >= _maxRequests)
                    _log.TryDequeue(out var _); // if we're at max capacity, remove earliest request from log
                _log.Enqueue(time); // log current request
            }
        }
    }
}

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
        private readonly long _bucketCapacity;
        private readonly TimeSpan _leakInterval;

        private DateTimeOffset _lastLeak;
        private long _currentCount;

        /// <summary>
        /// Delegate that retrieves current time
        /// </summary>
        /// <remarks>Included for unit testing purposes</remarks>
        protected virtual Func<DateTimeOffset> GetCurrentTime { get; set; } = () => DateTimeOffset.Now;

        /// <summary>
        /// Creates a new <see cref="LeakyBucketThrottler"/>
        /// </summary>
        /// <param name="bucketCapacity">Bucket capacity, ie. initial maximum number of requests allowed</param>
        /// <param name="leakInterval">Interval at which the bucket leaks</param>
        public LeakyBucketThrottler(int bucketCapacity, TimeSpan leakInterval)
        {
            if (bucketCapacity < 1) throw new ArgumentException("Bucket capacity must be at least 1", nameof(bucketCapacity));
            if (leakInterval <= TimeSpan.Zero) throw new ArgumentException("Leak interval must be a positive time span", nameof(leakInterval));

            _leakInterval = leakInterval;
            _bucketCapacity = bucketCapacity;
            _lastLeak = default(DateTimeOffset);
        }

        /// <summary>
        /// Waits an amount of time required to comply with throttling specification before invoking specified delegate
        /// </summary>
        /// <param name="func">An asynchronous delegate</param>
        /// <param name="cancellationToken">Cancellation token to observe while waiting to invoke delegate</param>
        /// <returns>An awaitable <see cref="Task"/></returns>
        public virtual async Task ThrottleAsync(Func<Task> func, CancellationToken cancellationToken = default(CancellationToken))
        {
            await WaitForEntryAndLogRequest(cancellationToken);
            await func();
        }

        /// <summary>
        /// Waits an amount of time required to comply with throttling specification before invoking specified delegate
        /// </summary>
        /// <typeparam name="T">Type of return value from delegate</typeparam>
        /// <param name="func">An asynchronous delegate</param>
        /// <param name="cancellationToken">Cancellation token to observe while waiting to invoke delegate</param>
        /// <returns>An awaitable <see cref="Task"/> with return value of type <typeparamref name="T"/></returns>
        public virtual async Task<T> ThrottleAsync<T>(Func<Task<T>> func, CancellationToken cancellationToken = default(CancellationToken))
        {
            await WaitForEntryAndLogRequest(cancellationToken);
            var result = await func();
            return result;
        }

        /// <summary>
        /// Waits until next request can be performed and logs the request.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to observe while waiting</param>
        /// <returns></returns>
        /// <remarks>
        /// Only a single thread at a time will get a fixed wait interval in order to guarantee its validity.
        /// Other threads simply wait to enter the semaphore. As per SemaphoreSlim documentation,
        /// there is no guaranteed order in which threads are allowed to enter the semaphore.
        /// https://docs.microsoft.com/en-us/dotnet/standard/threading/semaphore-and-semaphoreslim
        /// </remarks>
        protected async Task WaitForEntryAndLogRequest(CancellationToken cancellationToken)
        {
            await _waitLock.WaitAsync(cancellationToken);
            try
            {
                Leak();
                var delay = NextDelay();
                await WaitInternal(delay, cancellationToken);
                _currentCount++;
            }
            finally
            {
                _waitLock.Release();
            }
        }

        /// <summary>
        /// Lowers the current request count if sufficient time has passed
        /// </summary>
        protected virtual void Leak()
        {
            var time = GetCurrentTime();

            // determine how many leaks have occurred since bucket was last leaked:
            long leakedCount = (long)Math.Floor((double)time.Subtract(_lastLeak).Ticks / _leakInterval.Ticks);
            _lastLeak = _lastLeak.AddTicks(_leakInterval.Ticks * leakedCount);
            // update current count (don't go below zero)
            _currentCount = (leakedCount <= _currentCount) ? _currentCount - leakedCount : 0;
        }

        /// <summary>
        /// Waits an amount of time
        /// </summary>
        /// <param name="delay">Time to wait</param>
        /// <param name="cancellationToken">Token to observe while waiting</param>
        /// <returns>An awaitable <see cref="Task"/> that completes when the specified delay duration has elapsed</returns>
        protected virtual async Task WaitInternal(TimeSpan delay, CancellationToken cancellationToken)
        {
            await Task.Delay(delay, cancellationToken);
        }

        /// <summary>
        /// Calculates the time to wait before bucket leaks next
        /// </summary>
        /// <returns>A <see cref="TimeSpan"/> specifying the time to wait before bucket leaks</returns>
        protected virtual TimeSpan NextDelay()
        {
            var time = GetCurrentTime();
            if (_currentCount == _bucketCapacity)
            {
                // We are at full capacity currently, so next leak must occur in the future.
                // Determine how long it'll take before bucket leaks next:
                var delay = _lastLeak.Add(_leakInterval).Subtract(time);
                return delay;
            }
            // bucket isn't full
            return TimeSpan.Zero;
        }
    }
}

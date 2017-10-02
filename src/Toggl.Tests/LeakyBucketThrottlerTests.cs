using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Toggl.Throttling;

namespace Toggl.Tests
{
    public class TestLeakyBucketThrottler : LeakyBucketThrottler
    {
        public TestLeakyBucketThrottler(TimeSpan interval, int maxRequests) : base(maxRequests, interval)
        {
            GetCurrentTime = () => Time;
        }

        /// <summary>
        /// Seconds passed since starting point on Jan 1, 2017 12 AM (00:00:00)
        /// </summary>
        public double Seconds { get => Milliseconds * 1000; set => Milliseconds = value * 1000; }
        public double Milliseconds { get; set; } = 0;
        public DateTimeOffset Time => new DateTimeOffset(2017, 1, 1, 0, 0, 0, TimeSpan.Zero).AddMilliseconds(Milliseconds);
        public TimeSpan LastDelay { get; private set; }
        protected override Task WaitInternal(TimeSpan delay, CancellationToken cancellationToken)
        {
            this.LastDelay = delay;
            this.Milliseconds += delay.TotalMilliseconds;
            return Task.CompletedTask;
        }
        public async Task ThrottleAsync_AddToInvocationList(List<double> listOfInvocations)
        {
            await ThrottleAsync(() =>
            {
                listOfInvocations.Add(Milliseconds);
                return Task.CompletedTask;
            });
        }

        public async Task ThrottleAsync_Noop()
        {
            await ThrottleAsync(() =>
            {
                return Task.CompletedTask;
            });
        }
    }

    [TestClass]
    public class LeakyBucketThrottlerTests
    {
        [TestMethod]
        public async Task Next_delay_is_zero_when_bucket_is_not_full()
        {
            var sut = new TestLeakyBucketThrottler(TimeSpan.MaxValue, 1);
            await sut.ThrottleAsync_Noop();

            Assert.AreEqual(TimeSpan.Zero, sut.LastDelay);
        }

        [TestMethod]
        public async Task Next_delay_is_non_zero_when_bucket_is_full()
        {
            var interval = TimeSpan.FromMilliseconds(60);

            var sut = new TestLeakyBucketThrottler(interval, 1);
            await sut.ThrottleAsync_Noop();
            sut.Milliseconds += 50;
            await sut.ThrottleAsync_Noop();

            Assert.AreEqual(TimeSpan.FromMilliseconds(10), sut.LastDelay);
        }

        [TestMethod]
        public void Negative_interval_not_allowed()
        {
            var ex = Assert.ThrowsException<ArgumentException>(() => new LeakyBucketThrottler(1, TimeSpan.FromMinutes(-1)));
            Assert.AreEqual(ex.ParamName, "leakInterval");
        }

        [TestMethod]
        public void Non_positive_max_requests_not_allowed()
        {
            var ex = Assert.ThrowsException<ArgumentException>(() => new LeakyBucketThrottler(0, TimeSpan.FromMinutes(1)));
            Assert.AreEqual(ex.ParamName, "bucketCapacity");
        }

        [TestMethod]
        public async Task Bucket_capacity_of_1_distributes_calls_evenly()
        {
            // When max 1 req per interval, a request is allowed through every interval
            // This ensures that it's possible to throttle evenly as long as max requets = 1 
            var now = DateTimeOffset.Now;
            var interval = TimeSpan.FromMilliseconds(50);
            var sut = new TestLeakyBucketThrottler(interval, 1);
            var invocations = new List<double>();
            var startTime = sut.Time;

            // attempt multiple invocations immediately after each other
            foreach (var _ in Enumerable.Range(1, 5))
                await sut.ThrottleAsync_AddToInvocationList(invocations);

            // check that calls are spread out evenly across intervals.
            // first invocation is immediate, then we wait 50 ms between each call.
            CollectionAssert.AreEqual(
                new double[] { 0, 50, 100, 150, 200 },
                invocations);
        }

        [TestMethod]
        public async Task Bucket_capacity_fills_quickly_then_slowly_leaks()
        {
            var interval = TimeSpan.FromMilliseconds(50);
            var sut = new TestLeakyBucketThrottler(interval, 2);
            var invocations = new List<double>();
            var startTime = sut.Time;

            // attempt multiple invocations immediately after each other
            foreach (var _ in Enumerable.Range(1, 5))
                await sut.ThrottleAsync_AddToInvocationList(invocations);

            // first two invocations fill bucket (immediately), then requests are allowed through every leak interval:
            CollectionAssert.AreEqual(
                new double[] { 0, 0, 50, 100, 150 },
                invocations);
        }


        [TestMethod]
        public async Task Long_pause_leaks_multiple_times()
        {
            var interval = TimeSpan.FromMilliseconds(50);
            var sut = new TestLeakyBucketThrottler(interval, 3);
            var invocations = new List<double>();
            var startTime = sut.Time;

            await sut.ThrottleAsync_AddToInvocationList(invocations);
            await sut.ThrottleAsync_AddToInvocationList(invocations);
            await sut.ThrottleAsync_AddToInvocationList(invocations); // bucket is now full
            sut.Milliseconds += 1000; // more time than it takes to empty the bucket passes by.
            await sut.ThrottleAsync_AddToInvocationList(invocations);
            await sut.ThrottleAsync_AddToInvocationList(invocations);
            await sut.ThrottleAsync_AddToInvocationList(invocations); // again, bucket is now full

            // verify that across all 6 invocations, no delays were incurred, because bucket was emptied before the last 3
            CollectionAssert.AreEqual(
                new double[] { 0, 0, 0, 1000, 1000, 1000 },
                invocations);
        }
    }
}

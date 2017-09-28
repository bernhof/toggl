using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using Toggl.Throttling;

namespace Toggl.Tests
{
    [TestClass]
    public class LeakyBucketThrottlerTests
    {
        // Construct CTS that protects against timeout issues in tests (automatically cancels after 100 ms)
        private CancellationTokenSource TimeoutCancellationTokenSource() => new CancellationTokenSource(100);

        [TestMethod]
        public void Next_delay_is_zero_when_not_at_max_capacity()
        {
            var sut = new LeakyBucketThrottler(TimeSpan.MaxValue, 1);
            var delay = sut.NextDelay(DateTimeOffset.Now);
            Assert.AreEqual(TimeSpan.Zero, delay);
        }

        [TestMethod]
        public void Next_delay_is_zero_when_interval_has_passed()
        {
            var now = DateTimeOffset.Now;
            var sut = new LeakyBucketThrottler(TimeSpan.FromMinutes(1), 1);
            sut.Log(now);

            var delayExact = sut.NextDelay(now.AddMinutes(1));
            var delayLater = sut.NextDelay(now.AddMinutes(2));
            Assert.AreEqual(TimeSpan.Zero, delayExact);
            Assert.AreEqual(TimeSpan.Zero, delayLater);
        }
        
        [TestMethod]
        public void Next_delay_when_at_max_capacity()
        {
            var now = DateTimeOffset.Now;
            var interval = TimeSpan.FromSeconds(60);
            var nextRequestAt = now.AddSeconds(50);

            var sut = new LeakyBucketThrottler(interval, 1);
            sut.Log(now); // now at max capacity (1)
            var delay = sut.NextDelay(nextRequestAt);

            var expected = TimeSpan.FromSeconds(10);
            Assert.AreEqual(expected, delay);
        }

        [TestMethod]
        public void Negative_interval_not_allowed()
        {
            var ex = Assert.ThrowsException<ArgumentException>(() => new LeakyBucketThrottler(TimeSpan.FromSeconds(-1), 1));
            Assert.AreEqual(ex.ParamName, "interval");
        }

        [TestMethod]
        public void Non_positive_max_requests_not_allowed()
        {
            var ex = Assert.ThrowsException<ArgumentException>(() => new LeakyBucketThrottler(TimeSpan.FromSeconds(1), 0));
            Assert.AreEqual(ex.ParamName, "maxRequests");
        }
        
        [TestMethod]
        public void Zero_interval_performs_no_throttling()
        {
            var now = DateTimeOffset.Now;
            var sut = new LeakyBucketThrottler(TimeSpan.Zero, 1);
            sut.Log(now); // now at max capacity (1)
            var delay = sut.NextDelay(now); // no delay should be incurred b/c interval is zero

            Assert.AreEqual(TimeSpan.Zero, delay);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toggl.Services;

namespace Toggl
{
    /// <summary>
    /// Provides different utility methods for working with Toggl
    /// </summary>
    public static class Utilities
    {
        private static readonly Random _defaultRandom = new Random();
        
        /// <summary>
        /// Gets a random color in HTML/CSS-style hexadecimal format, e.g. &quot;#0033ff&quot;.
        /// Useful for generating random colors for projects in Toggl.
        /// </summary>
        /// <param name="random"><see cref="Random"/> instance to use when generating random color</param>
        /// <returns>A string representing a color in HTML/CSS-style hexadecimal format.</returns>
        public static string GetRandomColor(Random random = null)
        {
            random = random ?? _defaultRandom;
            var color = $"#{random.Next(0x1000000 + 1):x6}";
            return color;
        }

        internal static void CheckPageArgument(int page)
        {
            if (page < 1) throw new ArgumentException("Page number must be 1 or higher", nameof(page));
        }

        internal static string GetActiveStateString(ActiveState active)
        {
            switch (active)
            {
                case ActiveState.Active: return "active";
                case ActiveState.Archived: return "archived";
                case ActiveState.Both: return "both";
                default: throw new ArgumentException("Invalid Active specification", nameof(active));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toggl
{
    public static class Utilities
    {
        private static readonly Random _defaultRandom = new Random();
        
        public static string GetRandomColor(Random random = null)
        {
            random = random ?? _defaultRandom;
            var color = $"#{random.Next(0x1000000 + 1):x6}";
            return color;
        }
    }
}

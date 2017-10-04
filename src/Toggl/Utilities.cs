using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        /// Compares current item and a snapshot of its previous state to supply the most minimal payload (model) when performing updates
        /// where only changed fields are necessary to include in request.
        /// </summary>
        /// <typeparam name="T">Type of item being updated</typeparam>
        /// <param name="current">Item with current property values</param>
        /// <param name="previous">A copy of the item with property values before changes were made</param>
        /// <param name="changed">Returns a value indicating whether any changes were found on item based on comparison</param>
        /// <param name="skipProperties">Specifies properties of <typeparamref name="T"/> that should be ignored completely</param>
        /// <returns>
        /// If <paramref name="previous"/> is not null, returns a <see cref="JObject"/> that contains the payload necessary to perform the update.
        /// Otherwise, this method simply returns <paramref name="current"/>.
        /// </returns>
        /// <remarks>
        /// Properties marked with <see cref="JsonIgnoreAttribute"/> are ignored.
        /// Property names are extracted from any <see cref="JsonPropertyAttribute"/>s, if present. Otherwise uses actual property names in model.
        /// </remarks>
        internal static object GetMinimalModelForUpdate<T>(T current, T previous, out bool changed, string[] skipProperties = null)
            where T : Models.IEntity
        {
            if (current == null) throw new ArgumentNullException(nameof(current));
            if (previous == null)
            {
                // No previous state to compare to, so cannot create minimal model. Instead return full model.
                changed = true; // Assume there are changes to save, since we can't tell.
                return current;
            }

            var properties = typeof(T).GetTypeInfo().DeclaredProperties;
            var model = new JObject();
            changed = false;

            // always include ID
            model.Add("id", current.Id);

            foreach (var prop in properties)
            {
                if (prop.Name == nameof(Models.IEntity.Id)) continue; // skip Id (it's added above)
                var attributes = prop.GetCustomAttributes();
                if (attributes.OfType<JsonIgnoreAttribute>().Any()) continue; // don't check or serialize ignored attributes
                if (skipProperties != null && skipProperties.Any(p => p == prop.Name)) continue; // skip specified properties

                // See if property value was changed by comparing current value to previous
                var value = prop.GetValue(current);
                if (!Equals(prop.GetValue(previous), value))
                {
                    // Property value has changed, and should be included in payload.
                    var attr = attributes.OfType<JsonPropertyAttribute>().FirstOrDefault();
                    // Use the correct property name; that is, if JsonPropertyAttribute is present, use that name.
                    // Otherwise fall back to property name.
                    string propertyName = (attr != null && !string.IsNullOrEmpty(attr.PropertyName))
                        ? attr.PropertyName
                        : prop.Name;

                    // Add property to model
                    model.Add(propertyName, JToken.FromObject(value));
                    changed = true;
                }
            }
            return model;
        }


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

        internal static string ToTrueFalseBoth(this BothBool value)
        {
            switch (value)
            {
                case BothBool.False: return "false";
                case BothBool.True: return "true";
                case BothBool.Both: return "both";
                default: throw new InvalidOperationException("Invalid value");
            }
        }

        internal static string ToYesNoBoth(this BothBool value)
        {
            switch (value)
            {
                case BothBool.False: return "no";
                case BothBool.True: return "yes";
                case BothBool.Both: return "both";
                default: throw new InvalidOperationException("Invalid value");
            }
        }

        internal static long ToUnixTimeSeconds(this DateTimeOffset current)
        {
            // Constant and implementation taken from .NET reference source:
            // https://referencesource.microsoft.com/#mscorlib/system/datetimeoffset.cs

            const long unixEpochSeconds = 62_135_596_800L;

            var seconds = current.UtcDateTime.Ticks / TimeSpan.TicksPerSecond;
            return seconds - unixEpochSeconds;
        }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Toggl.Models
{
    /// <summary>
    /// Describes features in a workspace
    /// </summary>
    public partial class WorkspaceFeatureCollection
    {
        /// <summary>
        /// Workspace ID
        /// </summary>
        [JsonProperty("workspace_id")]
        public long WorkspaceId { get; set; }

        /// <summary>
        /// Feature list
        /// </summary>
        [JsonProperty("features")]
        public List<WorkspaceFeature> Features { get; set; }

        /// <summary>
        /// Determines whether a feature is enabled in this workspace
        /// </summary>
        /// <param name="feature">Feature to check</param>
        /// <returns></returns>
        public bool IsEnabled(WorkspaceFeatureId feature)
            => Features.Any(f => f.FeatureId == feature && f.Enabled);
    }
}

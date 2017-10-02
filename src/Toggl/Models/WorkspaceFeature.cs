using System;
using System.Collections.Generic;
using System.Text;

namespace Toggl.Models
{
    /// <summary>
    /// Describes a workspace feature
    /// </summary>
    public partial class WorkspaceFeature
    {
        /// <summary>
        /// Feature name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Feature ID
        /// </summary>
        public WorkspaceFeatureId FeatureId { get; set; }

        /// <summary>
        /// Determines whether feature is enabled
        /// </summary>
        public bool Enabled { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toggl.Models
{
    /// <summary>
    /// Represents an entity with an ID
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// ID of entity
        /// </summary>
        long Id { get; set; }
    }
}

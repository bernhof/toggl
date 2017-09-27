using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toggl.Models
{
    public interface IBaseModel
    {
        long Id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Data
{
    public enum RelativeSourceMode
    {
        PreviousData,
        TemplatedParent,
        Self,
        FindAncestor
    }

    public class RelativeSource
    {
        public RelativeSource()
        {
        }

        public RelativeSource(RelativeSourceMode mode)
        {
            this.Mode = mode;
        }

        public RelativeSourceMode Mode { get; set; }
    }
}

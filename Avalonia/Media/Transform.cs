using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Media
{
    public abstract class Transform
    {
        public abstract Matrix Value { get; }
    }
}

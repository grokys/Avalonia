using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia
{
    public struct CornerRadius
    {
        public CornerRadius(double uniformRadius)
            : this()
        {
            this.BottomLeft = this.BottomRight = this.TopLeft = this.TopRight = uniformRadius;
        }

        public double BottomLeft { get; set; }
        public double BottomRight { get; set; }
        public double TopLeft { get; set; }
        public double TopRight { get; set; }
    }
}

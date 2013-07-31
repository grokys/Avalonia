using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Media
{
    public class PointHitTestParameters : HitTestParameters
    {
        public PointHitTestParameters(Point point)
        {
            this.HitPoint = point;
        }

        public Point HitPoint { get; private set; }
    }
}

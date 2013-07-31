using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Media
{
    public class PointHitTestResult : HitTestResult
    {
        public PointHitTestResult(Visual visualHit, Point pointHit)
        {
            this.VisualHit = visualHit;
            this.PointHit = pointHit;
        }

        public Point PointHit { get; private set; }
    }
}

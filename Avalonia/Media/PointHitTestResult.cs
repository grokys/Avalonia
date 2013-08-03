// -----------------------------------------------------------------------
// <copyright file="PointHitTestResult.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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

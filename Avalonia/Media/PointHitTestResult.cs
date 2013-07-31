// -----------------------------------------------------------------------
// <copyright file="PointHitTestResult.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
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
        /// <summary>
        /// Initializes a new instance of the <see cref="PointHitTestResult"/> class.
        /// </summary>
        public PointHitTestResult(Visual visualHit, Point pointHit)
        {
            this.VisualHit = visualHit;
            this.PointHit = pointHit;
        }

        public Point PointHit { get; private set; }
    }
}

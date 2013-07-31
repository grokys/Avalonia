// -----------------------------------------------------------------------
// <copyright file="PointHitTestParameters.cs" company="Steven Kirk">
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

    public class PointHitTestParameters : HitTestParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PointHitTestParameters"/> class.
        /// </summary>
        public PointHitTestParameters(Point point)
        {
            this.HitPoint = point;
        }

        public Point HitPoint { get; private set; }
    }
}

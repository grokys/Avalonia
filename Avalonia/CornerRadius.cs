// -----------------------------------------------------------------------
// <copyright file="CornerRadius.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [TypeConverter(typeof(CornerRadiusConverter))]
    public struct CornerRadius
    {
        public CornerRadius(double uniformRadius)
            : this()
        {
            this.BottomLeft = this.BottomRight = this.TopLeft = this.TopRight = uniformRadius;
        }

        public CornerRadius(double topLeft, double topRight, double bottomRight, double bottomLeft)
                    : this()
        {
            this.TopLeft = topLeft;
            this.TopRight = topRight;
            this.BottomLeft = bottomLeft;
            this.BottomRight = bottomRight;
        }

        public double BottomLeft { get; set; }

        public double BottomRight { get; set; }

        public double TopLeft { get; set; }

        public double TopRight { get; set; }
    }
}

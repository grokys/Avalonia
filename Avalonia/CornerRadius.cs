// -----------------------------------------------------------------------
// <copyright file="CornerRadius.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public struct CornerRadius
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CornerRadius"/> struct.
        /// </summary>
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

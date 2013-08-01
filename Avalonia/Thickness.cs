// -----------------------------------------------------------------------
// <copyright file="Thickness.cs" company="Steven Kirk">
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

    [TypeConverter(typeof(ThicknessConverter))]
    public struct Thickness
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness"/> struct.
        /// </summary>
        public Thickness(double uniformLength)
            : this()
        {
            this.Left = this.Top = this.Right = this.Bottom = uniformLength;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness"/> struct.
        /// </summary>
        public Thickness(double left, double top, double right, double bottom)
                    : this()
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        public double Left { get; set; }

        public double Top { get; set; }

        public double Right { get; set; }

        public double Bottom { get; set; }
    }
}

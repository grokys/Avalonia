// -----------------------------------------------------------------------
// <copyright file="SolidColorBrush.cs" company="Steven Kirk">
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

    public class SolidColorBrush : Brush
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SolidColorBrush"/> class.
        /// </summary>
        public SolidColorBrush()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolidColorBrush"/> class.
        /// </summary>
        public SolidColorBrush(Color color)
        {
            this.Color = color;
        }

        public Color Color { get; set; }
    }
}

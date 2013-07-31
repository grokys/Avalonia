// -----------------------------------------------------------------------
// <copyright file="Visual.cs" company="Steven Kirk">
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

    public class Visual : DependencyObject
    {
        protected internal virtual int VisualChildrenCount
        {
            get { return 0; }
        }

        protected internal Transform VisualTransform { get; protected set; }

        protected internal virtual Visual GetVisualChild(int index)
        {
            throw new ArgumentOutOfRangeException();
        }
    }
}

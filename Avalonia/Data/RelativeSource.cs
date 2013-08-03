// -----------------------------------------------------------------------
// <copyright file="RelativeSource.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public enum RelativeSourceMode
    {
        PreviousData,
        TemplatedParent,
        Self,
        FindAncestor
    }

    public class RelativeSource
    {
        public RelativeSource()
        {
        }

        public RelativeSource(RelativeSourceMode mode)
        {
            this.Mode = mode;
        }

        public RelativeSourceMode Mode { get; set; }
    }
}

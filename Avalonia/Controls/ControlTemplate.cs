// -----------------------------------------------------------------------
// <copyright file="ControlTemplate.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Avalonia.Media;

    public class ControlTemplate : FrameworkTemplate
    {
        [AvaloniaSpecific]
        public virtual Visual CreateVisualTree(DependencyObject parent)
        {
            throw new NotImplementedException();
        }
    }
}

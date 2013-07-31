// -----------------------------------------------------------------------
// <copyright file="Brush.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Avalonia.Animation;

    [TypeConverter(typeof(BrushConverter))]
    public class Brush : Animatable
    {
    }
}

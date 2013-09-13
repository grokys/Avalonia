// -----------------------------------------------------------------------
// <copyright file="Geometry.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    using System.ComponentModel;
    using Avalonia.Animation;

    [TypeConverter(typeof(GeometryConverter))]
    public abstract class Geometry : Animatable
    {
    }
}

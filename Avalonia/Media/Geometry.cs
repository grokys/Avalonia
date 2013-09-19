// -----------------------------------------------------------------------
// <copyright file="Geometry.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    using System.ComponentModel;
    using Avalonia.Animation;
    using Avalonia.Platform;

    public enum ToleranceType
    {
        Absolute,
        Relative,
    }

    [TypeConverter(typeof(GeometryConverter))]
    public abstract class Geometry : Animatable
    {
        public static double StandardFlatteningTolerance 
        {
            get { return 0.25; }
        }

        public virtual Rect Bounds 
        {
            get { return new Rect(); }
        }

        public IPlatformStreamGeometry PlatformImpl
        {
            get;
            protected set;
        }

        public Rect GetRenderBounds(Pen pen)
        {
            return this.GetRenderBounds(pen, StandardFlatteningTolerance, ToleranceType.Absolute);
        }

        public virtual Rect GetRenderBounds(Pen pen, double tolerance, ToleranceType type)
        {
            return this.PlatformImpl.GetRenderBounds(pen, tolerance, type);
        }
    }
}

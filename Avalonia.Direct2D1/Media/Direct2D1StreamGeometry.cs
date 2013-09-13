// -----------------------------------------------------------------------
// <copyright file="Direct2D1StreamGeometry.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Direct2D1.Media
{
    using Avalonia.Media;
    using Avalonia.Platform;
    using SharpDX.Direct2D1;

    public class Direct2D1StreamGeometry : IPlatformStreamGeometry
    {
        public Direct2D1StreamGeometry(PathGeometry impl)
        {
            this.Direct2DGeometry = impl;
        }

        public PathGeometry Direct2DGeometry
        {
            get;
            private set;
        }

        public StreamGeometryContext Open()
        {
            return new Direct2D1StreamGeometryContext(this.Direct2DGeometry.Open());
        }
    }
}

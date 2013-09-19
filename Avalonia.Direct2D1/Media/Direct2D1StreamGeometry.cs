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

        public Rect Bounds
        {
            get { return this.Direct2DGeometry.GetBounds().ToAvalonia(); }
        }

        public PathGeometry Direct2DGeometry
        {
            get;
            private set;
        }

        public void Dispose()
        {
            this.Direct2DGeometry.Dispose();
        }

        public Rect GetRenderBounds(Pen pen, double tolerance, ToleranceType type)
        {
            float strokeWidth = (pen != null) ? (float)pen.Thickness : 0f;
            return this.Direct2DGeometry.GetWidenedBounds(strokeWidth, (float)tolerance).ToAvalonia();
        }

        public StreamGeometryContext Open()
        {
            return new Direct2D1StreamGeometryContext(this.Direct2DGeometry.Open());
        }
    }
}

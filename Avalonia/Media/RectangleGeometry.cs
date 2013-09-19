// -----------------------------------------------------------------------
// <copyright file="RectangleGeometry.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    using System;
    using Avalonia.Platform;

    public sealed class RectangleGeometry : Geometry
    {
        public static readonly DependencyProperty RectProperty =
            DependencyProperty.Register(
                "Rect",
                typeof(Rect),
                typeof(RectangleGeometry),
                new PropertyMetadata(RectChanged));

        public RectangleGeometry()
        {
            this.PlatformImpl = this.CreatePlatformImpl();
        }

        public RectangleGeometry(Rect rect)
        {
            this.Rect = rect;
            this.PlatformImpl = this.CreatePlatformImpl();
        }

        public override Rect Bounds
        {
            get { return this.PlatformImpl.Bounds; }
        }

        public Rect Rect
        {
            get { return (Rect)this.GetValue(RectProperty); }
            set { this.SetValue(RectProperty, value); }
        }

        private static void RectChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            RectangleGeometry r = (RectangleGeometry)sender;

            if (r.PlatformImpl != null)
            {
                r.PlatformImpl.Dispose();
                r.PlatformImpl = r.CreatePlatformImpl();
            }
        }

        private IPlatformStreamGeometry CreatePlatformImpl()
        {
            IPlatformStreamGeometry result = PlatformInterface.Instance.CreateStreamGeometry();

            using (StreamGeometryContext context = result.Open())
            {
                context.BeginFigure(new Point(this.Rect.Left, this.Rect.Top), true, true);
                context.LineTo(new Point(this.Rect.Right, this.Rect.Top), true, false);
                context.LineTo(new Point(this.Rect.Right, this.Rect.Bottom), true, false);
                context.LineTo(new Point(this.Rect.Left, this.Rect.Bottom), true, false);
            }

            return result;
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="Direct2D1DrawingContext.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Direct2D1.Media
{
    using System;
    using System.Collections.Generic;
    using Avalonia.Direct2D1.Media.Imaging;
    using Avalonia.Media;
    using Avalonia.Media.Imaging;
    using Avalonia.Platform;
    using SharpDX;
    using SharpDX.Direct2D1;
    using Brush = Avalonia.Media.Brush;
    using Geometry = Avalonia.Media.Geometry;
    using SolidColorBrush = Avalonia.Media.SolidColorBrush;

    public class Direct2D1DrawingContext : DrawingContext
    {
        private Factory factory;

        private RenderTarget target;

        private Stack<object> stack;

        public Direct2D1DrawingContext(Factory factory, RenderTarget target)
        {
            this.factory = factory;
            this.target = target;
            this.target.BeginDraw();
            this.stack = new Stack<object>();
        }

        public override void Dispose()
        {
            this.target.EndDraw();
        }

        public override void DrawGeometry(Brush brush, Pen pen, Geometry geometry)
        {
            StreamGeometry streamGeometry = (StreamGeometry)geometry;
            Direct2D1StreamGeometry platformGeometry = (Direct2D1StreamGeometry)streamGeometry.PlatformImpl;
            PathGeometry d2dGeometry = platformGeometry.Direct2DGeometry;

            if (brush != null)
            {
                this.target.FillGeometry(d2dGeometry, brush.ToSharpDX(this.target));
            }

            if (pen != null)
            {
                this.target.DrawGeometry(
                    d2dGeometry,
                    pen.Brush.ToSharpDX(this.target),
                    (float)pen.Thickness);
            }
        }

        public override void DrawGeometry(Brush brush, Pen pen, Geometry geometry, Avalonia.Media.Matrix transform)
        {
            Direct2D1StreamGeometry platformGeometry = (Direct2D1StreamGeometry)geometry.PlatformImpl;

            using (TransformedGeometry d2dGeometry = new TransformedGeometry(
                this.factory,
                platformGeometry.Direct2DGeometry,
                transform.ToSharpDX()))
            {
                if (brush != null)
                {
                    this.target.FillGeometry(d2dGeometry, brush.ToSharpDX(this.target));
                }

                if (pen != null)
                {
                    this.target.DrawGeometry(
                        d2dGeometry,
                        pen.Brush.ToSharpDX(this.target),
                        (float)pen.Thickness);
                }
            }
        }

        public override void DrawImage(ImageSource imageSource, Rect rectangle)
        {
            BitmapSource bitmapSource = imageSource as BitmapSource;

            if (bitmapSource == null)
            {
                throw new NotSupportedException("Cannot draw ImageSource that is not a BitmapSource.");
            }

            WicBitmapSource wic = (WicBitmapSource)bitmapSource.PlatformImpl;
            Bitmap bitmap = wic.GetDirect2DBitmap(this.target);
            this.target.DrawBitmap(bitmap, rectangle.ToSharpDX(), 1, BitmapInterpolationMode.Linear);
        }

        public override void DrawImage(ImageSource imageSource, double opacity, Rect sourceRectangle, Rect destinationRectangle)
        {
            BitmapSource bitmapSource = imageSource as BitmapSource;

            if (bitmapSource == null)
            {
                throw new NotSupportedException("Cannot draw ImageSource that is not a BitmapSource.");
            }

            WicBitmapSource wic = (WicBitmapSource)bitmapSource.PlatformImpl;
            Bitmap bitmap = wic.GetDirect2DBitmap(this.target);

            this.target.DrawBitmap(
                bitmap,
                destinationRectangle.ToSharpDX(),
                (float)opacity,
                BitmapInterpolationMode.Linear,
                sourceRectangle.ToSharpDX());
        }

        public override void DrawLine(Pen pen, Point point0, Point point1)
        {
            this.target.DrawLine(
                point0.ToSharpDX(),
                point1.ToSharpDX(),
                pen.Brush.ToSharpDX(this.target));
        }

        public override void DrawRectangle(Brush brush, Pen pen, Rect rectangle)
        {
            Rect brushRect = rectangle;
            Rect penRect = rectangle;

            if (pen != null)
            {
                double penOffset = -(pen.Thickness / 2);
                brushRect.Inflate(-pen.Thickness, -pen.Thickness);
                penRect.Inflate(penOffset, penOffset);
            }

            if (brush != null)
            {
                using (var brush2D = brush.ToSharpDX(this.target))
                {
                    this.target.FillRectangle(
                        brushRect.ToSharpDX(),
                        brush2D);
                }
            }

            if (pen != null)
            {
                using (var brush2D = pen.Brush.ToSharpDX(this.target))
                {
                    this.target.DrawRectangle(
                        penRect.ToSharpDX(),
                        brush2D,
                        (float)pen.Thickness);
                }
            }
        }

        public override void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, double radiusX, double radiusY)
        {
            RoundedRectangle roundedRect = new RoundedRectangle
            {
                Rect = rectangle.ToSharpDX(),
                RadiusX = (float)radiusX,
                RadiusY = (float)radiusY,
            };

            if (brush != null)
            {
                using (var brush2D = brush.ToSharpDX(this.target))
                {
                    this.target.FillRoundedRectangle(roundedRect, brush2D);
                }
            }

            if (pen != null)
            {
                using (var brush2D = pen.Brush.ToSharpDX(this.target))
                {
                    this.target.DrawRoundedRectangle(roundedRect, brush2D, (float)pen.Thickness);
                }
            }
        }

        public override void DrawText(FormattedText formattedText, Point origin)
        {
            this.target.DrawText(
                formattedText.Text ?? string.Empty,
                ((Direct2D1FormattedText)formattedText.PlatformImpl).DirectWriteTextLayout,
                new RectangleF((float)origin.X, (float)origin.Y, float.PositiveInfinity, float.PositiveInfinity),
                formattedText.Foreground.ToSharpDX(this.target),
                DrawTextOptions.None);
        }

        public override void PushOpacity(double opacity)
        {
            Layer layer = new Layer(this.target);
            LayerParameters p = new LayerParameters();
            p.Opacity = (float)opacity;
            this.target.PushLayer(ref p, layer);
            this.stack.Push(layer);
        }

        public override void PushTransform(Transform transform)
        {
            Matrix3x2 m = transform.Value.ToSharpDX();
            this.target.Transform = this.target.Transform * m;
            this.stack.Push(m);
        }

        public override void Pop()
        {
            object top = this.stack.Pop();
            Matrix3x2? transform = top as Matrix3x2?;
            Layer layer = top as Layer;

            if (transform != null)
            {
                this.target.Transform = this.target.Transform * Invert(transform.Value);
            }
            else if (layer != null)
            {
                this.target.PopLayer();
            }
        }

        // This should be added to SharpDX.
        private static Matrix3x2 Invert(Matrix3x2 value)
        {
            Matrix3x2 result = Matrix3x2.Identity;

            float determinant = value.Determinant();

            if (determinant == 0.0f)
            {
                return value;
            }

            float invdet = 1.0f / determinant;
            float offsetX = value.M31;
            float offsetY = value.M32;

            return new Matrix3x2(
                value.M22 * invdet,
                -value.M12 * invdet,
                -value.M21 * invdet,
                value.M11 * invdet,
                ((value.M21 * offsetY) - (offsetX * value.M22)) * invdet,
                ((offsetX * value.M12) - (value.M11 * offsetY)) * invdet);
        }
    }
}

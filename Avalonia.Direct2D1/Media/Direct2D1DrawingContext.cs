// -----------------------------------------------------------------------
// <copyright file="Direct2D1DrawingContext.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Direct2D1.Media
{
    using System.Collections.Generic;
    using Avalonia.Media;
    using SharpDX;
    using SharpDX.Direct2D1;
    using Brush = Avalonia.Media.Brush;
    using SolidColorBrush = Avalonia.Media.SolidColorBrush;

    internal class Direct2D1DrawingContext : DrawingContext
    {
        private RenderTarget target;
        private Stack<Matrix3x2> transforms;

        public Direct2D1DrawingContext(RenderTarget target)
        {
            this.target = target;
            this.target.BeginDraw();
            this.transforms = new Stack<Matrix3x2>();
        }

        public override void Dispose()
        {
            this.target.EndDraw();
        }

        public override void DrawRectangle(Brush brush, Pen pen, Rect rectangle)
        {
            if (brush != null)
            {
                using (var brush2D = brush.ToSharpDX(this.target))
                {
                    this.target.FillRectangle(
                        this.CreateRectangleF(rectangle),
                        brush2D);
                }
            }

            if (pen != null)
            {
                using (var brush2D = pen.Brush.ToSharpDX(this.target))
                {
                    this.target.DrawRectangle(
                        this.CreateRectangleF(rectangle),
                        brush2D,
                        (float)pen.Thickness);
                }
            }
        }

        public override void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, double radiusX, double radiusY)
        {
            RoundedRectangle roundedRect = new RoundedRectangle
            {
                Rect = this.CreateRectangleF(rectangle),
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
                formattedText.Text,
                ((Direct2D1FormattedText)formattedText.PlatformImpl).DirectWriteTextLayout,
                new RectangleF(0, 0, 1000, 1000),
                new SolidColorBrush(Colors.Black).ToSharpDX(this.target),
                DrawTextOptions.None);
        }

        public override void PushTransform(Transform transform)
        {
            Matrix3x2 m = transform.Value.ToSharpDX();
            this.target.Transform = this.target.Transform * m;
            this.transforms.Push(m);
        }

        public override void Pop()
        {
            Matrix3x2 top = this.transforms.Pop();           
            this.target.Transform = this.target.Transform * Invert(top);
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

        private RectangleF CreateRectangleF(Rect rect)
        {
            return new RectangleF(
                (float)rect.Left,
                (float)rect.Top,
                (float)rect.Width,
                (float)rect.Height);
        }
    }
}

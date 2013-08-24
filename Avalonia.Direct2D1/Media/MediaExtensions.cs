// -----------------------------------------------------------------------
// <copyright file="MediaExtensions.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Direct2D1.Media
{
    using System;
    using Avalonia.Media;
    using Brush2D = SharpDX.Direct2D1.Brush;
    using Color4 = SharpDX.Color4;
    using Matrix3x2 = SharpDX.Matrix3x2;
    using RenderTarget2D = SharpDX.Direct2D1.RenderTarget;
    using SolidColorBrush2D = SharpDX.Direct2D1.SolidColorBrush;

    public static class MediaExtensions
    {
        public static Color4 ToSharpDX(this Color color)
        {
            return new Color4(
                (float)(color.R / 255.0),
                (float)(color.G / 255.0),
                (float)(color.B / 255.0),
                (float)(color.A / 255.0));
        }

        public static Matrix3x2 ToSharpDX(this Matrix matrix)
        {
            return new Matrix3x2(
                (float)matrix.M11,
                (float)matrix.M12,
                (float)matrix.M21,
                (float)matrix.M22,
                (float)matrix.OffsetX,
                (float)matrix.OffsetY);
        }

        public static Matrix FromSharpDX(this Matrix3x2 matrix)
        {
            return new Matrix(
                (float)matrix.M11,
                (float)matrix.M12,
                (float)matrix.M21,
                (float)matrix.M22,
                (float)matrix.M31,
                (float)matrix.M32);
        }

        public static Brush2D ToSharpDX(this Brush brush, RenderTarget2D target)
        {
            SolidColorBrush solidColorBrush = brush as SolidColorBrush;

            if (solidColorBrush != null)
            {
                return solidColorBrush.ToSharpDX(target);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public static SolidColorBrush2D ToSharpDX(this SolidColorBrush brush, RenderTarget2D target)
        {
            return new SharpDX.Direct2D1.SolidColorBrush(target, brush.Color.ToSharpDX());
        }
    }
}

namespace Avalonia.Direct2D1.Media
{
    using System;
    using Avalonia.Media;
    using Color4 = SharpDX.Color4;
    using Matrix3x2 = SharpDX.Matrix3x2;
    using Brush2D = SharpDX.Direct2D1.Brush;
    using RenderTarget2D = SharpDX.Direct2D1.RenderTarget;
    using RoundedRectangle2D = SharpDX.Direct2D1.RoundedRectangle;
    using SolidColorBrush2D = SharpDX.Direct2D1.SolidColorBrush;

    public static class MediaExtensions
    {
        public static Color4 ToSharpDX(this Color color)
        {
            return new Color4(
                (float)color.R,
                (float)color.G,
                (float)color.B,
                (float)color.A);
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

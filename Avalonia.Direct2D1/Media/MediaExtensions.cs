// -----------------------------------------------------------------------
// <copyright file="MediaExtensions.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Direct2D1.Media
{
    using System;
    using System.Collections.Generic;
    using Avalonia.Media;
    using Avalonia.Utils;
    using Brush2D = SharpDX.Direct2D1.Brush;
    using Color4 = SharpDX.Color4;
    using Matrix3x2 = SharpDX.Matrix3x2;
    using RectangleF = SharpDX.RectangleF;
    using RenderTarget2D = SharpDX.Direct2D1.RenderTarget;
    using SolidColorBrush2D = SharpDX.Direct2D1.SolidColorBrush;
    using Vector2 = SharpDX.Vector2;
    using WicPixelFormat = SharpDX.WIC.PixelFormat;

    public static class MediaExtensions
    {
        private static readonly DoubleKeyedDictionary<Guid, PixelFormat> FormatGuids =
            new DoubleKeyedDictionary<Guid, PixelFormat>
            {
                { WicPixelFormat.Format32bppBGR101010, PixelFormats.Bgr101010 },
                { WicPixelFormat.Format24bppBGR, PixelFormats.Bgr24 },
                { WicPixelFormat.Format32bppBGR, PixelFormats.Bgr32 },
                { WicPixelFormat.Format16bppBGR555, PixelFormats.Bgr555 },
                { WicPixelFormat.Format16bppBGR565, PixelFormats.Bgr565 },
                { WicPixelFormat.Format32bppBGRA, PixelFormats.Bgra32 },
                { WicPixelFormat.FormatBlackWhite, PixelFormats.BlackWhite },
                { WicPixelFormat.Format32bppCMYK, PixelFormats.Cmyk32 },
                { WicPixelFormat.Format16bppGray, PixelFormats.Gray16 },
                { WicPixelFormat.Format2bppGray, PixelFormats.Gray2 },
                { WicPixelFormat.Format32bppGrayFloat, PixelFormats.Gray32Float },
                { WicPixelFormat.Format4bppGray, PixelFormats.Gray4 },
                { WicPixelFormat.Format8bppGray, PixelFormats.Gray8 },
                { WicPixelFormat.Format1bppIndexed, PixelFormats.Indexed1 },
                { WicPixelFormat.Format2bppIndexed, PixelFormats.Indexed2 },
                { WicPixelFormat.Format4bppIndexed, PixelFormats.Indexed4 },
                { WicPixelFormat.Format8bppIndexed, PixelFormats.Indexed8 },
                { WicPixelFormat.Format32bppPBGRA, PixelFormats.Pbgra32 },
                { WicPixelFormat.Format128bppPRGBAFloat, PixelFormats.Prgba128Float },
                { WicPixelFormat.Format64bppPRGBA, PixelFormats.Prgba64 },
                { WicPixelFormat.Format128bppRGBFloat, PixelFormats.Rgb128Float },
                { WicPixelFormat.Format24bppRGB, PixelFormats.Rgb24 },
                { WicPixelFormat.Format48bppRGB, PixelFormats.Rgb48 },
                { WicPixelFormat.Format128bppRGBAFloat, PixelFormats.Rgba128Float },
                { WicPixelFormat.Format64bppRGBA, PixelFormats.Rgba64 },
            };

        public static Color4 ToSharpDX(this Color color)
        {
            return new Color4(
                (float)(color.R / 255.0),
                (float)(color.G / 255.0),
                (float)(color.B / 255.0),
                (float)(color.A / 255.0));
        }

        public static Vector2 ToSharpDX(this Point point)
        {
            return new Vector2((float)point.X, (float)point.Y);
        }

        public static PixelFormat ToAvalonia(this Guid pixelFormat)
        {
            return FormatGuids[pixelFormat];
        }

        public static Guid ToSharpDX(this PixelFormat pixelFormat)
        {
            return FormatGuids[pixelFormat];
        }

        public static Rect ToAvalonia(this RectangleF rect)
        {
            return new Rect(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        public static RectangleF ToSharpDX(this Rect rect)
        {
            return new RectangleF(
                (float)rect.Left,
                (float)rect.Top,
                (float)rect.Right,
                (float)rect.Bottom);
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

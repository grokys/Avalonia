// -----------------------------------------------------------------------
// <copyright file="Direct2D1RenderTargetBitmap.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Direct2D1.Media.Imaging
{
    using System;
    using Avalonia.Direct2D1.Media;
    using Avalonia.Media;
    using Avalonia.Platform;
    using SharpDX.Direct2D1;
    using SharpDX.WIC;
    using WicBitmap = SharpDX.WIC.Bitmap;

    public class Direct2D1RenderTargetBitmap : WicBitmapSource, IPlatformRenderTargetBitmap
    {
        private WicRenderTarget target;

        public Direct2D1RenderTargetBitmap(
            Factory d2dFactory, 
            ImagingFactory wicFactory,
            SharpDX.WIC.Bitmap bitmap)
            : base(wicFactory, bitmap)
        {
            double dpiX;
            double dpiY;

            bitmap.GetResolution(out dpiX, out dpiY);

            this.target = new WicRenderTarget(
                d2dFactory,
                bitmap,
                new RenderTargetProperties
                {
                    DpiX = (float)dpiX,
                    DpiY = (float)dpiY,
                });
        }

        public DrawingContext CreateDrawingContext()
        {
            return new Direct2D1DrawingContext(this.target);
        }
    }
}

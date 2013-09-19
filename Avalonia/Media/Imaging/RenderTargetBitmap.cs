// -----------------------------------------------------------------------
// <copyright file="RenderTargetBitmap.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media.Imaging
{
    using Avalonia.Platform;

    public sealed class RenderTargetBitmap : BitmapSource
    {
        public RenderTargetBitmap(
            int pixelWidth, 
            int pixelHeight, 
            double dpiX, 
            double dpiY, 
            PixelFormat pixelFormat)
            : base(PlatformInterface.Instance.CreateRenderTargetBitmap(
                pixelWidth,
                pixelHeight,
                dpiX,
                dpiY,
                pixelFormat))
        {
        }

        public void Render(Visual visual)
        {
            IPlatformRenderTargetBitmap impl = (IPlatformRenderTargetBitmap)this.PlatformImpl;
            FrameworkElement fe = visual as FrameworkElement;

            if (fe != null && !fe.IsInitialized)
            {
                fe.IsInitialized = true;
                fe.ApplyTemplate();
            }

            using (DrawingContext context = impl.CreateDrawingContext())
            {
                Renderer renderer = new Renderer();
                renderer.Render(context, visual);
            }
        }
    }
}

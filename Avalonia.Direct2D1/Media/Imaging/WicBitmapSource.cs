// -----------------------------------------------------------------------
// <copyright file="WicBitmapSource.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Direct2D1.Media.Imaging
{
    using Avalonia.Platform;
    using SharpDX.WIC;

    public class WicBitmapSource : IPlatformBitmapSource
    {
        private ImagingFactory factory;
        
        private BitmapSource wicImpl;
        
        private SharpDX.Direct2D1.Bitmap direct2D;

        public WicBitmapSource(ImagingFactory factory, BitmapSource wicImpl)
        {
            this.factory = factory;
            this.wicImpl = wicImpl;
        }

        public int PixelWidth
        {
            get { return this.wicImpl.Size.Width; }
        }

        public int PixelHeight
        {
            get { return this.wicImpl.Size.Height; }
        }

        public double Width
        {
            get
            {
                double dpiX;
                double dpiY;
                this.wicImpl.GetResolution(out dpiX, out dpiY);
                return (this.wicImpl.Size.Width / dpiX) * 96.0;
            }
        }

        public double Height
        {
            get
            {
                double dpiX;
                double dpiY;
                this.wicImpl.GetResolution(out dpiX, out dpiY);
                return (this.wicImpl.Size.Height / dpiY) * 96.0;
            }
        }

        public SharpDX.Direct2D1.Bitmap GetDirect2DBitmap(SharpDX.Direct2D1.RenderTarget renderTarget)
        {
            if (this.direct2D == null)
            {
                FormatConverter converter = new FormatConverter(factory);
                converter.Initialize(this.wicImpl, PixelFormat.Format32bppPBGRA);
                this.direct2D = SharpDX.Direct2D1.Bitmap.FromWicBitmap(renderTarget, converter);
            }

            return this.direct2D;
        }
    }
}

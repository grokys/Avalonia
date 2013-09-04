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
        private BitmapSource wicImpl;

        public WicBitmapSource(BitmapSource wicImpl)
        {
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
    }
}

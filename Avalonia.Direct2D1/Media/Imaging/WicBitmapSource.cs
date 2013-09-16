// -----------------------------------------------------------------------
// <copyright file="WicBitmapSource.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Direct2D1.Media.Imaging
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using Avalonia.Platform;
    using SharpDX;
    using SharpDX.WIC;
    using AvPixelFormat = Avalonia.Media.PixelFormat;
    using AvPixelFormats = Avalonia.Media.PixelFormats;
    using WicPixelFormat = SharpDX.WIC.PixelFormat;

    public class WicBitmapSource : IPlatformBitmapSource
    {
        private ImagingFactory factory;

        private SharpDX.Direct2D1.Bitmap direct2D;

        public WicBitmapSource(ImagingFactory factory, BitmapSource wicImpl)
        {
            this.factory = factory;
            this.WicImpl = wicImpl;
        }

        public BitmapSource WicImpl
        {
            get;
            private set;
        }

        public AvPixelFormat Format
        {
            get { return this.WicImpl.PixelFormat.ToAvalonia(); }
        }

        public int PixelWidth
        {
            get { return this.WicImpl.Size.Width; }
        }

        public int PixelHeight
        {
            get { return this.WicImpl.Size.Height; }
        }

        public double Width
        {
            get
            {
                double dpiX;
                double dpiY;
                this.WicImpl.GetResolution(out dpiX, out dpiY);
                return (this.WicImpl.Size.Width / dpiX) * 96.0;
            }
        }

        public double Height
        {
            get
            {
                double dpiX;
                double dpiY;
                this.WicImpl.GetResolution(out dpiX, out dpiY);
                return (this.WicImpl.Size.Height / dpiY) * 96.0;
            }
        }

        public unsafe void CopyPixels(Array pixels, int stride, int offset)
        {
            if (pixels is uint[])
            {
                fixed (uint* array = (uint[])pixels)
                {
                    IntPtr ptr = (IntPtr)(array + offset);
                    this.WicImpl.CopyPixels(stride, ptr, pixels.Length * sizeof(uint));
                }
            }
            else
            {
                throw new NotImplementedException("Support for this array type not yet implemented.");
            }
        }

        public SharpDX.Direct2D1.Bitmap GetDirect2DBitmap(SharpDX.Direct2D1.RenderTarget renderTarget)
        {
            if (this.direct2D == null)
            {
                FormatConverter converter = new FormatConverter(this.factory);
                converter.Initialize(this.WicImpl, PixelFormat.Format32bppPBGRA);
                this.direct2D = SharpDX.Direct2D1.Bitmap.FromWicBitmap(renderTarget, converter);
            }

            return this.direct2D;
        }
    }
}

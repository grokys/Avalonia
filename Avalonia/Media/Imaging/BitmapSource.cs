// -----------------------------------------------------------------------
// <copyright file="BitmapSource.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media.Imaging
{
    using System;
    using Avalonia.Platform;

    public abstract class BitmapSource : ImageSource
    {
        internal BitmapSource(IPlatformBitmapSource platformImpl)
        {
            this.PlatformImpl = platformImpl;
        }
        
        public virtual PixelFormat Format 
        {
            get { return this.PlatformImpl.Format; }
        }
        
        public virtual int PixelWidth
        {
            get { return this.PlatformImpl.PixelWidth; }
        }

        public virtual int PixelHeight
        {
            get { return this.PlatformImpl.PixelHeight; }
        }

        [AvaloniaSpecific]
        public IPlatformBitmapSource PlatformImpl
        {
            get;
            private set;
        }

        public override double Width
        {
            get { return this.PlatformImpl.Width; }
        }

        public override double Height
        {
            get { return this.PlatformImpl.Height; }
        }

        public virtual void CopyPixels(Array pixels, int stride, int offset)
        {
            this.PlatformImpl.CopyPixels(pixels, stride, offset);
        }
    }
}

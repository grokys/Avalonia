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
        private IPlatformBitmapSource impl;

        internal BitmapSource(IPlatformBitmapSource platformImpl)
        {
            this.impl = platformImpl;
        }

        public virtual int PixelWidth
        {
            get { return this.impl.PixelWidth; }
        }

        public virtual int PixelHeight
        {
            get { return this.impl.PixelHeight; }
        }

        public override double Width
        {
            get { return this.impl.Width; }
        }

        public override double Height
        {
            get { return this.impl.Height; }
        }
    }
}

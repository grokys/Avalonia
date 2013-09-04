// -----------------------------------------------------------------------
// <copyright file="WicBitmapFrame.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Direct2D1.Media.Imaging
{
    using Avalonia.Platform;
    using SharpDX.WIC;

    public class WicBitmapFrame : WicBitmapSource, IPlatformBitmapFrame
    {
        private BitmapFrameDecode wicImpl;

        public WicBitmapFrame(BitmapFrameDecode wicImpl)
            : base(wicImpl)
        {
            this.wicImpl = wicImpl;
        }
    }
}

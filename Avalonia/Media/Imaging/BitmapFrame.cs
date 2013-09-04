// -----------------------------------------------------------------------
// <copyright file="BitmapFrame.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media.Imaging
{
    using Avalonia.Platform;

    public class BitmapFrame : BitmapSource
    {
        private IPlatformBitmapFrame impl;

        internal BitmapFrame(IPlatformBitmapFrame platformImpl)
            : base(platformImpl)
        {
            this.impl = platformImpl;
        }
    }
}

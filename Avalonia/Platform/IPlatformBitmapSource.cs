// -----------------------------------------------------------------------
// <copyright file="IPlatformBitmapSource.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Platform
{
    using System;
    using Avalonia.Media;

    public interface IPlatformBitmapSource
    {
        PixelFormat Format { get; }

        int PixelWidth { get; }

        int PixelHeight { get; }

        double Width { get; }

        double Height { get; }

        void CopyPixels(Array pixels, int stride, int offset);
    }
}

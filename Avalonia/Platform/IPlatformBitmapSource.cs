// -----------------------------------------------------------------------
// <copyright file="IPlatformBitmapSource.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Platform
{
    public interface IPlatformBitmapSource
    {
        int PixelWidth { get; }

        int PixelHeight { get; }

        double Width { get; }

        double Height { get; }
    }
}

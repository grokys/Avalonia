// -----------------------------------------------------------------------
// <copyright file="IPlatformRenderTargetBitmap.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Platform
{
    using Avalonia.Media;

    public interface IPlatformRenderTargetBitmap : IPlatformBitmapSource
    {
        DrawingContext CreateDrawingContext();
    }
}

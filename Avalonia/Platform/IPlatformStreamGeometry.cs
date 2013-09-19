// -----------------------------------------------------------------------
// <copyright file="IPlatformStreamGeometry.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Platform
{
    using System;
    using Avalonia.Media;

    [AvaloniaSpecific]
    public interface IPlatformStreamGeometry : IDisposable
    {
        Rect Bounds { get; }

        Rect GetRenderBounds(Pen pen, double tolerance, ToleranceType type);

        StreamGeometryContext Open();
    }
}

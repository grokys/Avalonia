// -----------------------------------------------------------------------
// <copyright file="IPlatformStreamGeometry.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Platform
{
    using Avalonia.Media;

    [AvaloniaSpecific]
    public interface IPlatformStreamGeometry
    {
        Rect Bounds { get; }

        Rect GetRenderBounds(Pen pen, double tolerance, ToleranceType type);

        StreamGeometryContext Open();
    }
}

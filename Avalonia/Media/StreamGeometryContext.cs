// -----------------------------------------------------------------------
// <copyright file="StreamGeometryContext.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    using System;
    using Avalonia.Threading;

    public abstract class StreamGeometryContext : DispatcherObject, IDisposable
    {
        public abstract void Dispose();

        public abstract void BeginFigure(Point startPoint, bool isFilled, bool isClosed);

        public abstract void BezierTo(Point point1, Point point2, Point point3, bool isStroked, bool isSmoothJoin);

        public abstract void LineTo(Point point, bool isStroked, bool isSmoothJoin);
    }
}

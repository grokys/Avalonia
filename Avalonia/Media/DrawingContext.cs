// -----------------------------------------------------------------------
// <copyright file="DrawingContext.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class DrawingContext : IDisposable
    {
        public abstract void Dispose();

        public abstract void DrawGeometry(Brush brush, Pen pen, Geometry geometry);

        [AvaloniaSpecific]
        public abstract void DrawGeometry(Brush brush, Pen pen, Geometry geometry, Matrix transform);

        public abstract void DrawImage(ImageSource imageSource, Rect rectangle);

        [AvaloniaSpecific]
        public abstract void DrawImage(ImageSource imageSource, double opacity, Rect sourceRectangle, Rect destinationRectangle);

        public abstract void DrawLine(Pen pen, Point point0, Point point1);

        public abstract void DrawRectangle(Brush brush, Pen pen, Rect rectangle);

        public abstract void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, double radiusX, double radiusY);

        public abstract void DrawText(FormattedText formattedText, Point origin);

        public abstract void PushOpacity(double opacity);

        public abstract void PushTransform(Transform transform);

        public abstract void Pop();
    }
}

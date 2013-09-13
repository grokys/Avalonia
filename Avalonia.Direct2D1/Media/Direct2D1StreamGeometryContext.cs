// -----------------------------------------------------------------------
// <copyright file="Direct2D1StreamGeometryContext.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Direct2D1.Media
{
    using Avalonia.Media;
    using SharpDX.Direct2D1;

    public class Direct2D1StreamGeometryContext : StreamGeometryContext
    {
        private GeometrySink sink;

        public Direct2D1StreamGeometryContext(GeometrySink geometrySink)
        {
            this.sink = geometrySink;
        }

        public override void Dispose()
        {
            this.sink.Dispose();
        }

        public override void BeginFigure(Point startPoint, bool isFilled, bool isClosed)
        {
            this.sink.BeginFigure(
                startPoint.ToSharpDX(),
                isFilled ? FigureBegin.Filled : FigureBegin.Hollow);
        }

        public override void LineTo(Point point, bool isStroked, bool isSmoothJoin)
        {
            this.sink.AddLine(point.ToSharpDX());
        }
    }
}

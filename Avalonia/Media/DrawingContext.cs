using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Media
{
    public abstract class DrawingContext : IDisposable
    {
        public abstract void Dispose();
        public abstract void DrawRectangle(Brush brush, Pen pen, Rect rectangle);
        public abstract void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, double radiusX, double radiusY);
        public abstract void DrawText(FormattedText formattedText, Point origin);
        public abstract void PushTransform(Transform transform);
        public abstract void Pop();
    }
}

// -----------------------------------------------------------------------
// <copyright file="Rectangle.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Shapes
{
    using Avalonia.Media;

    public class Rectangle : Shape
    {
        private RectangleGeometry geometry = new RectangleGeometry();

        protected override Geometry DefiningGeometry
        {
            get { return this.geometry; }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            return new Size(
                double.IsNaN(this.Width) ? this.Width : 0,
                double.IsNaN(this.Height) ? this.Height : 0);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.geometry != null && this.geometry.Bounds.Size != finalSize)
            {
                this.geometry.Rect = new Rect(finalSize);
            }

            return base.ArrangeOverride(finalSize);
        }
    }
}

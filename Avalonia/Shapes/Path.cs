// -----------------------------------------------------------------------
// <copyright file="Path.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Shapes
{
    using Avalonia.Media;

    public sealed class Path : Shape
    {
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register(
                "Data",
                typeof(Geometry),
                typeof(Path),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public Geometry Data
        {
            get { return (Geometry)this.GetValue(DataProperty); }
            set { this.SetValue(DataProperty, value); }
        }

        protected internal override void OnRender(DrawingContext drawingContext)
        {
            Pen pen = (this.Stroke != null) ? new Pen(this.Stroke, this.StrokeThickness) : null;
            drawingContext.DrawGeometry(this.Fill, pen, this.Data);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            return this.Data.Bounds.Size;
        }
    }
}

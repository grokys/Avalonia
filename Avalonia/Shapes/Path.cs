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

        public override Geometry RenderedGeometry
        {
            get { return this.Data; }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Rect bounds = this.RenderedGeometry.Bounds;
            return new Size(bounds.Left + bounds.Width, bounds.Top + bounds.Height);
        }
    }
}

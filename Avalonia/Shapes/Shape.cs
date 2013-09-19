// -----------------------------------------------------------------------
// <copyright file="Shape.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Shapes
{
    using System;
    using System.ComponentModel;
    using Avalonia.Media;

    public abstract class Shape : FrameworkElement
    {
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register(
                "Fill",
                typeof(Brush),
                typeof(Shape),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register(
                "Stretch",
                typeof(Stretch),
                typeof(Shape),
                new FrameworkPropertyMetadata(
                    Stretch.None,
                    FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register(
                "Stroke",
                typeof(Brush),
                typeof(Shape),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register(
                "StrokeThickness",
                typeof(double),
                typeof(Shape),
                new FrameworkPropertyMetadata(
                    1.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Brush Fill
        {
            get { return (Brush)this.GetValue(FillProperty); }
            set { this.SetValue(FillProperty, value); }
        }

        public virtual Geometry RenderedGeometry 
        {
            get { return this.DefiningGeometry; }
        }

        public Stretch Stretch
        {
            get { return (Stretch)this.GetValue(StretchProperty); }
            set { this.SetValue(StretchProperty, value); }
        }

        public Brush Stroke
        {
            get { return (Brush)this.GetValue(StrokeProperty); }
            set { this.SetValue(StrokeProperty, value); }
        }

        public double StrokeThickness
        {
            get { return (double)this.GetValue(StrokeThicknessProperty); }
            set { this.SetValue(StrokeThicknessProperty, value); }
        }

        protected abstract Geometry DefiningGeometry 
        { 
            get; 
        }

        protected internal override void OnRender(DrawingContext drawingContext)
        {
            Pen pen = (this.Stroke != null) ? new Pen(this.Stroke, this.StrokeThickness) : null;
            Rect shapeBounds = this.RenderedGeometry.GetRenderBounds(pen);
            Matrix matrix = Matrix.Identity;

            if (this.Stretch != Stretch.None)
            {
                double scaleX = this.ActualWidth / shapeBounds.Width;
                double scaleY = this.ActualHeight / shapeBounds.Height;

                switch (this.Stretch)
                {
                    case Stretch.Uniform:
                        scaleX = scaleY = Math.Min(scaleX, scaleY);
                        break;

                    case Stretch.UniformToFill:
                        // Hmm, in WPF appears to be the same as Uniform. This can't be right...
                        scaleX = scaleY = Math.Min(scaleX, scaleY);
                        break;
                }

                matrix.Translate(-shapeBounds.X, -shapeBounds.Y);
                matrix.Scale(scaleX, scaleY);
                matrix.Translate(
                    (this.ActualWidth - (shapeBounds.Width * scaleX)) / 2,
                    (this.ActualHeight - (shapeBounds.Height * scaleY)) / 2);
            }

            drawingContext.DrawGeometry(this.Fill, pen, this.RenderedGeometry, matrix);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Pen pen = (this.Stroke != null) ? new Pen(this.Stroke, this.StrokeThickness) : null;
            Rect shapeBounds = this.RenderedGeometry.GetRenderBounds(pen);
            Size desired = constraint;
            double sx = 0.0;
            double sy = 0.0;

            if (this.Stretch == Stretch.None)
            {
                return new Size(
                    shapeBounds.X + shapeBounds.Width,
                    shapeBounds.Y + shapeBounds.Height);
            }

            if (double.IsInfinity(constraint.Width))
            {
                desired.Width = shapeBounds.Width;
            }

            if (double.IsInfinity(constraint.Height))
            {
                desired.Height = shapeBounds.Height;
            }

            if (shapeBounds.Width > 0)
            {
                sx = desired.Width / shapeBounds.Width;
            }

            if (shapeBounds.Height > 0)
            {
                sy = desired.Height / shapeBounds.Height;
            }

            if (double.IsInfinity(constraint.Width))
            {
                sx = sy;
            }

            if (double.IsInfinity(constraint.Height))
            {
                sy = sx;
            }

            switch (this.Stretch)
            {
                case Stretch.Uniform:
                    sx = sy = Math.Min(sx, sy);
                    break;
                case Stretch.UniformToFill:
                    sx = sy = Math.Max(sx, sy);
                    break;
                case Stretch.Fill:
                    if (double.IsInfinity(constraint.Width))
                    {
                        sx = 1.0;
                    }

                    if (double.IsInfinity(constraint.Height))
                    {
                        sy = 1.0;
                    }

                    break;
                default:
                    break;
            }

            return new Size(shapeBounds.Width * sx, shapeBounds.Height * sy);
        }
    }
}

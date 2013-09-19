// -----------------------------------------------------------------------
// <copyright file="Border.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Avalonia.Media;

    public class Border : Decorator
    {
        public static readonly DependencyProperty BackgroundProperty =
            Panel.BackgroundProperty.AddOwner(
                typeof(Border),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register(
                "BorderBrush",
                typeof(Brush),
                typeof(Border),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register(
                "BorderThickness",
                typeof(Thickness),
                typeof(Border),
                new FrameworkPropertyMetadata(
                    new Thickness(),
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(
                "CornerRadius",
                typeof(CornerRadius),
                typeof(Border),
                new FrameworkPropertyMetadata(
                    new CornerRadius(),
                    FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty PaddingProperty =
            DependencyProperty.Register(
                "Padding",
                typeof(Thickness),
                typeof(Border),
                new FrameworkPropertyMetadata(
                    new Thickness(),
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Brush Background
        {
            get { return (Brush)this.GetValue(BackgroundProperty); }
            set { this.SetValue(BackgroundProperty, value); }
        }

        public Brush BorderBrush
        {
            get { return (Brush)this.GetValue(BorderBrushProperty); }
            set { this.SetValue(BorderBrushProperty, value); }
        }

        public Thickness BorderThickness
        {
            get { return (Thickness)this.GetValue(BorderThicknessProperty); }
            set { this.SetValue(BorderThicknessProperty, value); }
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)this.GetValue(CornerRadiusProperty); }
            set { this.SetValue(CornerRadiusProperty, value); }
        }

        public Thickness Padding
        {
            get { return (Thickness)this.GetValue(PaddingProperty); }
            set { this.SetValue(PaddingProperty, value); }
        }

        protected internal override void OnRender(DrawingContext drawingContext)
        {
            Rect brushRect = new Rect(new Size(this.ActualWidth, this.ActualHeight));
            Rect penRect = brushRect;
            Pen pen = null;

            if (this.BorderBrush != null && !this.BorderThickness.IsEmpty)
            {
                pen = new Pen(this.BorderBrush, this.BorderThickness.Left);

                double penOffset = -(pen.Thickness / 2);
                brushRect.Inflate(-pen.Thickness, -pen.Thickness);
                penRect.Inflate(penOffset, penOffset);
            }

            if (this.CornerRadius.TopLeft > 0 || this.CornerRadius.BottomLeft > 0)
            {
                drawingContext.DrawRoundedRectangle(
                    this.Background,
                    pen,
                    brushRect,
                    this.CornerRadius.TopLeft,
                    this.CornerRadius.BottomLeft);
            }
            else
            {
                if (this.Background != null)
                {
                    drawingContext.DrawRectangle(this.Background, null, brushRect);
                }

                if (pen != null)
                {
                    drawingContext.DrawRectangle(null, pen, penRect);
                }
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (this.Child != null)
            {
                constraint -= this.Padding + this.BorderThickness;
                this.Child.Measure(constraint);
                return this.Child.DesiredSize + this.Padding + this.BorderThickness;
            }
            else
            {
                return base.MeasureOverride(constraint);
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.Child != null)
            {
                Rect rect = new Rect(finalSize) - this.Padding - this.BorderThickness;
                this.Child.Arrange(rect);
            }

            return finalSize;
        }
    }
}

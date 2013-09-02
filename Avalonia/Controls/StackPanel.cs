// -----------------------------------------------------------------------
// <copyright file="StackPanel.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;

    public enum Orientation
    {
        Horizontal,
        Vertical,
    }

    public class StackPanel : Panel
    {
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(
                "Orientation",
                typeof(Orientation),
                typeof(StackPanel),
                new FrameworkPropertyMetadata(
                    Orientation.Vertical,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Orientation Orientation 
        { 
            get { return (Orientation)this.GetValue(OrientationProperty); }
            set { this.SetValue(OrientationProperty, value); }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size childAvailable = new Size(double.PositiveInfinity, double.PositiveInfinity);
            Size measured = new Size(0, 0);

            if (Orientation == Orientation.Vertical)
            {
                // Vertical layout
                childAvailable.Width = constraint.Width;
                
                if (!double.IsNaN(this.Width))
                {
                    childAvailable.Width = this.Width;
                }

                childAvailable.Width = Math.Min(childAvailable.Width, this.MaxWidth);
                childAvailable.Width = Math.Max(childAvailable.Width, this.MinWidth);
            }
            else
            {
                // Horizontal layout
                childAvailable.Height = constraint.Height;

                if (!double.IsNaN(this.Height))
                {
                    childAvailable.Height = this.Height;
                }

                childAvailable.Height = Math.Min(childAvailable.Height, this.MaxHeight);
                childAvailable.Height = Math.Max(childAvailable.Height, this.MinHeight);
            }

            // Measure our children to get our extents
            foreach (UIElement child in Children)
            {
                child.Measure(childAvailable);
                Size size = child.DesiredSize;

                if (Orientation == Orientation.Vertical)
                {
                    measured.Height += size.Height;
                    measured.Width = Math.Max(measured.Width, size.Width);
                }
                else
                {
                    measured.Width += size.Width;
                    measured.Height = Math.Max(measured.Height, size.Height);
                }
            }

            return measured;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Size arranged = finalSize;

            if (Orientation == Orientation.Vertical)
                arranged.Height = 0;
            else
                arranged.Width = 0;

            // Arrange our children
            foreach (UIElement child in Children)
            {
                Size size = child.DesiredSize;

                if (Orientation == Orientation.Vertical)
                {
                    size.Width = finalSize.Width;

                    Rect childFinal = new Rect(0, arranged.Height, size.Width, size.Height);

                    if (childFinal.IsEmpty)
                        child.Arrange(new Rect());
                    else
                        child.Arrange(childFinal);

                    arranged.Width = Math.Max(arranged.Width, size.Width);
                    arranged.Height += size.Height;
                }
                else
                {
                    size.Height = finalSize.Height;

                    Rect childFinal = new Rect(arranged.Width, 0, size.Width, size.Height);
                    if (childFinal.IsEmpty)
                        child.Arrange(new Rect());
                    else
                        child.Arrange(childFinal);

                    arranged.Width += size.Width;
                    arranged.Height = Math.Max(arranged.Height, size.Height);
                }
            }

            if (Orientation == Orientation.Vertical)
                arranged.Height = Math.Max(arranged.Height, finalSize.Height);
            else
                arranged.Width = Math.Max(arranged.Width, finalSize.Width);

            return arranged;
        }
    }
}

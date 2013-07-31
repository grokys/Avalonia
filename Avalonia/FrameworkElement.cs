// -----------------------------------------------------------------------
// <copyright file="FrameworkElement.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Avalonia.Media;

    public class FrameworkElement : UIElement
    {
        public static readonly DependencyProperty MarginProperty =
            DependencyProperty.Register(
                "Margin",
                typeof(Thickness),
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    new Thickness(),
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double ActualWidth
        {
            get { return this.RenderSize.Width; }
        }

        public double ActualHeight
        {
            get { return this.RenderSize.Height; }
        }

        public Thickness Margin
        {
            get { return (Thickness)this.GetValue(MarginProperty); }
            set { this.SetValue(MarginProperty, value); }
        }

        public DependencyObject TemplatedParent
        {
            get;
            internal set;
        }

        public virtual bool ApplyTemplate()
        {
            return false;
        }

        protected sealed override Size MeasureCore(Size availableSize)
        {
            this.ApplyTemplate();

            availableSize = new Size(
                Math.Max(0, availableSize.Width - this.Margin.Left - this.Margin.Right),
                Math.Max(0, availableSize.Height - this.Margin.Top - this.Margin.Bottom));

            return this.MeasureOverride(availableSize);
        }

        protected virtual Size MeasureOverride(Size constraint)
        {
            return new Size();
        }

        protected sealed override void ArrangeCore(Rect finalRect)
        {
            Point origin = new Point(
                finalRect.Left + this.Margin.Left,
                finalRect.Top + this.Margin.Top);
            Size size = new Size(
                Math.Max(0, finalRect.Width - this.Margin.Left - this.Margin.Right),
                Math.Max(0, finalRect.Height - this.Margin.Top - this.Margin.Bottom));

            size = this.ArrangeOverride(size);
            base.ArrangeCore(new Rect(origin, size));
        }

        protected virtual Size ArrangeOverride(Size finalSize)
        {
            return finalSize;
        }
    }
}

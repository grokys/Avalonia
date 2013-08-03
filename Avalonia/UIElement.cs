// -----------------------------------------------------------------------
// <copyright file="UIElement.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using Avalonia.Input;
    using Avalonia.Media;

    public class UIElement : Visual
    {
        public UIElement()
        {
            this.IsMeasureValid = true;
            this.IsArrangeValid = true;
        }

        public event MouseButtonEventHandler MouseLeftButtonDown;

        public Size DesiredSize { get; set; }

        public bool IsMeasureValid { get; private set; }

        public bool IsArrangeValid { get; private set; }

        public Size RenderSize { get; private set; }

        public void Measure(Size availableSize)
        {
            this.DesiredSize = this.MeasureCore(availableSize);
            this.IsMeasureValid = true;
        }

        public void Arrange(Rect finalRect)
        {
            if (!this.IsMeasureValid)
            {
                this.Measure(finalRect.Size);
            }

            this.ArrangeCore(finalRect);
            this.IsArrangeValid = true;
        }

        public void InvalidateMeasure()
        {
            this.IsMeasureValid = this.IsArrangeValid = false;
            LayoutManager.Instance.QueueMeasure(this);
        }

        public void InvalidateArrange()
        {
            this.IsArrangeValid = false;
            LayoutManager.Instance.QueueArrange(this);
        }

        public void InvalidateVisual()
        {
            this.InvalidateArrange();
        }

        public void UpdateLayout()
        {
            Size size = this.RenderSize;
            this.Measure(size);
            this.Arrange(new Rect(new Point(), size));
        }

        internal override Rect GetHitTestBounds()
        {
            return new Rect((Point)this.VisualOffset, this.RenderSize);
        }

        protected internal virtual void OnRender(DrawingContext drawingContext)
        {
        }

        protected virtual Size MeasureCore(Size availableSize)
        {
            return new Size();
        }

        protected virtual void ArrangeCore(Rect finalRect)
        {
            this.VisualOffset = (Vector)finalRect.TopLeft;
            this.RenderSize = finalRect.Size;
        }

        protected void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (this.MouseLeftButtonDown != null)
            {
                this.MouseLeftButtonDown(this, e);
            }
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="UIElement.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using Avalonia.Input;
    using Avalonia.Media;

    public class UIElement : Visual
    {
        public event MouseButtonEventHandler MouseLeftButtonDown;

        public Size DesiredSize { get; set; }
        
        public bool IsMeasureValid { get; private set; }
        
        public bool IsArrangeValid { get; private set; }
        
        public Size RenderSize { get; private set; }

        public void Measure(Size availableSize)
        {
            this.DesiredSize = this.MeasureCore(availableSize);
        }

        public void Arrange(Rect finalRect)
        {
            this.ArrangeCore(finalRect);
        }

        public void InvalidateMeasure()
        {
            this.IsMeasureValid = this.IsArrangeValid = false;
        }

        public void InvalidateArrange()
        {
            this.IsArrangeValid = false;
            this.HackDoingARedraw();
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

        protected internal virtual void OnRender(DrawingContext drawingContext)
        {
        }

        protected virtual Size MeasureCore(Size availableSize)
        {
            return new Size();
        }

        protected virtual void ArrangeCore(Rect finalRect)
        {
            if (finalRect.Left != 0 || finalRect.Top != 0)
            {
                this.VisualTransform = new TranslateTransform(finalRect.Top, finalRect.Left);
            }
            else
            {
                this.VisualTransform = null;
            }

            this.RenderSize = finalRect.Size;
        }

        protected void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (this.MouseLeftButtonDown != null)
            {
                this.MouseLeftButtonDown(this, e);
            }
        }

        private void HackDoingARedraw()
        {
            Visual visual = this.VisualParent as Visual;
            Window window = visual as Window;

            while (visual != null && window == null)
            {
                visual = visual.VisualParent as Visual;
                window = visual as Window;
            }

            if (window != null)
            {
                window.DoMeasureArrange();
            }
        }
    }
}

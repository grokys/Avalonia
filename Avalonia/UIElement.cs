// -----------------------------------------------------------------------
// <copyright file="UIElement.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Avalonia.Input;
    using Avalonia.Media;

    public class UIElement : Visual, IInputElement
    {
        public static readonly RoutedEvent MouseMoveEvent =
            EventManager.RegisterRoutedEvent(
                "MouseMove",
                RoutingStrategy.Bubble,
                typeof(MouseEventHandler),
                typeof(UIElement));

        private bool measureCalled;
        private Size previousMeasureSize;
        private Dictionary<RoutedEvent, List<Delegate>> eventHandlers = new Dictionary<RoutedEvent, List<Delegate>>();

        public UIElement()
        {
            this.IsMeasureValid = true;
            this.IsArrangeValid = true;

            this.AddHandler(MouseMoveEvent, (MouseEventHandler)((s, e) => this.OnMouseMove(e)));
        }

        public event MouseEventHandler MouseMove
        {
            add { this.AddHandler(MouseMoveEvent, value); }
            remove { this.RemoveHandler(MouseMoveEvent, value); }
        }

        public Size DesiredSize { get; set; }

        public bool IsMeasureValid { get; private set; }

        public bool IsArrangeValid { get; private set; }

        public Size RenderSize { get; private set; }

        public void Measure(Size availableSize)
        {
            this.measureCalled = true;
            this.previousMeasureSize = availableSize;
            this.DesiredSize = this.MeasureCore(availableSize);
            this.IsMeasureValid = true;
            this.IsArrangeValid = false;
        }

        public void Arrange(Rect finalRect)
        {
            if (!this.measureCalled || !this.IsMeasureValid)
            {
                this.Measure(this.measureCalled ? this.previousMeasureSize : finalRect.Size);
            }

            this.ArrangeCore(finalRect);
            this.IsArrangeValid = true;
        }

        public void InvalidateMeasure()
        {
            this.IsMeasureValid = false;
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

        public IInputElement InputHitTest(Point point)
        {
            Rect bounds = new Rect(new Point(), this.RenderSize);

            if (bounds.Contains(point))
            {
                foreach (UIElement child in VisualTreeHelper.GetChildren(this).OfType<UIElement>())
                {
                    Point offsetPoint = point - child.VisualOffset;
                    IInputElement hit = child.InputHitTest(offsetPoint);

                    if (hit != null)
                    {
                        return hit;
                    }
                }

                return this;
            }
            else
            {
                return null;
            }
        }

        public void AddHandler(RoutedEvent routedEvent, Delegate handler)
        {
            if (routedEvent == null)
            {
                throw new ArgumentNullException("routedEvent");
            }

            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            List<Delegate> delegates;

            if (!this.eventHandlers.TryGetValue(routedEvent, out delegates))
            {
                delegates = new List<Delegate>();
                this.eventHandlers.Add(routedEvent, delegates);
            }

            delegates.Add(handler);
        }

        public void RemoveHandler(RoutedEvent routedEvent, Delegate handler)
        {
            if (routedEvent == null)
            {
                throw new ArgumentNullException("routedEvent");
            }

            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            List<Delegate> delegates;

            if (this.eventHandlers.TryGetValue(routedEvent, out delegates))
            {
                delegates.Remove(handler);
            }
        }

        public void RaiseEvent(RoutedEventArgs e)
        {
            switch (e.RoutedEvent.RoutingStrategy)
            {
                case RoutingStrategy.Bubble:
                    this.BubbleEvent(e);
                    break;
                default:
                    throw new NotImplementedException();
            }
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

        protected virtual void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
        }

        protected virtual void OnMouseMove(MouseEventArgs e)
        {
        }

        private void BubbleEvent(RoutedEventArgs e)
        {
            UIElement target = this;

            while (target != null)
            {
                target.RaiseEventImpl(e);
                target = VisualTreeHelper.GetAncestor<UIElement>(target);
            }
        }

        private void RaiseEventImpl(RoutedEventArgs e)
        {
            List<Delegate> delegates;

            if (this.eventHandlers.TryGetValue(e.RoutedEvent, out delegates))
            {
                foreach (Delegate handler in delegates)
                {
                    // TODO: Implement the Handled stuff.
                    handler.DynamicInvoke(this, e);
                }
            }
        }
    }
}

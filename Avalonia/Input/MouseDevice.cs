// -----------------------------------------------------------------------
// <copyright file="MouseDevice.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Input
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Avalonia.Media;
    using Avalonia.Platform;

    public abstract class MouseDevice : InputDevice, IDisposable
    {
        private List<UIElement> mouseOvers = new List<UIElement>();

        public MouseDevice()
        {
            InputManager.Current.PreProcessInput += this.PreProcessMouseInput;
        }

        ~MouseDevice()
        {
            this.Dispose(false);
        }

        public PresentationSource ActiveSource
        {
            get;
            protected set;
        }

        public IInputElement Captured
        {
            get;
            private set;
        }

        public IInputElement DirectlyOver 
        {
            get { return this.Target; }
        }

        public virtual void Capture(IInputElement element)
        {
            this.Captured = element;
            this.UpdateUIElementMouseOvers();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Point GetPosition(IInputElement relativeTo)
        {
            Point p = this.GetClientPosition();
            Visual v = (Visual)relativeTo;

            if (v != null)
            {
                p -= v.VisualOffset;

                foreach (Visual ancestor in VisualTreeHelper.GetAncestors(v).OfType<Visual>())
                {
                    p -= ancestor.VisualOffset;
                }
            }

            return p;
        }

        protected virtual void Dispose(bool disposing)
        {
            InputManager.Current.PreProcessInput -= this.PreProcessMouseInput;
        }

        protected abstract Point GetClientPosition();

        protected abstract Point GetScreenPosition();

        private void PreProcessMouseInput(object sender, PreProcessInputEventArgs e)
        {
            if (e.Input.Device == this)
            {
                RawMouseEventArgs rawMouseEvent = e.Input as RawMouseEventArgs;

                if (rawMouseEvent != null)
                {
                    if (this.ProcessRawMouseEvent(rawMouseEvent))
                    {
                        e.Cancel();
                    }
                }
                else if (e.Input.RoutedEvent == UIElement.MouseMoveEvent)
                {
                    this.UpdateUIElementMouseOvers();
                }
            }
        }

        private bool ProcessRawMouseEvent(RawMouseEventArgs input)
        {
            MouseEventArgs e = null;

            switch (input.Type)
            {
                case RawMouseEventType.Move:
                    e = new MouseEventArgs(this, input.Timestamp);
                    e.RoutedEvent = UIElement.MouseMoveEvent;
                    break;

                case RawMouseEventType.LeftButtonDown:
                    e = new MouseButtonEventArgs(this, input.Timestamp);
                    e.RoutedEvent = UIElement.MouseLeftButtonDownEvent;
                    break;

                case RawMouseEventType.LeftButtonUp:
                    e = new MouseButtonEventArgs(this, input.Timestamp);
                    e.RoutedEvent = UIElement.MouseLeftButtonUpEvent;
                    break;
            }

            if (e != null)
            {
                InputManager.Current.ProcessInput(e);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void UpdateUIElementMouseOvers()
        {
            IEnumerable<UIElement> current = this.ElementAndAncestors(this.Target);

            foreach (UIElement ui in current.Except(this.mouseOvers))
            {
                ui.SetValue(UIElement.IsMouseOverProperty, true);
                this.mouseOvers.Add(ui);

                MouseEventArgs e = new MouseEventArgs(this, Environment.TickCount);
                e.RoutedEvent = UIElement.MouseEnterEvent;
                ui.RaiseEvent(e);
            }

            foreach (UIElement ui in this.mouseOvers.Except(current).ToArray())
            {
                ui.SetValue(UIElement.IsMouseOverProperty, false);
                this.mouseOvers.Remove(ui);

                MouseEventArgs e = new MouseEventArgs(this, Environment.TickCount);
                e.RoutedEvent = UIElement.MouseLeaveEvent;
                ui.RaiseEvent(e);
            }
        }

        private IEnumerable<UIElement> ElementAndAncestors(IInputElement mouseOver)
        {
            UIElement mo = (UIElement)mouseOver;

            if (mo != null)
            {
                yield return mo;

                foreach (UIElement e in VisualTreeHelper.GetAncestors(mo).OfType<UIElement>())
                {
                    yield return e;
                }
            }
        }
    }
}

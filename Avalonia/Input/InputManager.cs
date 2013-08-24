// -----------------------------------------------------------------------
// <copyright file="InputManager.cs" company="Steven Kirk">
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
    using Avalonia.Threading;

    public sealed class InputManager : DispatcherObject
    {
        private Dictionary<PresentationSource, List<UIElement>> mouseOvers = 
            new Dictionary<PresentationSource, List<UIElement>>();

        static InputManager()
        {
            Current = new InputManager();
        }

        public static InputManager Current
        {
            get;
            private set;
        }

        public bool ProcessInput(InputEventArgs input)
        {
            RawMouseMoveEventArgs mouseMove = input as RawMouseMoveEventArgs;

            if (mouseMove != null)
            {
                return this.ProcessMouseMove(mouseMove);
            }
            else
            {
                return false;
            }
        }

        private bool ProcessMouseMove(RawMouseMoveEventArgs input)
        {
            MouseDevice mouse = (MouseDevice)input.Device;
            UIElement uiElement = input.PresentationSource.RootVisual as UIElement;

            if (uiElement != null)
            {
                Point position = mouse.GetPosition(uiElement);
                IInputElement mouseOver = uiElement.InputHitTest(position);
                MouseEventArgs e = new MouseEventArgs(mouse, input.Timestamp);
                e.OriginalSource = mouseOver;
                e.RoutedEvent = UIElement.MouseMoveEvent;
                this.RaiseEvent(e);
                this.UpdateMouseOver(mouse, input.PresentationSource, mouseOver);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void UpdateMouseOver(
            MouseDevice mouse, 
            PresentationSource presentationSource, 
            IInputElement mouseOver)
        {
            List<UIElement> old = this.GetMouseOvers(presentationSource);
            IEnumerable<UIElement> current = this.ElementAndAncestors(mouseOver);

            foreach (UIElement ui in current.Except(old))
            {
                ui.SetValue(UIElement.IsMouseOverProperty, true);
                old.Add(ui);

                MouseEventArgs e = new MouseEventArgs(mouse, Environment.TickCount);
                e.RoutedEvent = UIElement.MouseEnterEvent;
                ui.RaiseEvent(e);
            }

            foreach (UIElement ui in old.Except(current).ToArray())
            {
                ui.SetValue(UIElement.IsMouseOverProperty, false);
                old.Remove(ui);

                MouseEventArgs e = new MouseEventArgs(mouse, Environment.TickCount);
                e.RoutedEvent = UIElement.MouseLeaveEvent;
                ui.RaiseEvent(e);
            }
        }

        private List<UIElement> GetMouseOvers(PresentationSource presentationSource)
        {
            List<UIElement> result;

            if (!this.mouseOvers.TryGetValue(presentationSource, out result))
            {
                result = new List<UIElement>();
                this.mouseOvers.Add(presentationSource, result);
            }

            return result;
        }

        private IEnumerable<UIElement> ElementAndAncestors(IInputElement mouseOver)
        {
            return new[] { (DependencyObject)mouseOver }
                .Concat(VisualTreeHelper.GetAncestors((DependencyObject)mouseOver))
                .OfType<UIElement>();
        }

        private void RaiseEvent(RoutedEventArgs e)
        {
            UIElement uiElement = e.OriginalSource as UIElement;

            if (uiElement != null)
            {
                uiElement.RaiseEvent(e);
            }
        }
    }
}

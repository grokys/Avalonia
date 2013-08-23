// -----------------------------------------------------------------------
// <copyright file="InputManager.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Input
{
    using Avalonia.Platform;
    using Avalonia.Threading;

    public sealed class InputManager : DispatcherObject
    {
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

                MouseEventArgs e = new MouseEventArgs(mouse, input.Timestamp);
                e.OriginalSource = uiElement.InputHitTest(position);
                e.RoutedEvent = UIElement.MouseMoveEvent;

                this.RaiseEvent(e);
                return true;
            }
            else
            {
                return false;
            }
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

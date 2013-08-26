// -----------------------------------------------------------------------
// <copyright file="KeyboardDevice.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Input
{
    using System;
    using Avalonia.Controls;
    using Avalonia.Media;
    using Avalonia.Platform;

    public abstract class KeyboardDevice : InputDevice
    {
        private UIElement target;

        public KeyboardDevice()
        {
            InputManager.Current.PreProcessInput += this.PreProcessKeyboardInput;
            InputManager.Current.PreProcessInput += this.PreProcessMouseInput;
        }

        void Current_PreProcessInput(object sender, PreProcessInputEventArgs e)
        {
            throw new NotImplementedException();
        }

        public IInputElement FocusedElement
        {
            get { return this.target; }
        }

        public override IInputElement Target
        {
            get 
            {
                return (IInputElement)this.target ?? (IInputElement)this.ActiveSource.RootVisual;
            }
        }

        public IInputElement Focus(IInputElement element)
        {
            UIElement focusElement = this.FindFocusable(element);

            if (focusElement != null)
            {
                this.SetFocus(focusElement);
            }

            return this.target;
        }

        private void PreProcessMouseInput(object sender, PreProcessInputEventArgs e)
        {
            if (e.Input.Device == Mouse.PrimaryDevice)
            {
                if (e.Input.RoutedEvent == Mouse.MouseLeftButtonDownEvent)
                {
                    IInputElement element = e.Input.OriginalSource as IInputElement;

                    if (element != null)
                    {
                        this.Focus(element);
                    }
                }
            }
        }

        private void PreProcessKeyboardInput(object sender, PreProcessInputEventArgs e)
        {
            if (e.Input.Device == this)
            {
                RawKeyEventArgs rawKeyEvent = e.Input as RawKeyEventArgs;

                if (rawKeyEvent != null)
                {
                    switch (rawKeyEvent.Type)
                    {
                        case RawKeyEventType.KeyDown:
                            KeyEventArgs ek = new KeyEventArgs(
                                (KeyboardDevice)rawKeyEvent.Device,
                                Mouse.PrimaryDevice.ActiveSource,
                                rawKeyEvent.Timestamp,
                                rawKeyEvent.Key);
                            ek.RoutedEvent = Keyboard.KeyDownEvent;
                            InputManager.Current.ProcessInput(ek);
                            e.Cancel();
                            break;
                    }
                }
            }
        }

        private UIElement FindFocusable(object o)
        {
            UIElement ui = o as UIElement;

            while (ui != null)
            {
                if (ui.IsVisible && ui.Focusable)
                {
                    return ui;
                }

                ui = VisualTreeHelper.GetAncestor<UIElement>(ui);
            }

            return null;
        }

        private void SetFocus(UIElement element)
        {
            if (element != this.target)
            {
                // TODO: Set IsIsKeyboardFocusWithin for children, raise events.

                if (this.target != null)
                {
                    this.target.SetValue(UIElement.IsKeyboardFocusedProperty, false);
                }

                if (element != null)
                {
                    element.SetValue(UIElement.IsKeyboardFocusedProperty, true);
                }

                this.target = element;
            }
        }
    }
}

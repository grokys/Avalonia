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

    public abstract class KeyboardDevice : InputDevice
    {
        private IInputElement target;

        public KeyboardDevice()
        {
            InputManager.Current.PreProcessInput += this.PreProcessMouseInput;
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
                element.SetValue(UIElement.IsKeyboardFocusedProperty, true);
                element.SetValue(UIElement.IsIsKeyboardFocusWithinProperty, true);

                // TODO: Set IsIsKeyboardFocusWithin for children, raise events.

                this.target = element;
            }
        }
    }
}

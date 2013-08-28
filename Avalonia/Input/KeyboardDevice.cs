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

    [Flags]
    public enum ModifierKeys
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Windows = 8,
    }

    [Flags]
    public enum KeyStates
    {
        None = 0,
        Down = 1,
        Toggled = 2,
    }

    public abstract class KeyboardDevice : InputDevice
    {
        private UIElement target;

        public KeyboardDevice()
        {
            InputManager.Current.PreProcessInput += this.PreProcessKeyboardInput;
            InputManager.Current.PreProcessInput += this.PreProcessMouseInput;
        }

        public IInputElement FocusedElement
        {
            get { return this.target; }
        }

        public ModifierKeys Modifiers
        {
            get
            {
                ModifierKeys result = 0;

                if (this.GetKeyStatesFromSystem(Key.LeftAlt) == KeyStates.Down || 
                    this.GetKeyStatesFromSystem(Key.RightAlt) == KeyStates.Down) 
                {
                    result |= ModifierKeys.Alt;
                }

                if (this.GetKeyStatesFromSystem(Key.LeftCtrl) == KeyStates.Down ||
                    this.GetKeyStatesFromSystem(Key.RightCtrl) == KeyStates.Down)
                {
                    result |= ModifierKeys.Control;
                }

                if (this.GetKeyStatesFromSystem(Key.LeftShift) == KeyStates.Down ||
                    this.GetKeyStatesFromSystem(Key.RightShift) == KeyStates.Down)
                {
                    result |= ModifierKeys.Shift;
                }

                if (this.GetKeyStatesFromSystem(Key.LWin) == KeyStates.Down ||
                    this.GetKeyStatesFromSystem(Key.RWin) == KeyStates.Down)
                {
                    result |= ModifierKeys.Windows;
                }

                return result;
            }
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

        protected internal abstract string KeyToString(Key key);

        protected abstract KeyStates GetKeyStatesFromSystem(Key key);

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
                if (this.target != null)
                {
                    this.target.SetValue(UIElement.IsKeyboardFocusedProperty, false);

                    KeyboardFocusChangedEventArgs e = new KeyboardFocusChangedEventArgs();
                    e.OriginalSource = e.Source = this.target;
                    e.RoutedEvent = UIElement.LostKeyboardFocusEvent;
                    this.target.RaiseEvent(e);
                }

                if (element != null)
                {
                    element.SetValue(UIElement.IsKeyboardFocusedProperty, true);

                    KeyboardFocusChangedEventArgs e = new KeyboardFocusChangedEventArgs();
                    e.OriginalSource = e.Source = element;
                    e.RoutedEvent = UIElement.GotKeyboardFocusEvent;
                    element.RaiseEvent(e);
                }

                this.target = element;
            }
        }
    }
}

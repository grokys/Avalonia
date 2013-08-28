// -----------------------------------------------------------------------
// <copyright file="Keyboard.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Input
{
    using Avalonia.Platform;

    public static class Keyboard
    {
        public static readonly RoutedEvent GotKeyboardFocusEvent =
            EventManager.RegisterRoutedEvent(
                "GotKeyboardFocus",
                RoutingStrategy.Bubble,
                typeof(KeyboardFocusChangedEventHandler),
                typeof(Keyboard));

        public static readonly RoutedEvent LostKeyboardFocusEvent =
            EventManager.RegisterRoutedEvent(
                "LostKeyboardFocus",
                RoutingStrategy.Bubble,
                typeof(KeyboardFocusChangedEventHandler),
                typeof(Keyboard));

        public static readonly RoutedEvent KeyDownEvent =
            EventManager.RegisterRoutedEvent(
                "KeyDown",
                RoutingStrategy.Bubble,
                typeof(KeyEventHandler),
                typeof(Keyboard));

        static Keyboard()
        {
            PrimaryDevice = PlatformInterface.Instance.KeyboardDevice;
        }

        public static KeyboardDevice PrimaryDevice
        {
            get;
            private set;
        }

        public static IInputElement FocusedElement 
        {
            get { return PrimaryDevice.FocusedElement; }
        }

        public static IInputElement Focus(IInputElement element)
        {
            return PrimaryDevice.Focus(element);
        }
    }
}

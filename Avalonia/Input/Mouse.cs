// -----------------------------------------------------------------------
// <copyright file="Mouse.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Input
{
    using Avalonia.Platform;

    public static class Mouse
    {
        public static readonly RoutedEvent MouseEnterEvent =
            EventManager.RegisterRoutedEvent(
                "MouseEnter",
                RoutingStrategy.Direct,
                typeof(MouseEventHandler),
                typeof(Mouse));

        public static readonly RoutedEvent MouseLeaveEvent =
            EventManager.RegisterRoutedEvent(
                "MouseLeave",
                RoutingStrategy.Direct,
                typeof(MouseEventHandler),
                typeof(Mouse));

        public static readonly RoutedEvent MouseLeftButtonDownEvent =
            EventManager.RegisterRoutedEvent(
                "MouseLeftButtonDown",
                RoutingStrategy.Bubble,
                typeof(MouseButtonEventHandler),
                typeof(Mouse));

        public static readonly RoutedEvent MouseLeftButtonUpEvent =
            EventManager.RegisterRoutedEvent(
                "MouseLeftButtonUp",
                RoutingStrategy.Bubble,
                typeof(MouseButtonEventHandler),
                typeof(Mouse));

        public static readonly RoutedEvent MouseMoveEvent =
            EventManager.RegisterRoutedEvent(
                "MouseMove",
                RoutingStrategy.Bubble,
                typeof(MouseEventHandler),
                typeof(Mouse));

        static Mouse()
        {
            PrimaryDevice = PlatformFactory.Instance.MouseDevice;
        }

        public static IInputElement Captured 
        {
            get { return PrimaryDevice.Captured; }
        }

        public static MouseDevice PrimaryDevice
        {
            get;
            private set;
        }
    }
}

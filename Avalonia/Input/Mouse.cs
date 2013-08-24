using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Input
{
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
    }
}

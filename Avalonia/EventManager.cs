// -----------------------------------------------------------------------
// <copyright file="EventManager.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public enum RoutingStrategy
    {
        Tunnel,
        Bubble,
        Direct,
    }

    public static class EventManager
    {
        public static RoutedEvent RegisterRoutedEvent(
            string name,
            RoutingStrategy routingStrategy,
            Type handlerType,
            Type ownerType)
        {
            return new RoutedEvent(name, routingStrategy, handlerType, ownerType);
        }
    }
}

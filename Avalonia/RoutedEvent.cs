using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia
{
    public sealed class RoutedEvent
    {
        internal RoutedEvent(
            string name,
            RoutingStrategy routingStrategy,
            Type handlerType,
            Type ownerType)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("'name' property cannot be null or an empty string.");
            }

            if (handlerType == null)
            {
                throw new ArgumentNullException("handlerType");
            }

            if (!handlerType.IsAssignableFrom(typeof(Delegate)))
            {
                throw new ArgumentException("'handlerType' must be a delegate type.");
            }

            if (ownerType == null)
            {
                throw new ArgumentNullException("ownerType");
            }

            this.Name = name;
            this.RoutingStrategy = routingStrategy;
            this.HandlerType = handlerType;
            this.OwnerType = ownerType;
        }

        public Type HandlerType { get; private set; }
        public string Name { get; private set; }
        public Type OwnerType { get; private set; }
        public RoutingStrategy RoutingStrategy { get; private set; }
    }
}

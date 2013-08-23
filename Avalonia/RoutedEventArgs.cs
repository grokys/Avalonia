// -----------------------------------------------------------------------
// <copyright file="RoutedEventArgs.cs" company="Steven Kirk">
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

    public class RoutedEventArgs : EventArgs
    {
        public RoutedEventArgs()
        {
        }

        public RoutedEventArgs(RoutedEvent routedEvent)
        {
            this.RoutedEvent = routedEvent;
        }

        public RoutedEventArgs(RoutedEvent routedEvent, object source)
        {
            this.RoutedEvent = routedEvent;
            this.Source = source;
        }

        public bool Handled { get; set; }

        public object OriginalSource { get; set; }
        
        public RoutedEvent RoutedEvent { get; set; }
        
        public object Source { get; set; }
    }
}

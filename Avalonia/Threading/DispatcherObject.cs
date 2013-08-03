// -----------------------------------------------------------------------
// <copyright file="DispatcherObject.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Threading
{
    public class DispatcherObject
    {
        public DispatcherObject()
        {
            this.Dispatcher = Dispatcher.CurrentDispatcher;
        }

        public Dispatcher Dispatcher { get; private set; }
    }
}

// -----------------------------------------------------------------------
// <copyright file="DispatcherObject.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Threading
{
    public class DispatcherObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherObject"/> class.
        /// </summary>
        public DispatcherObject()
        {
            this.Dispatcher = Dispatcher.CurrentDispatcher;
        }

        public Dispatcher Dispatcher { get; set; }
    }
}

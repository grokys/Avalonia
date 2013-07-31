// -----------------------------------------------------------------------
// <copyright file="DispatcherEventArgs.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Threading
{
    using System;

    public class DispatcherEventArgs : EventArgs
    {
        private Dispatcher dispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherEventArgs"/> class.
        /// </summary>
        internal DispatcherEventArgs(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public Dispatcher Dispatcher
        {
            get { return this.dispatcher; }
        }
    }
}
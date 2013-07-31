// -----------------------------------------------------------------------
// <copyright file="DispatcherHookEventArgs.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Threading
{
    using System;

    public delegate void DispatcherHookEventHandler(object sender, DispatcherHookEventArgs e);

    public sealed class DispatcherHookEventArgs : EventArgs
    {
        private DispatcherOperation operation;

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherHookEventArgs"/> class.
        /// </summary>
        public DispatcherHookEventArgs(DispatcherOperation operation)
        {
            this.operation = operation;
        }

        public Dispatcher Dispatcher
        {
            get
            {
                return this.operation.Dispatcher;
            }
        }

        public DispatcherOperation Operation
        {
            get
            {
                return this.operation;
            }
        }
    }
}
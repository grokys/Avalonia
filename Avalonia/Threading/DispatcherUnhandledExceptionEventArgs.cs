// -----------------------------------------------------------------------
// <copyright file="DispatcherUnhandledExceptionEventArgs.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Threading
{
    using System;

    public delegate void DispatcherUnhandledExceptionEventHandler(object sender, DispatcherUnhandledExceptionEventArgs e);

    public sealed class DispatcherUnhandledExceptionEventArgs : DispatcherEventArgs
    {
        private Exception exception;
        private bool handled;

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherUnhandledExceptionEventArgs"/> class.
        /// </summary>
        internal DispatcherUnhandledExceptionEventArgs(Dispatcher dispatcher, Exception exception)
                    : base(dispatcher)
        {
            this.exception = exception;
        }

        public Exception Exception
        {
            get { return this.exception; }
        }

        public bool Handled
        {
            get { return this.handled; }
            set { this.handled = value; }
        }
    }
}
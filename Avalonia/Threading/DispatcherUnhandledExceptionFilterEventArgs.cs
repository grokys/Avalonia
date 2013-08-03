// -----------------------------------------------------------------------
// <copyright file="DispatcherUnhandledExceptionFilterEventArgs.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Threading
{
    using System;

    public delegate void DispatcherUnhandledExceptionFilterEventHandler(
        object sender,
        DispatcherUnhandledExceptionFilterEventArgs e);

    public sealed class DispatcherUnhandledExceptionFilterEventArgs : DispatcherEventArgs
    {
        private Exception exception;
        private bool requestCatch;

        internal DispatcherUnhandledExceptionFilterEventArgs(Dispatcher dispatcher, Exception exception)
                    : base(dispatcher)
        {
            this.exception = exception;
        }

        public Exception Exception
        {
            get { return this.exception; }
        }

        public bool RequestCatch
        {
            get { return this.requestCatch; }
            set { this.requestCatch = value; }
        }
    }
}
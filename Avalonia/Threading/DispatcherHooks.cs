// -----------------------------------------------------------------------
// <copyright file="DispatcherHooks.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Threading
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading;

    public sealed class DispatcherHooks
    {
        private Dispatcher owner;

        internal DispatcherHooks(Dispatcher owner)
        {
            this.owner = owner;
        }

        public event EventHandler DispatcherInactive;

        public event DispatcherHookEventHandler OperationAborted;

        public event DispatcherHookEventHandler OperationCompleted;

        public event DispatcherHookEventHandler OperationPosted;

        public event DispatcherHookEventHandler OperationPriorityChanged;

        internal void EmitOperationPosted(DispatcherOperation op)
        {
            DispatcherHookEventHandler posted = this.OperationPosted;
            if (posted != null)
            {
                posted(this.owner, new DispatcherHookEventArgs(op));
            }
        }

        internal void EmitOperationCompleted(DispatcherOperation op)
        {
            DispatcherHookEventHandler completed = this.OperationCompleted;
            if (completed != null)
            {
                completed(this.owner, new DispatcherHookEventArgs(op));
            }
        }

        internal void EmitOperationAborted(DispatcherOperation op)
        {
            DispatcherHookEventHandler aborted = this.OperationAborted;
            if (aborted != null)
            {
                aborted(this.owner, new DispatcherHookEventArgs(op));
            }
        }

        internal void EmitOperationPriorityChanged(DispatcherOperation op)
        {
            DispatcherHookEventHandler prio = this.OperationPriorityChanged;
            if (prio != null)
            {
                prio(this.owner, new DispatcherHookEventArgs(op));
            }
        }

        internal void EmitInactive()
        {
            EventHandler inactive = this.DispatcherInactive;
            if (inactive != null)
            {
                inactive(this.owner, EventArgs.Empty);
            }
        }
    }
}
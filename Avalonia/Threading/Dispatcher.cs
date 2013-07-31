// -----------------------------------------------------------------------
// <copyright file="Dispatcher.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Threading
{
    using System.Threading;

    public sealed class Dispatcher
    {
        private static ThreadLocal<Dispatcher> dispatcher =
            new ThreadLocal<Dispatcher>(() => new Dispatcher());

        private bool shutdown;

        /// <summary>
        /// Prevents a default instance of the <see cref="Dispatcher"/> class from being created.
        /// </summary>
        private Dispatcher()
        {
        }

        public static Dispatcher CurrentDispatcher
        {
            get { return dispatcher.Value; }
        }

        public static IPlatformDispatcherImpl PlatformDispatcher
        {
            get;
            set;
        }

        public void InvokeShutdown()
        {
            this.shutdown = true;
        }

        public void PushFrame(DispatcherFrame frame)
        {
            while (frame.Continue && !this.shutdown)
            {
                PlatformDispatcher.ProcessMessage();
            }
        }

        public void Run()
        {
            this.PushFrame(new DispatcherFrame());
        }
    }
}

namespace Avalonia.Threading
{
    using System.Threading;

    public sealed class Dispatcher
    {
        private static ThreadLocal<Dispatcher> dispatcher = 
            new ThreadLocal<Dispatcher>(() => new Dispatcher());

        private bool shutdown;

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
            PushFrame(new DispatcherFrame());
        }

        private Dispatcher()
        {
        }
    }
}

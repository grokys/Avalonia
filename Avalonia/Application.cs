namespace Avalonia
{
    using System;
    using Avalonia.Threading;

    public class Application : DispatcherObject
    {
        public Application()
        {
            Application.Current = this;
        }

        public static Application Current { get; private set; }
        
        public Window MainWindow { get; set; }
        public Type PresentationSourceType { get; set; }

        public void Run()
        {
            this.Run(this.MainWindow);
        }

        public void Run(Window window)
        {
            if (window != null)
            {
                window.Closed += (s, e) => this.Dispatcher.InvokeShutdown();
                window.Show();
            }

            if (this.MainWindow == null)
            {
                this.MainWindow = window;
            }

            this.Dispatcher.Run();
        }
    }
}

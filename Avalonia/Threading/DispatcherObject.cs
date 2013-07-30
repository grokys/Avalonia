namespace Avalonia.Threading
{
    public class DispatcherObject
    {
        public DispatcherObject()
        {
            this.Dispatcher = Dispatcher.CurrentDispatcher;
        }

        public Dispatcher Dispatcher { get; set; }
    }
}

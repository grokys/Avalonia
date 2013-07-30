namespace Avalonia.Threading
{
    public class DispatcherFrame : DispatcherObject
    {
        public DispatcherFrame()
        {
            this.Continue = true;
        }

        public DispatcherFrame(bool exitWhenRequested)
            : this()
        {
        }

        public bool Continue { get; set; }
    }
}

namespace Avalonia.Direct2D1
{
    using System;
    using Avalonia.Direct2D1.Interop;
    using Avalonia.Threading;

    public class WindowMessageDispatcher : IPlatformDispatcherImpl
    {
        public void ProcessMessage()
        {
            UnmanagedMethods.MSG msg;
            UnmanagedMethods.GetMessage(out msg, IntPtr.Zero, 0, 0);
            UnmanagedMethods.TranslateMessage(ref msg);
            UnmanagedMethods.DispatchMessage(ref msg);
        }
    }
}

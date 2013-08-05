namespace Avalonia.Direct2D1
{
    using System;
    using Avalonia.Direct2D1.Interop;
    using Avalonia.Interop;
    using Avalonia.Platform;
    using Avalonia.Threading;

    public class WindowMessageDispatcher : IPlatformDispatcher
    {
        public void ProcessMessage()
        {
            UnmanagedMethods.MSG msg;
            UnmanagedMethods.GetMessage(out msg, IntPtr.Zero, 0, 0);
            UnmanagedMethods.TranslateMessage(ref msg);
            UnmanagedMethods.DispatchMessage(ref msg);
        }

        public void SendMessage()
        {
            IntPtr result = UnmanagedMethods.PostMessage(
                IntPtr.Zero,
                (int)UnmanagedMethods.WindowsMessage.WM_DISPATCH_WORK_ITEM,
                IntPtr.Zero,
                IntPtr.Zero);
        }

        private IntPtr GetHwnd()
        {
            return new WindowInteropHelper(Application.Current.MainWindow).Handle;
        }
    }
}

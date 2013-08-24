// -----------------------------------------------------------------------
// <copyright file="Win32MouseDevice.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Direct2D1.Input
{
    using Avalonia.Direct2D1.Interop;
    using Avalonia.Input;

    public class Win32MouseDevice : MouseDevice
    {
        private HwndSource source;

        public Win32MouseDevice(HwndSource source)
        {
            this.source = source;
        }

        protected override Point GetClientPosition()
        {
            UnmanagedMethods.POINT p;
            UnmanagedMethods.GetCursorPos(out p);
            UnmanagedMethods.ScreenToClient(this.source.Handle, ref p);
            return new Point(p.X, p.Y);
        }

        protected override Point GetScreenPosition()
        {
            UnmanagedMethods.POINT p;
            UnmanagedMethods.GetCursorPos(out p);
            return new Point(p.X, p.Y);
        }
    }
}

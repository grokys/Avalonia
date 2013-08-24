// -----------------------------------------------------------------------
// <copyright file="Win32MouseDevice.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Direct2D1.Input
{
    using System;
    using Avalonia.Direct2D1.Interop;
    using Avalonia.Input;

    public class Win32MouseDevice : MouseDevice
    {
        private static Win32MouseDevice instance = new Win32MouseDevice();

        private UnmanagedMethods.POINT cursorPos;

        private Win32MouseDevice()
        {
        }

        public static Win32MouseDevice Instance
        {
            get { return instance; }
        }

        public override IInputElement Target
        {
            get 
            {
                if (this.Captured != null)
                {
                    return this.Captured;
                }
                else
                {
                    Point p = this.GetClientPosition();
                    UIElement ui = this.ActiveSource.RootVisual as UIElement;
                    return ui.InputHitTest(p);
                }
            }
        }

        public override void Capture(IInputElement element)
        {
            if (element != null && this.ActiveSource != null)
            {
                UnmanagedMethods.SetCapture(((HwndSource)ActiveSource).Handle);
            }
            else
            {
                UnmanagedMethods.ReleaseCapture();
            }

            base.Capture(element);
        }

        internal void SetActiveSource(PresentationSource source)
        {
            this.ActiveSource = source;
        }

        internal void UpdateCursorPos()
        {
            UnmanagedMethods.GetCursorPos(out this.cursorPos);
        }

        protected override Point GetClientPosition()
        {
            IntPtr handle = ((HwndSource)ActiveSource).Handle;
            UnmanagedMethods.POINT p = this.cursorPos;
            UnmanagedMethods.ScreenToClient(handle, ref p);
            return new Point(p.X, p.Y);
        }

        protected override Point GetScreenPosition()
        {
            return new Point(this.cursorPos.X, this.cursorPos.Y);
        }
    }
}

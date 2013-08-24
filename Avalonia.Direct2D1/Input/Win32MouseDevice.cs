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
        private UnmanagedMethods.POINT cursorPos;

        public Win32MouseDevice(HwndSource source)
            : base(source)
        {
            this.source = source;
        }

        public override IInputElement Target
        {
            get 
            {
                Point p = this.GetClientPosition();
                UIElement ui = source.RootVisual as UIElement;
                return ui.InputHitTest(p);
            }
        }

        internal void UpdateCursorPos()
        {
            UnmanagedMethods.GetCursorPos(out this.cursorPos);
        }

        protected override Point GetClientPosition()
        {
            UnmanagedMethods.POINT p = this.cursorPos;
            UnmanagedMethods.ScreenToClient(this.source.Handle, ref p);
            return new Point(p.X, p.Y);
        }

        protected override Point GetScreenPosition()
        {
            return new Point(this.cursorPos.X, this.cursorPos.Y);
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="Win32MouseDevice.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Direct2D1.Input
{
    using System;
    using System.Text;
    using Avalonia.Direct2D1.Interop;
    using Avalonia.Input;

    public class Win32KeyboardDevice : KeyboardDevice
    {
        private static Win32KeyboardDevice instance = new Win32KeyboardDevice();

        private byte[] keyStates = new byte[256];

        public static Win32KeyboardDevice Instance
        {
            get { return instance; }
        }

        internal void UpdateKeyStates()
        {
            UnmanagedMethods.GetKeyboardState(keyStates);
        }

        protected override string KeyToString(Key key)
        {
            StringBuilder result = new StringBuilder(256);
            int length = UnmanagedMethods.ToUnicode(
                (uint)KeyInterop.VirtualKeyFromKey(key),
                0,
                this.keyStates,
                result,
                256,
                0);
            return result.ToString();
        }

        protected override KeyStates GetKeyStatesFromSystem(Key key)
        {
            int vk = KeyInterop.VirtualKeyFromKey(key);
            byte state = keyStates[vk];
            KeyStates result = 0;

            if ((state & 0x80) != 0)
            {
                result |= KeyStates.Down;
            }

            if ((state & 0x01) != 0)
            {
                result |= KeyStates.Toggled;
            }

            return result;
        }

        internal void SetActiveSource(PresentationSource source)
        {
            this.ActiveSource = source;
        }
    }
}

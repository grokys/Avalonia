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

    public class Win32KeyboardDevice : KeyboardDevice
    {
        private static Win32KeyboardDevice instance = new Win32KeyboardDevice();

        public static Win32KeyboardDevice Instance
        {
            get { return instance; }
        }
    }
}

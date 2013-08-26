// -----------------------------------------------------------------------
// <copyright file="KeyboardEventArgs.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Input
{
    public class KeyboardEventArgs : InputEventArgs
    {
        public KeyboardEventArgs(KeyboardDevice keyboard, int timestamp)
            : base(keyboard, timestamp)
        {
        }

        public KeyboardDevice KeyboardDevice 
        {
            get { return (KeyboardDevice)this.Device; }
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="InputEventArgs.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Input
{
    public class InputEventArgs : RoutedEventArgs
    {
        public InputEventArgs(InputDevice inputDevice, int timestamp)
        {
            this.Device = inputDevice;
            this.Timestamp = timestamp;
        }

        public InputDevice Device { get; private set; }

        public int Timestamp { get; private set; }
    }
}

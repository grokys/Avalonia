// -----------------------------------------------------------------------
// <copyright file="RawKeyEventArgs.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Avalonia.Input;

    public enum RawKeyEventType
    {
        KeyDown,
        KeyUp
    }

    [AvaloniaSpecific]
    public class RawKeyEventArgs : InputEventArgs
    {
        public RawKeyEventArgs(KeyboardDevice device, RawKeyEventType type, Key key)
            : base(device, Environment.TickCount)
        {
            this.Key = key;
            this.Type = type;
        }

        public Key Key { get; private set; }

        public RawKeyEventType Type { get; private set; }
    }
}

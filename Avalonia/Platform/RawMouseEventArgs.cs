// -----------------------------------------------------------------------
// <copyright file="RawMouseMoveEventArgs.cs" company="Steven Kirk">
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

    public enum RawMouseEventType
    {
        Move,
    }

    [AvaloniaSpecific]
    public class RawMouseEventArgs : InputEventArgs
    {
        public RawMouseEventArgs(MouseDevice device, RawMouseEventType type)
            : base(device, Environment.TickCount)
        {
            this.Type = type;
        }

        public RawMouseEventType Type { get; private set; }
    }
}

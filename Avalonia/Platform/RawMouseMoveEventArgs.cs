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

    [AvaloniaSpecific]
    public class RawMouseMoveEventArgs : InputEventArgs
    {
        public RawMouseMoveEventArgs(MouseDevice device, int timestamp, PresentationSource presentationSource)
            : base(device, timestamp)
        {
            this.PresentationSource = presentationSource;
        }

        public PresentationSource PresentationSource { get; set; }
    }
}

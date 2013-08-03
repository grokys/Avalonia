// -----------------------------------------------------------------------
// <copyright file="DispatcherFrame.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Threading
{
    using System.Security;

    public class DispatcherFrame : DispatcherObject
    {
        public DispatcherFrame()
        {
            this.ExitOnRequest = true;
            this.Continue = true;
        }

        public DispatcherFrame(bool exitWhenRequested)
        {
            this.ExitOnRequest = exitWhenRequested;
            this.Continue = true;
        }

        public bool Continue { get; set; }

        internal Dispatcher Running { get; set; }

        internal DispatcherFrame ParentFrame { get; set; }

        internal bool ExitOnRequest { get; set; }
    }
}

// -----------------------------------------------------------------------
// <copyright file="DispatcherFrame.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Threading
{
    public class DispatcherFrame : DispatcherObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherFrame"/> class.
        /// </summary>
        public DispatcherFrame()
        {
            this.Continue = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherFrame"/> class.
        /// </summary>
        public DispatcherFrame(bool exitWhenRequested)
            : this()
        {
        }

        public bool Continue { get; set; }

        public Dispatcher Running { get; internal set; }
    }
}

// -----------------------------------------------------------------------
// <copyright file="CurrentChangingEventArgs.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System;

    public delegate void CurrentChangingEventHandler(object sender, CurrentChangingEventArgs e);

    public class CurrentChangingEventArgs : EventArgs
    {
        public CurrentChangingEventArgs()
        {
            this.IsCancelable = true;
        }

        public CurrentChangingEventArgs(bool isCancelable)
        {
            this.IsCancelable = isCancelable;
        }

        public bool Cancel { get; set; }

        public bool IsCancelable { get; private set; }
    }
}

// -----------------------------------------------------------------------
// <copyright file="PlatformPresentationSource.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Platform
{
    using System;
    using Avalonia.Input;
    using Avalonia.Media;

    [AvaloniaSpecific]
    public abstract class PlatformPresentationSource : PresentationSource
    {
        public event EventHandler Closed;

        public event EventHandler Resized;

        public abstract Size ClientSize { get; }

        public abstract Rect BoundingRect { get; set; }

        public IntPtr Handle { get; protected set; }

        public abstract DrawingContext CreateDrawingContext();

        public abstract void Show();

        protected void OnClosed()
        {
            if (this.Closed != null)
            {
                this.Closed(this, EventArgs.Empty);
            }
        }

        protected void OnResized()
        {
            if (this.Resized != null)
            {
                this.Resized(this, EventArgs.Empty);
            }
        }
    }
}

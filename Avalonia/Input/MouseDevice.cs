// -----------------------------------------------------------------------
// <copyright file="MouseDevice.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Input
{
    using System;
    using System.Linq;
    using Avalonia.Media;

    public abstract class MouseDevice : InputDevice
    {
        public Point GetPosition(IInputElement relativeTo)
        {
            Point p = this.GetClientPosition();
            Visual v = (Visual)relativeTo;

            if (v != null)
            {
                p -= v.VisualOffset;

                foreach (Visual ancestor in VisualTreeHelper.GetAncestors(v).OfType<Visual>())
                {
                    p -= ancestor.VisualOffset;
                }
            }

            return p;
        }

        protected abstract Point GetClientPosition();

        protected abstract Point GetScreenPosition();
    }
}

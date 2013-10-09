// -----------------------------------------------------------------------
// <copyright file="Expression.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    public abstract class Expression
    {
        internal bool Attached
        {
            get;
            private set;
        }

        internal bool Updating
        {
            get;
            set;
        }

        internal Expression()
        {
        }

        internal abstract object GetValue(DependencyProperty dp);

        internal virtual void OnAttached(DependencyObject element)
        {
            Attached = true;
        }

        internal virtual void OnDetached(DependencyObject element)
        {
            Attached = false;
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="TriggerBase.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    public abstract class TriggerBase : DependencyObject
    {
        internal abstract void Attach(FrameworkElement element, DependencyObject parent);
    }
}

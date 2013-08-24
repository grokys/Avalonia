// -----------------------------------------------------------------------
// <copyright file="SetterBaseCollection.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class SetterBaseCollection : Collection<SetterBase>
    {
        internal void Attach(FrameworkElement element)
        {
            foreach (SetterBase setter in this)
            {
                setter.Attach(element);
            }
        }

        internal void Detach(FrameworkElement element)
        {
            foreach (SetterBase setter in this)
            {
                setter.Detach(element);
            }
        }
    }
}

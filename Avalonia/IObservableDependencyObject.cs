// -----------------------------------------------------------------------
// <copyright file="IObservableDependencyObject.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IObservableDependencyObject
    {
        void AttachPropertyChangedHandler(
            string propertyName,
            DependencyPropertyChangedEventHandler handler);

        void RemovePropertyChangedHandler(
            string propertyName,
            DependencyPropertyChangedEventHandler handler);
    }
}

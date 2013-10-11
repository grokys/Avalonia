// -----------------------------------------------------------------------
// <copyright file="IPropertyPathNode.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System;
    using System.Reflection;

    interface IPropertyPathNode
    {
        event EventHandler IsBrokenChanged;

        event EventHandler ValueChanged;

        bool IsBroken { get; }

        IPropertyPathNode Next { get; set; }

        void SetValue(object value);

        object Source { get; }

        object Value { get; }

        PropertyInfo PropertyInfo { get; }

        Type ValueType { get; }

        void SetSource(object source);
    }
}

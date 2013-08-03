// -----------------------------------------------------------------------
// <copyright file="ResourceReferenceKeyNotFoundException.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ResourceReferenceKeyNotFoundException : Exception
    {
        public ResourceReferenceKeyNotFoundException(string message, object key)
            : base(message)
        {
            this.Key = key;
        }

        public object Key { get; private set; }
    }
}

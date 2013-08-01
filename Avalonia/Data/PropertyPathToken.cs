// -----------------------------------------------------------------------
// <copyright file="PropertyPathToken.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public struct PropertyPathToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyPathToken"/> struct.
        /// </summary>
        public PropertyPathToken(object o, string propertyName)
                    : this()
        {
            this.Object = o;
            this.PropertyName = propertyName;
        }

        public object Object { get; private set; }

        public string PropertyName { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}", this.Object, this.PropertyName);
        }
    }
}

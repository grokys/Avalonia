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

    public enum PropertyPathTokenType
    {
        /// <summary>
        /// The token leads to another token.
        /// </summary>
        Valid,

        /// <summary>
        /// The token is the last in the chain because the requested 
        /// <see cref="PropertyPathToken.PropertyName"/> was not found on 
        /// <see cref="PropertyPathToken.Object"/>.
        /// </summary>
        Broken,

        /// <summary>
        /// The token is the last in the chain because the bound value was found and is stored
        /// in <see cref="PropertyPathToken.Object"/>.
        /// </summary>
        FinalValue
    }

    public struct PropertyPathToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyPathToken"/> struct.
        /// </summary>
        /// <param name="o">
        /// The object at this stage of the property path.
        /// </param>
        /// <param name="propertyName">
        /// The name of the property on <paramref cref="o"/> which leads to the next 
        /// token.
        /// </param>
        public PropertyPathToken(object o, string propertyName)
                    : this()
        {
            this.Object = o;
            this.PropertyName = propertyName;
        }

        /// <summary>
        /// Gets or sets the object at this stage of the property path.
        /// </summary>
        public object Object { get; set; }

        /// <summary>
        /// Gets or sets the name of the property on <see cref="Object"/> which leads to the next
        /// <see cref="PropertyPathToken"/>, or this is the final value.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the type of link in the chain of property path tokens.
        /// </summary>
        public PropertyPathTokenType Type { get; set; }

        /// <summary>
        /// Returns a string that represents the property path token.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return string.Format("{0}, {1}", this.Object, this.PropertyName);
        }
    }
}

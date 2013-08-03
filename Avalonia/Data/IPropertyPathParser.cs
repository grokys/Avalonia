// -----------------------------------------------------------------------
// <copyright file="IPropertyPathParser.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System.Collections.Generic;

    public interface IPropertyPathParser
    {
        IEnumerable<PropertyPathToken> Parse(object source, string path);
    }
}

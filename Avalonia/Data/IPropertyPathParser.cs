// -----------------------------------------------------------------------
// <copyright file="IPropertyPathParser.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    public interface IPropertyPathParser
    {
        PropertyPathToken[] Parse(object source, string path);
    }
}

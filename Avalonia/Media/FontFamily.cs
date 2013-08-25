// -----------------------------------------------------------------------
// <copyright file="FontFamily.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    public class FontFamily
    {
        public FontFamily()
        {
        }

        public FontFamily(string familyName)
        {
            this.Source = familyName;
        }
        
        public string Source { get; private set; }
    }
}

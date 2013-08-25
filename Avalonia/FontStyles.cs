// -----------------------------------------------------------------------
// <copyright file="FontStyles.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    public static class FontStyles
    {
        private static FontStyle normal = new FontStyle(0);

        private static FontStyle oblique = new FontStyle(1);
         
        private static FontStyle italic = new FontStyle(2);

        public static FontStyle Normal
        {
            get { return normal; }            
        }

        public static FontStyle Oblique
        {
            get { return oblique; }
        }

        public static FontStyle Italic
        {
            get { return italic; }
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="FontWeights.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    public static class FontWeights
    {
        private static FontWeight normal = new FontWeight();

        public static FontWeight Normal 
        {
            get { return normal; }
        }
    }
}

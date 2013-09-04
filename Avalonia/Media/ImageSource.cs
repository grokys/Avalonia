// -----------------------------------------------------------------------
// <copyright file="ImageSource.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    using Avalonia.Animation;

    public abstract class ImageSource : Animatable
    {
        public abstract double Width { get; }

        public abstract double Height { get; }
    }
}

// -----------------------------------------------------------------------
// <copyright file="ImageSource.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    using System.ComponentModel;
    using Avalonia.Animation;

    [TypeConverter(typeof(ImageSourceConverter))]
    public abstract class ImageSource : Animatable
    {
        public abstract double Width { get; }

        public abstract double Height { get; }
    }
}
